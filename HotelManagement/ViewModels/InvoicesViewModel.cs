using GalaSoft.MvvmLight.CommandWpf;
using HotelManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;



namespace HotelManagement.ViewModels
{
    public class InvoicesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Invoice> _invoices;
        private ObservableCollection<Reservation> _reservations;
        private Reservation _selectedReservation;
        private bool _isGenerateButtonEnabled;

        private ObservableCollection<Reservation> _filteredReservations;
        private string _searchReservationText;
        private ObservableCollection<Invoice> _filteredInvoices;
        private string _searchInvoiceText;


        public ObservableCollection<Invoice> FilteredInvoices
        {
            get => _filteredInvoices;
            set
            {
                _filteredInvoices = value;
                OnPropertyChanged();
            }
        }

        public string SearchInvoiceText
        {
            get => _searchInvoiceText;
            set
            {
                if (_searchInvoiceText != value)
                {
                    _searchInvoiceText = value;
                    OnPropertyChanged();
                    FilterInvoices();
                }
            }
        }


        public ObservableCollection<Reservation> FilteredReservations
        {
            get => _filteredReservations;
            set
            {
                _filteredReservations = value;
                OnPropertyChanged();
            }
        }

        public string SearchReservationText
        {
            get => _searchReservationText;
            set
            {
                if (_searchReservationText != value)
                {
                    _searchReservationText = value;
                    OnPropertyChanged();
                    FilterReservations();
                }
            }
        }

        private void FilterReservations()
        {
            if (string.IsNullOrEmpty(SearchReservationText))
            {
                FilteredReservations = Reservations;
            }
            else
            {
                FilteredReservations = new ObservableCollection<Reservation>(
                    Reservations.Where(r => r.GuestName.StartsWith(SearchReservationText, StringComparison.InvariantCultureIgnoreCase))
                );
            }
        }

        private void FilterInvoices()
        {
            if (string.IsNullOrWhiteSpace(SearchInvoiceText))
            {
                FilteredInvoices = Invoices;
            }
            else
            {
                FilteredInvoices = new ObservableCollection<Invoice>(
                    Invoices.Where(i => i.GuestName.Contains(SearchInvoiceText, StringComparison.InvariantCultureIgnoreCase))
                );
            }
        }
        public ObservableCollection<Invoice> Invoices
        {
            get => _invoices;
            set
            {
                _invoices = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Reservation> Reservations
        {
            get => _reservations;
            set
            {
                _reservations = value;
                OnPropertyChanged(nameof(Reservations));
            }
        }

        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
                IsGenerateButtonEnabled = _selectedReservation != null;
            }
        }

        public bool IsGenerateButtonEnabled
        {
            get => _isGenerateButtonEnabled;
            set
            {
                _isGenerateButtonEnabled = value;
                OnPropertyChanged(nameof(IsGenerateButtonEnabled));
            }
        }

        public ICommand AddInvoiceCommand { get; }
        public ICommand DeleteInvoiceCommand { get; }
        public ICommand SelectReservationCommand { get; }
        public ICommand GenerateInvoiceCommand { get; }

