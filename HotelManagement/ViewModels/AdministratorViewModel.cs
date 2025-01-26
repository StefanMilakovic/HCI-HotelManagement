using HotelManagement.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;


namespace HotelManagement.ViewModels
{
    public class AdministratorViewModel : INotifyPropertyChanged
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        private string _languageImage;

        public string LanguageImage
        {
            get => _languageImage;
            set
            {
                _languageImage = value;
                OnPropertyChanged();
            }
        }

        public void LoadLanguageImage(string language)
        {
            if (language == "SR")
            {
                LanguageImage = "../Resources/sr.png";
            }
            else
            {
                LanguageImage = "../Resources/en.png";
            }
        }

        public ICommand ShowUsersViewCommand { get; }
        public ICommand ShowRoomsViewCommand { get; }
        public ICommand ShowServicesViewCommand { get; }
        public ICommand ShowReportsViewCommand { get; }
        public ICommand ShowGuestsViewCommand { get; }
        public ICommand LogOutCommand { get; }
        public ICommand CloseCommand { get; }

        public AdministratorViewModel()
        {
            LoadLanguageImage(Properties.Settings.Default.Language);
            ShowUsersViewCommand = new RelayCommand(o => CurrentView = new UsersViewModel());
            ShowRoomsViewCommand = new RelayCommand(o => CurrentView = new RoomsViewModel());
            ShowServicesViewCommand = new RelayCommand(o => CurrentView = new ServicesViewModel());
            ShowReportsViewCommand = new RelayCommand(o => CurrentView = new ReportsViewModel());
            ShowGuestsViewCommand = new RelayCommand(o => CurrentView = new GuestsView());
            LogOutCommand = new RelayCommand(LogOut);

            CloseCommand = new RelayCommand(o => Application.Current.Shutdown());

            CurrentView = new UsersViewModel();

        }

        private void LogOut(object parameter)
        {
            var logInView = new LoginView();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = logInView;
            logInView.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
