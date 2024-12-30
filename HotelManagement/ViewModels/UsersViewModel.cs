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

namespace HotelManagement.ViewModels
{
    
    public class UsersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddUserCommand { get; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role? Role { get; set; }

        public UsersViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new HotelManagementContext())
            {
                // Load users from the database
                Users = new ObservableCollection<User>(context.Users.ToList());
            }
        }

        private bool CanAddUser(object parameter)
        {
            // Ensure that all required fields are filled before allowing the user to be added
            return !string.IsNullOrEmpty(FirstName) &&
                   !string.IsNullOrEmpty(LastName) &&
                   !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(PasswordHash) &&
                   Role.HasValue;
        }

        private void AddUser(object parameter)
        {
            using (var context = new HotelManagementContext())
            {
                // Create new User object and populate it
                var newUser = new User
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Username = this.Username,
                    PasswordHash = this.PasswordHash,
                    Role = this.Role
                };

                // Add to the database
                context.Users.Add(newUser);
                context.SaveChanges();

                // Add to the ObservableCollection to reflect changes in the UI
                Users.Add(newUser);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
