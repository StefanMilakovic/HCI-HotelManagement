using HotelManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    public class GuestsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Guest> _guests;
        private ObservableCollection<Guest> _filteredGuests;
        private string _searchText;



        public ObservableCollection<Guest> Guests
        {
            get => _filteredGuests;
            set
            {
                _filteredGuests = value;
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
                    FilterGuests();
                }
            }
        }

        public ICommand AddGuestCommand { get; }
        public ICommand DeleteGuestCommand { get; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassportNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public GuestsViewModel()
        {
            AddGuestCommand = new RelayCommand(AddGuest, CanAddGuest);
            DeleteGuestCommand = new RelayCommand(DeleteGuest, CanDeleteGuest);
            LoadGuests();
        }

        private void LoadGuests()
        {
            using (var context = new HotelManagementContext())
            {
                _guests = new ObservableCollection<Guest>(context.Guests.ToList());
                _filteredGuests = _guests;
            }
        }


        private void FilterGuests()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                _filteredGuests = _guests;
            }
            else
            {
                _filteredGuests = new ObservableCollection<Guest>(_guests.Where(g =>
            g.PassportNumber.StartsWith(SearchText, StringComparison.InvariantCultureIgnoreCase)));
            }
            OnPropertyChanged(nameof(Guests));
        }


        private bool CanAddGuest(object parameter)
        {
            return !string.IsNullOrEmpty(FirstName) &&
                   !string.IsNullOrEmpty(LastName) &&
                   !string.IsNullOrEmpty(PassportNumber) &&
                   !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(PhoneNumber);
        }

        private void AddGuest(object parameter)
        {
            using (var context = new HotelManagementContext())
            {
                var newGuest = new Guest
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    PassportNumber = this.PassportNumber,
                    Email = this.Email,
                    PhoneNumber = this.PhoneNumber
                };

                context.Guests.Add(newGuest);
                context.SaveChanges();

                Guests.Add(newGuest);
                //novo
                FilterGuests();

                FirstName = string.Empty;
                LastName = string.Empty;
                PassportNumber = string.Empty;
                Email = string.Empty;
                PhoneNumber = string.Empty;

                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(PassportNumber));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(PhoneNumber));

                System.Windows.MessageBox.Show("New guest added successfully!", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DeleteGuest(object parameter)
        {
            var guestToDelete = parameter as Guest;
            if (guestToDelete == null) return;

            using (var context = new HotelManagementContext())
            {
                context.Guests.Remove(guestToDelete);
                context.SaveChanges();
            }

            Guests.Remove(guestToDelete);
            //novo
            FilterGuests();
        }

        private bool CanDeleteGuest(object parameter)
        {
            return parameter is Guest;
        }
    }
    
}
