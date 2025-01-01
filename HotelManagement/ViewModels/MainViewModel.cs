
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;


namespace HotelManagement.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
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

        public ICommand ShowUsersViewCommand { get; }
        public ICommand ShowRoomsViewCommand { get; }
        public ICommand ShowItemsViewCommand { get; }
        public ICommand ShowReportsViewCommand { get; }
        public ICommand CloseCommand { get; }

        public MainViewModel()
        {
            ShowUsersViewCommand = new RelayCommand(o => CurrentView = new UsersViewModel());
            ShowRoomsViewCommand = new RelayCommand(o => CurrentView = new RoomsViewModel());
            ShowItemsViewCommand = new RelayCommand(o => CurrentView = new ServicesViewModel());
            ShowReportsViewCommand = new RelayCommand(o => CurrentView = new ReportsViewModel());

            CloseCommand = new RelayCommand(o => Application.Current.Shutdown());

            // Default view
            CurrentView = new UsersViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