        public int InvoiceID { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int GuestID { get; set; }
        public int ReservationID { get; set; }

        public InvoicesViewModel()
        {
            AddInvoiceCommand = new RelayCommand(AddInvoice, CanAddInvoice);
            DeleteInvoiceCommand = new RelayCommand(DeleteInvoice, CanDeleteInvoice);
            SelectReservationCommand = new RelayCommand<Reservation>(SelectReservation);
            GenerateInvoiceCommand = new RelayCommand(GenerateInvoice);

            LoadInvoices();
            LoadReservations();
        }


        /*
        private void LoadInvoices()
        {
            using (var context = new HotelManagementContext())
            {
                Invoices = new ObservableCollection<Invoice>(context.Invoices.ToList());
            }
        }
        */

        private void LoadInvoices()
        {
            using (var context = new HotelManagementContext())
            {
                var invoices = context.Invoices.ToList();
                Invoices = new ObservableCollection<Invoice>(invoices);
                FilteredInvoices = Invoices;
            }
        }

        /*
        private void LoadReservations()
        {
            using (var context = new HotelManagementContext())
            {
                var invoices = context.Invoices.ToList();
                var reservations = context.Reservations.ToList();

                var filteredReservations = reservations
                    .Where(reservation => !invoices.Any(invoice => invoice.ReservationID == reservation.ReservationID))
                    .ToList();

                Reservations = new ObservableCollection<Reservation>(filteredReservations);
            }
        }*/

        private void LoadReservations()
        {
            using (var context = new HotelManagementContext())
            {
                var invoices = context.Invoices.ToList();
                var reservations = context.Reservations.ToList();

                var filteredReservations = reservations
                    .Where(reservation => !invoices.Any(invoice => invoice.ReservationID == reservation.ReservationID))
                    .ToList();

                _reservations = new ObservableCollection<Reservation>(filteredReservations);
                _filteredReservations = _reservations;
            }
        }



        private void SelectReservation(Reservation reservation)
        {
            SelectedReservation = reservation;
        }

        private bool CanAddInvoice(object parameter)
        {
            return Date != default &&
                   TotalAmount > 0 &&
                   GuestID > 0 &&
                   ReservationID > 0;
        }

        private void AddInvoice(object parameter)
        {
            using (var context = new HotelManagementContext())
            {
                var newInvoice = new Invoice
                {
                    IssuedDate = this.Date,
                    TotalAmount = this.TotalAmount,
                    GuestID = this.GuestID,
                    ReservationID = this.ReservationID
                };

                context.Invoices.Add(newInvoice);
                context.SaveChanges();

                Invoices.Add(newInvoice);

                Date = default;
                TotalAmount = 0;
                GuestID = 0;
                ReservationID = 0;

                OnPropertyChanged(nameof(Date));
                OnPropertyChanged(nameof(TotalAmount));
                OnPropertyChanged(nameof(GuestID));
                OnPropertyChanged(nameof(ReservationID));

            }
        }

        
        private void GenerateInvoice(object parameter)
        {
            if (SelectedReservation == null)
            {
                throw new InvalidOperationException("No reservation selected.");
            }

            using (var context = new HotelManagementContext())
            {
                // Pronađi rezervaciju
                var reservation = context.Reservations
                    .FirstOrDefault(r => r.ReservationID == SelectedReservation.ReservationID);

                if (reservation == null)
                {
                    throw new InvalidOperationException("Reservation not found.");
                }

                // Pronađi sobu povezanu sa rezervacijom
                var room = context.Rooms.FirstOrDefault(r => r.RoomID == reservation.RoomID);

                if (room == null)
                {
                    throw new InvalidOperationException("Room associated with the reservation not found.");
                }

                // Pronađi RoomType povezanu sa sobom
                var roomType = context.RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == room.RoomTypeId);

                if (roomType == null)
                {
                    throw new InvalidOperationException("Room type not found.");
                }

                // Izračunaj broj noćenja
                var totalNights = (reservation.CheckOutDate - reservation.CheckInDate).Days;

                if (totalNights <= 0)
                {
                    throw new InvalidOperationException("Invalid reservation dates.");
                }

                // Cijena sobe na osnovu broja noćenja
                var roomPrice = totalNights * (decimal)roomType.PricePerNight;

                // Pronađi dodatne servise povezane sa rezervacijom
                var reservationServices = context.ReservationHasServices
                    .Where(rhs => rhs.ReservationId == reservation.ReservationID)
                    .ToList();

                // Izračunaj ukupnu cijenu servisa
                var servicesPrice = reservationServices.Sum(service => service.TotalPrice);

                // Ukupna cijena (soba + servisi)
                var totalPrice = roomPrice + servicesPrice;

                // Kreiraj novi račun (Invoice)
                var newInvoice = new Invoice
                {
                    IssuedDate = DateTime.Now,
                    TotalAmount = totalPrice,
                    GuestID = reservation.GuestID,
                    ReservationID = reservation.ReservationID
                };

                context.Invoices.Add(newInvoice);
                context.SaveChanges();

                Invoices.Add(newInvoice);
                LoadReservations();
                LoadInvoices();
                System.Windows.MessageBox.Show("New invoice created successfully!", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);


            }
        }

        private void DeleteInvoice(object parameter)
        {
            var invoiceToDelete = parameter as Invoice;
            if (invoiceToDelete == null) return;

            using (var context = new HotelManagementContext())
            {
                context.Invoices.Remove(invoiceToDelete);
                context.SaveChanges();
            }

            Invoices.Remove(invoiceToDelete);
            FilterInvoices();
        }

        private bool CanDeleteInvoice(object parameter)
        {
            return parameter is Invoice;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
