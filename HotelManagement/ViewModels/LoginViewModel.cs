using HotelManagement.Models;
using HotelManagement.Utilities;
using HotelManagement.Views;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace HotelManagement
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
     
        public ICommand LoginCommand { get; }
        public ICommand CloseCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            CloseCommand = new RelayCommand(ExecuteClose);
        }

        private async void ExecuteLogin(object parameter)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == Username);
                    
                    if (user == null || !VerifyPassword(Password, user.PasswordHash))
                    {
                        MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    CurrentUser.UserID = user.UserId;
                    CurrentUser.Username = user.Username;

                    switch (user.Role)
                    {
                        case Role.Administrator:
                            OpenAdministratorView();
                            break;

                        case Role.Receptionist:
                            OpenReceptionistView();
                            break;

                        default:
                            MessageBox.Show("Role not recognized.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            string hashedEnteredPassword = PasswordHelper.HashPassword(password);
            return hashedEnteredPassword == passwordHash;
        }

        private void OpenAdministratorView()
        {
            var adminView = new AdministratorView();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = adminView;
            adminView.Show();
        }

        private void OpenReceptionistView()
        {
            var receptionistView = new ReceptionistView();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = receptionistView;
            receptionistView.Show();
        }

        private void ExecuteClose(object parameter)
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
