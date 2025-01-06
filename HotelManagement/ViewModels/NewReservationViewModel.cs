using HotelManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    public class NewReservationViewModel : INotifyPropertyChanged
    {
        private Guest _selectedGuest;
        private RoomType _selectedRoomType;
        private DateTime? _checkInDate;
        private DateTime? _checkOutDate;
        private Room _selectedRoom;



        private ObservableCollection<Reservation> _reservations;
        public ObservableCollection<RoomType> _roomTypes;


        public ObservableCollection<Guest> Guests { get; set; }
        public ObservableCollection<Room> AvailableRooms { get; set; }


        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged();
            }
        }

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
                UpdateAvailableRooms();
            }
        }

        public DateTime? CheckInDate
        {
            get => _checkInDate;
            set
            {
                _checkInDate = value;
                OnPropertyChanged(nameof(CheckInDate));
                UpdateAvailableRooms();
            }
        }

        public DateTime? CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                _checkOutDate = value;
                OnPropertyChanged(nameof(CheckOutDate));
                UpdateAvailableRooms();
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
            //Guests = new ObservableCollection<Guest>();
            Guests = new GuestsViewModel().Guests;

            //RoomTypes = new ObservableCollection<RoomType>(Enum.GetValues(typeof(RoomType)).Cast<RoomType>());
            AvailableRooms = new RoomsViewModel().Rooms;

            LoadReservations();
            LoadRoomTypes();


            AddReservationCommand = new RelayCommand(AddReservation, CanAddReservation);
            DeleteReservationCommand = new RelayCommand(DeleteReservation, CanDeleteReservation);
        }


        
        private void UpdateAvailableRooms()
        {
            if (SelectedRoomType != null && CheckInDate.HasValue && CheckOutDate.HasValue)
            {
                using (var context = new HotelManagementContext())
                {
                    var availableRooms = context.Rooms
                        .Where(room => room.RoomTypeId == SelectedRoomType.RoomTypeId)
                        .Where(room => !context.Reservations.Any(reservation =>
                            reservation.RoomID == room.RoomID &&
                            ((CheckInDate.Value >= reservation.CheckInDate && CheckInDate.Value < reservation.CheckOutDate) ||
                             (CheckOutDate.Value > reservation.CheckInDate && CheckOutDate.Value <= reservation.CheckOutDate))))
                        .ToList();

                    AvailableRooms.Clear();
                    foreach (var room in availableRooms)
                    {
                        AvailableRooms.Add(room);
                    }
                }
            }
            else
            {
                AvailableRooms.Clear();
            }
        }
        

        private void LoadRoomTypes()
        {
            using (var context = new HotelManagementContext())
            {
                RoomTypes = new ObservableCollection<RoomType>(context.RoomTypes.ToList());
            }
        }
        private bool CanAddReservation(object parameter)
        {
            return SelectedGuest != null && SelectedRoomType != null && CheckInDate.HasValue && CheckOutDate.HasValue && SelectedRoom != null;
        }

        private void AddReservation(object parameter)
        {
            
            using (var context = new HotelManagementContext())
            {
                var newReservation = new Reservation
                {
                    GuestID = SelectedGuest.GuestId,
                    RoomID = SelectedRoom.RoomID,
                    CheckInDate = CheckInDate.Value,
                    CheckOutDate = CheckOutDate.Value,
                    UserID = CurrentUser.UserID
                };

                context.Reservations.Add(newReservation);
                context.SaveChanges();

                Reservations.Add(newReservation);

                OnPropertyChanged(nameof(Reservations));

                SelectedGuest = null;
                SelectedRoomType = default;
                CheckInDate = null;
                CheckOutDate = null;
                SelectedRoom = null;

                OnPropertyChanged(nameof(CheckInDate));
                OnPropertyChanged(nameof(CheckOutDate));
                OnPropertyChanged(nameof(SelectedGuest));
                OnPropertyChanged(nameof(SelectedRoomType));
                OnPropertyChanged(nameof(SelectedRoom));
            }

        }
        
        private void LoadReservations()
        {
            using (var context = new HotelManagementContext())
            {
                Reservations = new ObservableCollection<Reservation>(context.Reservations.ToList());
            }
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
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
