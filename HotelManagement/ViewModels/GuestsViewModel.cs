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
    public class GuestsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Guest> _guests;
        public ObservableCollection<Guest> Guests
        {
            get => _guests;
            set
            {
                _guests = value;
                OnPropertyChanged();
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
                Guests = new ObservableCollection<Guest>(context.Guests.ToList());
            }
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
        }

        private bool CanDeleteGuest(object parameter)
        {
            return parameter is Guest;
        }
    }
    
}
