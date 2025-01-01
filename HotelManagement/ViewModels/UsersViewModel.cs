using HotelManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        public ICommand DeleteUserCommand { get; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role? Role { get; set; }

        public IEnumerable<Role> Roles => Enum.GetValues(typeof(Role)).Cast<Role>();


        public UsersViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser, CanAddUser);

            DeleteUserCommand = new RelayCommand(DeleteUser, CanDeleteUser);
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new HotelManagementContext())
            {
                Users = new ObservableCollection<User>(context.Users.ToList());
            }
        }

        private bool CanAddUser(object parameter)
        {
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
                var newUser = new User
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Username = this.Username,
                    PasswordHash = this.PasswordHash,
                    Role = this.Role
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                Users.Add(newUser);

                FirstName = string.Empty;
                LastName = string.Empty;
                Username = string.Empty;
                PasswordHash = string.Empty;
                Role = null;

                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(PasswordHash));
                OnPropertyChanged(nameof(Role));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DeleteUser(object parameter)
        {
            var userToDelete = parameter as User;
            if (userToDelete == null) return;

            using (var context = new HotelManagementContext())
            {
                context.Users.Remove(userToDelete);
                context.SaveChanges();
            }

            Users.Remove(userToDelete);
        }

        private bool CanDeleteUser(object parameter)
        {
            return parameter is User;  // Ensure that there is a valid user to delete
        }
    }
    
}
