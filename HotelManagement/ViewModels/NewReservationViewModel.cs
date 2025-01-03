using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;



using System;
using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

namespace HotelManagement.ViewModels
{
    public class NewReservationViewModel : INotifyPropertyChanged
    {
        // Properties for binding
        private Guest _selectedGuest;
        private RoomType _selectedRoomType;
        private DateTime? _checkInDate;
        private DateTime? _checkOutDate;
        private Room _selectedRoom;
        private ObservableCollection<Reservation> _reservations;

        public ObservableCollection<Guest> Guests { get; set; }
        public ObservableCollection<RoomType> RoomTypes { get; set; }
        public ObservableCollection<Room> AvailableRooms { get; set; }

        public Guest SelectedGuest
        {
            get => _selectedGuest;
            set
            {
                _selectedGuest = value;
                OnPropertyChanged(nameof(SelectedGuest));
            }
        }

        public RoomType SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                OnPropertyChanged(nameof(SelectedRoomType));
                UpdateAvailableRooms(); // Update available rooms when room type changes
            }
        }

        public DateTime? CheckInDate
        {
            get => _checkInDate;
            set
            {
                _checkInDate = value;
                OnPropertyChanged(nameof(CheckInDate));
            }
        }

        public DateTime? CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                _checkOutDate = value;
                OnPropertyChanged(nameof(CheckOutDate));
            }
        }

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
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

        // Commands
        public ICommand AddReservationCommand { get; }
        public ICommand DeleteReservationCommand { get; }

        public NewReservationViewModel()
        {
            // Initialize collections
            //Guests = new ObservableCollection<Guest>();
            Guests = new GuestsViewModel().Guests;
            
            RoomTypes = new ObservableCollection<RoomType>(Enum.GetValues(typeof(RoomType)).Cast<RoomType>());
            AvailableRooms = new RoomsViewModel().Rooms;
            //Reservations = new ObservableCollection<Reservation>();
            //Reservations = new ReservationsViewModel().Reservations;

          
            AddReservationCommand = new RelayCommand(AddReservation, CanAddReservation);
            DeleteReservationCommand = new RelayCommand(DeleteReservation, CanDeleteReservation);
        }

        public void RefreshData()
        {
            Guests = new GuestsViewModel().Guests;
        }

        

        private void UpdateAvailableRooms()
        {
            // Filter rooms by the selected room type (this is a sample; replace with actual filtering logic)
            if (SelectedRoomType != null)
            {
                var filteredRooms = AvailableRooms.Where(room => room.RoomType == SelectedRoomType).ToList();
                AvailableRooms.Clear();
                foreach (var room in filteredRooms)
                {
                    AvailableRooms.Add(room);
                }
            }
        }

        private bool CanAddReservation(object parameter)
        {
            // Validate input fields
            return SelectedGuest != null && SelectedRoomType != null && CheckInDate.HasValue && CheckOutDate.HasValue && SelectedRoom != null;
        }

        private void AddReservation(object parameter)
        {
            // Add new reservation
            var newReservation = new Reservation
            {
                ReservationID = Reservations.Count + 1,
                GuestID = SelectedGuest.GuestId,
                RoomID = SelectedRoom.RoomID,
                CheckInDate = CheckInDate.Value,
                CheckOutDate = CheckOutDate.Value,
                NumberOfGuests = 1 // Placeholder; replace with actual logic
            };

            Reservations.Add(newReservation);

            // Clear fields after adding
            SelectedGuest = null;
            SelectedRoomType = default;
            CheckInDate = null;
            CheckOutDate = null;
            SelectedRoom = null;

            MessageBox.Show("Reservation added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanDeleteReservation(object parameter)
        {
            return parameter is Reservation;
        }

        private void DeleteReservation(object parameter)
        {
            if (parameter is Reservation reservation)
            {
                Reservations.Remove(reservation);
                MessageBox.Show("Reservation deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
