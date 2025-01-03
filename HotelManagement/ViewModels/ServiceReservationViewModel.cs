using GalaSoft.MvvmLight.CommandWpf;
using HotelManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    public class ServiceReservationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Reservation> _reservations;
        private Reservation _selectedReservation;
        private Service _selectedService;
        private ObservableCollection<Service> _services;
        private bool _isNextButtonEnabled;

        public ObservableCollection<Reservation> Reservations
        {
            get => _reservations;
            set
            {
                _reservations = value;
                OnPropertyChanged(nameof(Reservations));
            }
        }

        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
                IsNextButtonEnabled = _selectedReservation != null;
            }
        }

        public bool IsNextButtonEnabled
        {
            get => _isNextButtonEnabled;
            set
            {
                _isNextButtonEnabled = value;
                OnPropertyChanged(nameof(IsNextButtonEnabled));
            }
        }

        public ICommand SelectReservationCommand { get; }
        public ICommand SelectServiceCommand { get; }

        public ServiceReservationViewModel()
        {
            SelectReservationCommand = new RelayCommand<Reservation>(SelectReservation);
            SelectServiceCommand = new RelayCommand<Service>(SelectService);
            LoadReservations();
            LoadServices();
        }

        private void SelectReservation(Reservation reservation)
        {
            SelectedReservation = reservation;
        }

        private void LoadReservations()
        {
            using (var context = new HotelManagementContext())
            {
                Reservations = new ObservableCollection<Reservation>(context.Reservations.ToList());
            }
        }

        public ObservableCollection<Service> Services
        {
            get => _services;
            set
            {
                _services = value;
                OnPropertyChanged(nameof(Services));
            }
        }

        public Service SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged(nameof(SelectedService));
                IsNextButtonEnabled = _selectedService != null;
            }
        }

        private void LoadServices()
        {
            using (var context = new HotelManagementContext())
            {
                Services = new ObservableCollection<Service>(context.Services.ToList());
            }
        }

        private void SelectService(Service service)
        {
            SelectedService = service;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}
