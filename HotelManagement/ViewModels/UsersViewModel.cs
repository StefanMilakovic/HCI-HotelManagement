using HotelManagement.Models;
using HotelManagement.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    public class UsersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _users;
        private ObservableCollection<User> _filteredUsers;
        private string _searchText;
        public ObservableCollection<User> Users
        {
            get => _filteredUsers;
            set
            {
                _filteredUsers = value;
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
                    FilterUsers();
                }
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

        private void FilterUsers()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                _filteredUsers = _users;
            }
            else
            {
                _filteredUsers = new ObservableCollection<User>(_users.Where(u =>
                    u.FirstName.StartsWith(SearchText, StringComparison.InvariantCultureIgnoreCase) ||
                    u.LastName.StartsWith(SearchText, StringComparison.InvariantCultureIgnoreCase) ||
                    u.Username.StartsWith(SearchText, StringComparison.InvariantCultureIgnoreCase)));
            }
            OnPropertyChanged(nameof(Users));
        }

        private void LoadUsers()
        {
            using (var context = new HotelManagementContext())
            {
                _users = new ObservableCollection<User>(context.Users.ToList());
                _filteredUsers = _users;
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
                string hashedPassword = PasswordHelper.HashPassword(this.PasswordHash);

                var newUser = new User
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Username = this.Username,
                    PasswordHash = hashedPassword,
                    Role = this.Role
                };

                context.Users.Add(newUser);
                context.SaveChanges();
                Users.Add(newUser);

                FilterUsers();

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
                System.Windows.MessageBox.Show("New user added successfully!", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

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
            FilterUsers();
        }

        private bool CanDeleteUser(object parameter)
        {
            return parameter is User;
        }
    }
    
}
