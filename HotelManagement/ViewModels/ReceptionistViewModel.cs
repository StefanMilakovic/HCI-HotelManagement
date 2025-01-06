using HotelManagement.Models;
using HotelManagement.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace HotelManagement.ViewModels
{
    internal class ReceptionistViewModel : INotifyPropertyChanged
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

        public ICommand ShowNewReservationCommand { get; }
        public ICommand ShowGuestsViewCommand { get; }
        public ICommand ShowServiceReservationCommand { get; }
        public ICommand ShowInvoicesViewCommand { get; }
        public ICommand ShowCheckInCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand LogOutCommand { get; }

        public ReceptionistViewModel()
        {
            ShowGuestsViewCommand = new RelayCommand(o => CurrentView = new GuestsView());
            ShowNewReservationCommand = new RelayCommand(o => CurrentView = new NewReservationView());
            ShowServiceReservationCommand = new RelayCommand(o => CurrentView = new ServiceReservationView());
            ShowInvoicesViewCommand = new RelayCommand(o => CurrentView = new InvoicesView());
            ShowCheckInCommand = new RelayCommand(o => CurrentView = new CheckInView());

            LogOutCommand = new RelayCommand(LogOut);
            CloseCommand = new RelayCommand(o => Application.Current.Shutdown());

            CurrentView = new GuestsView();
        }

        private void LogOut(object parameter)
        {
            var logInView = new LoginView();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = logInView;
            logInView.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
