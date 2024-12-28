using System;
using System.ComponentModel;
using System.Windows.Input;

namespace HotelManagement
{
    public class LoginViewModel
    {
        /*
        // Username i Password - Binding iz View-a
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

        // Komande za Login i promenu teme
        public ICommand LoginCommand { get; }
        public ICommand ThemeToggleCommand { get; }
        public ICommand CloseWindowCommand { get; }

        public LoginViewModel()
        {
            // Kreiranje komandi
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            ThemeToggleCommand = new RelayCommand(ExecuteThemeToggle);
            CloseWindowCommand = new RelayCommand(ExecuteCloseWindow);
        }

        private void ExecuteLogin(object parameter)
        {
            // Login logika
            Console.WriteLine($"Username: {Username}, Password: {Password}");

            // Ovde dodaj validaciju ili proveru autentifikacije
            if (Username == "admin" && Password == "1234")
            {
                Console.WriteLine("Login successful");
                // Možeš ovde otvarati novi prozor ako je potrebno (ne u MVVM direktno, već kroz servis)
            }
            else
            {
                Console.WriteLine("Invalid username or password");
            }
        }

        private bool CanExecuteLogin(object parameter)
        {
            // Omogućava ili onemogućava dugme na osnovu uslova
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void ExecuteThemeToggle(object parameter)
        {
            // Logika za promenu teme (ovo se može povezati sa MaterialDesignThemes API-jem)
            Console.WriteLine("Theme toggled");
        }

        private void ExecuteCloseWindow(object parameter)
        {
            // Zatvaranje prozora
            Application.Current.Shutdown();
        }

        // INotifyPropertyChanged implementacija
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
    }
}
