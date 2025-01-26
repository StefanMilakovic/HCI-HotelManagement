using System.Linq;
using System.Collections.ObjectModel;
using HotelManagement.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    public class ReportsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Reservation> _reservations;
        private ObservableCollection<Invoice> _invoices;
        private ObservableCollection<Reservation> _filteredReservations;
        private ObservableCollection<Invoice> _filteredInvoices;
        private string _searchReservationText;
        private string _searchInvoiceText;

        public ObservableCollection<Reservation> Reservations
        {
            get => _reservations;
            set
            {
                _reservations = value;
                OnPropertyChanged(nameof(Reservations));
                FilterReservations();
            }
        }

        public ObservableCollection<Invoice> Invoices
        {
            get => _invoices;
            set
            {
                _invoices = value;
                OnPropertyChanged(nameof(Invoices));
                FilterInvoices();
            }
        }

        public ObservableCollection<Reservation> FilteredReservations
        {
            get => _filteredReservations;
            set
            {
                _filteredReservations = value;
                OnPropertyChanged(nameof(FilteredReservations));
            }
        }

        public ObservableCollection<Invoice> FilteredInvoices
        {
            get => _filteredInvoices;
            set
            {
                _filteredInvoices = value;
                OnPropertyChanged(nameof(FilteredInvoices));
            }
        }

        public string SearchReservationText
        {
            get => _searchReservationText;
            set
            {
                _searchReservationText = value;
                OnPropertyChanged(nameof(SearchReservationText));
                FilterReservations();
            }
        }

        public string SearchInvoiceText
        {
            get => _searchInvoiceText;
            set
            {
                _searchInvoiceText = value;
                OnPropertyChanged(nameof(SearchInvoiceText));
                FilterInvoices();
            }
        }

        public ICommand DeleteReservationCommand { get; }
        public ICommand DeleteInvoiceCommand { get; }

        public ReportsViewModel()
        {
            LoadReservations();
            LoadInvoices();
            DeleteReservationCommand = new RelayCommand(DeleteReservation, CanDeleteReservation);
            DeleteInvoiceCommand = new RelayCommand(DeleteInvoice, CanDeleteInvoice);
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
            if (string.IsNullOrEmpty(SearchInvoiceText))
            {
                FilteredInvoices = Invoices;
            }
            else
            {
                FilteredInvoices = new ObservableCollection<Invoice>(
                    Invoices.Where(i => i.GuestName.StartsWith(SearchInvoiceText, StringComparison.InvariantCultureIgnoreCase))
                );
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

        private bool CanDeleteReservation(object parameter)
        {
            return parameter is Reservation;
        }

        private void DeleteReservation(object parameter)
        {
            var reservationToDelete = parameter as Reservation;
            if (reservationToDelete == null) return;

            using (var context = new HotelManagementContext())
            {
                context.Reservations.Remove(reservationToDelete);
                context.SaveChanges();
            }

            Reservations.Remove(reservationToDelete);
            FilterReservations();
        }

        private void LoadReservations()
        {
            using (var context = new HotelManagementContext())
            {
                Reservations = new ObservableCollection<Reservation>(context.Reservations.ToList());
                FilterReservations();
            }
        }

        private void LoadInvoices()
        {
            using (var context = new HotelManagementContext())
            {
                Invoices = new ObservableCollection<Invoice>(context.Invoices.ToList());
                FilterInvoices();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
