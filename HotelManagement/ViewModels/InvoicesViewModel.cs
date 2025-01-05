using GalaSoft.MvvmLight.CommandWpf;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace HotelManagement.ViewModels
{
    public class InvoicesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Invoice> _invoices;
        private ObservableCollection<Reservation> _reservations;
        private Reservation _selectedReservation;
        private bool _isGenerateButtonEnabled;
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


        
        private void LoadInvoices()
        {
            using (var context = new HotelManagementContext())
            {
                Invoices = new ObservableCollection<Invoice>(context.Invoices.ToList());
            }
        }

        private void LoadReservations()
        {
            using (var context = new HotelManagementContext())
            {
                // Dohvati sve račune (Invoices) iz baze
                var invoices = context.Invoices.ToList(); // Dohvatanje svih računa u memoriju

                // Dohvati sve rezervacije (Reservations) iz baze
                var reservations = context.Reservations.ToList(); // Dohvatanje svih rezervacija u memoriju

                // Filtriraj rezervacije koje nemaju povezane račune
                var filteredReservations = reservations
                    .Where(reservation => !invoices.Any(invoice => invoice.ReservationID == reservation.ReservationID))
                    .ToList(); // Filtriranje u memoriji

                // Postavi filtrirane rezervacije u ObservableCollection
                Reservations = new ObservableCollection<Reservation>(filteredReservations);
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
                   ReservationID > 0; // Validacija unosa
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

                // Reset unosa
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

                // Cena sobe na osnovu broja noćenja
                var roomPrice = totalNights * (decimal)roomType.PricePerNight;

                // Pronađi dodatne servise povezane sa rezervacijom
                var reservationServices = context.ReservationHasServices
                    .Where(rhs => rhs.ReservationId == reservation.ReservationID)
                    .ToList();

                // Izračunaj ukupnu cenu servisa
                var servicesPrice = reservationServices.Sum(service => service.TotalPrice);

                // Ukupna cena (soba + servisi)
                var totalPrice = roomPrice + servicesPrice;

                // Kreiraj novi račun (Invoice)
                var newInvoice = new Invoice
                {
                    IssuedDate = DateTime.Now,
                    TotalAmount = totalPrice,
                    GuestID = reservation.GuestID,
                    ReservationID = reservation.ReservationID
                };

                // Sačuvaj račun u bazi podataka
                context.Invoices.Add(newInvoice);
                context.SaveChanges();

                // Dodaj račun u kolekciju
                Invoices.Add(newInvoice);

                // Osveži listu dostupnih rezervacija
                LoadReservations();

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
        }

        private bool CanDeleteInvoice(object parameter)
        {
            return parameter is Invoice; // Osiguraj da je validan parametar
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
