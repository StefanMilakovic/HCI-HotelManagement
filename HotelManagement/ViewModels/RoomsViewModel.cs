using HotelManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    public class RoomsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Room> _rooms;
        public ObservableCollection<RoomType> _roomTypes;
        private ObservableCollection<Room> _filteredRooms;
        private string _searchText;


        public ObservableCollection<Room> Rooms
        {
            get => _filteredRooms;
            set
            {
                _filteredRooms = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterRooms();
                }
            }
        }

        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddRoomCommand { get; }
        public ICommand DeleteRoomCommand { get; }

        public int RoomNumber { get; set; }
        public int Floor { get; set; }

        public RoomType SelectedRoomType { get; set; }
        public IEnumerable<int> Floors { get; } = Enumerable.Range(1, 10);

        public RoomsViewModel()
        {
            AddRoomCommand = new RelayCommand(AddRoom, CanAddRoom);

            DeleteRoomCommand = new RelayCommand(DeleteRoom, CanDeleteRoom);
            LoadRooms();
            LoadRoomTypes();
        }

        
        private void LoadRooms()
        {
            using (var context = new HotelManagementContext())
            {
                _rooms = new ObservableCollection<Room>(context.Rooms.ToList());
                _filteredRooms = _rooms;
            }
        }

        private void FilterRooms()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                _filteredRooms = _rooms;
            }
            else
            {
                _filteredRooms = new ObservableCollection<Room>(_rooms.Where(r =>
                    r.RoomNumber.ToString().StartsWith(SearchText)));
            }
            OnPropertyChanged(nameof(Rooms));
        }


        private void LoadRoomTypes()
        {
            using (var context = new HotelManagementContext())
            {
                RoomTypes = new ObservableCollection<RoomType>(context.RoomTypes.ToList());
            }
        }


        private bool CanAddRoom(object parameter)
        {
            return RoomNumber > 0 &&
                   Floor > 0 &&
                   SelectedRoomType != null;
        }

        private void AddRoom(object parameter)
        {
            using (var context = new HotelManagementContext())
            {
                var newRoom = new Room
                {
                    RoomNumber = this.RoomNumber,
                    Floor = this.Floor,
                    RoomTypeId = this.SelectedRoomType.RoomTypeId
                };

                context.Rooms.Add(newRoom);
                context.SaveChanges();

                Rooms.Add(newRoom);

                RoomNumber = 0;
                Floor = 0;
                SelectedRoomType = null;

                OnPropertyChanged(nameof(RoomNumber));
                OnPropertyChanged(nameof(Floor));
                OnPropertyChanged(nameof(SelectedRoomType));

                System.Windows.MessageBox.Show("New room added successfully!", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

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
            return parameter is Room;
        }
    }
}