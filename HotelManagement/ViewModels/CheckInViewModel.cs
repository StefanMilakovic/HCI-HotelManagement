using GalaSoft.MvvmLight.CommandWpf;
using HotelManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    public class CheckInViewModel
    {
        private ObservableCollection<Reservation> _reservations;
        private Reservation _selectedReservation;
        private bool _isNextButtonEnabled;

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
                IsNextButtonEnabled = _selectedReservation != null;
            }
        }

        public bool IsNextButtonEnabled
        {
            get => _isNextButtonEnabled;
            set
            {
                _isNextButtonEnabled = value;
                OnPropertyChanged(nameof(IsNextButtonEnabled));
            }
        }

        public ICommand SelectReservationCommand { get; }


        public CheckInViewModel()
        {
            SelectReservationCommand = new RelayCommand<Reservation>(SelectReservation);
            LoadReservations();
        }



        private void SelectReservation(Reservation reservation)
        {
            SelectedReservation = reservation;
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



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
