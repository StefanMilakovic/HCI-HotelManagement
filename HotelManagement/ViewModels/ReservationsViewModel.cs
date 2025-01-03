using HotelManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HotelManagement
{
    public class ReservationsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Reservation> _reservations;
        public ObservableCollection<Reservation> Reservations
        {
            get => _reservations;
            set
            {
                _reservations = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddReservationCommand { get; }
        public ICommand DeleteReservationCommand { get; }

        public int ReservationID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int RoomID { get; set; }

        public ReservationsViewModel()
        {
            //AddReservationCommand = new RelayCommand(AddReservation, CanAddReservation);
            DeleteReservationCommand = new RelayCommand(DeleteReservation, CanDeleteReservation);

            LoadReservations();
        }

        private void LoadReservations()
        {
            using (var context = new HotelManagementContext())
            {
                Reservations = new ObservableCollection<Reservation>(context.Reservations.ToList());
            }
        }

        private bool CanAddReservation(object parameter)
        {
            return CheckInDate != default &&
                   CheckOutDate != default &&
                   NumberOfGuests > 0 &&
                   RoomID > 0 &&
                   CheckInDate < CheckOutDate; // Validacija: Check-in pre Check-out
        }



        /*
        private void AddReservation(object parameter)
        {
            using (var context = new HotelManagementContext())
            {
                var newReservation = new Reservation
                {
                    CheckInDate = this.CheckInDate,
                    CheckOutDate = this.CheckOutDate,
                    NumberOfGuests = this.NumberOfGuests,
                    RoomID = this.RoomID
                };

                context.Reservations.Add(newReservation);
                context.SaveChanges();

                Reservations.Add(newReservation);

                CheckInDate = default;
                CheckOutDate = default;
                NumberOfGuests = 0;
                RoomID = 0;

                OnPropertyChanged(nameof(CheckInDate));
                OnPropertyChanged(nameof(CheckOutDate));
                OnPropertyChanged(nameof(NumberOfGuests));
                OnPropertyChanged(nameof(RoomID));
            }
        }
        */


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
        }

        private bool CanDeleteReservation(object parameter)
        {
            return parameter is Reservation; // Osiguraj da je validan parametar
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
