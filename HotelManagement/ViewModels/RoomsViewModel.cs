using HotelManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    public class RoomsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Room> _rooms;
        public ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddRoomCommand { get; }
        public ICommand DeleteRoomCommand { get; }

        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public RoomType? RoomType { get; set; }

        public IEnumerable<RoomType> RoomTypes => Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
        public IEnumerable<int> Floors { get; } = Enumerable.Range(1, 10);

        public RoomsViewModel()
        {
            AddRoomCommand = new RelayCommand(AddRoom, CanAddRoom);

            DeleteRoomCommand = new RelayCommand(DeleteRoom, CanDeleteRoom);
            LoadRooms();
        }

        private void LoadRooms()
        {
            using (var context = new HotelManagementContext())
            {
                Rooms = new ObservableCollection<Room>(context.Rooms.ToList());
            }
        }

        private bool CanAddRoom(object parameter)
        {
            return RoomNumber > 0 &&
                   Floor > 0 &&
                   RoomType.HasValue;
        }

        private void AddRoom(object parameter)
        {
            using (var context = new HotelManagementContext())
            {
                var newRoom = new Room
                {
                    RoomNumber = this.RoomNumber,
                    Floor = this.Floor,
                    RoomType = this.RoomType.Value
                };

                context.Rooms.Add(newRoom);
                context.SaveChanges();

                Rooms.Add(newRoom);

                RoomNumber = 0;
                Floor = 0;
                RoomType = null;

                OnPropertyChanged(nameof(RoomNumber));
                OnPropertyChanged(nameof(Floor));
                OnPropertyChanged(nameof(RoomType));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DeleteRoom(object parameter)
        {
            var roomToDelete = parameter as Room;
            if (roomToDelete == null) return;

            using (var context = new HotelManagementContext())
            {
                context.Rooms.Remove(roomToDelete);
                context.SaveChanges();
            }

            Rooms.Remove(roomToDelete);
        }

        private bool CanDeleteRoom(object parameter)
        {
            return parameter is Room;  // Ensure that there is a valid room to delete
        }
    }
}