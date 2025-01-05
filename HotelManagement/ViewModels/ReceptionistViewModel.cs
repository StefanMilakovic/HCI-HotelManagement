using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HotelManagement.Views;
using HotelManagement.Models;
using System.Collections.ObjectModel;

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
        public ICommand CloseCommand { get; }

        public ReceptionistViewModel()
        {
            ShowGuestsViewCommand = new RelayCommand(o => CurrentView = new GuestsView());
            ShowNewReservationCommand = new RelayCommand(o => CurrentView = new NewReservationView());
            ShowServiceReservationCommand = new RelayCommand(o => CurrentView = new ServiceReservationView());
            ShowInvoicesViewCommand = new RelayCommand(o => CurrentView = new InvoicesView());

            CloseCommand = new RelayCommand(o => Application.Current.Shutdown());

            CurrentView = new GuestsView();
            //Reservations = new ObservableCollection<Reservation>();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
