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
        private bool _isAddServiceButtonEnabled;
        private ObservableCollection<int> _quantityList;
        private int _selectedQuantity;



        public ObservableCollection<int> QuantityList
        {
            get => _quantityList;
            set
            {
                _quantityList = value;
                OnPropertyChanged(nameof(QuantityList));
            }
        }

        public int SelectedQuantity
        {
            get => _selectedQuantity;
            set
            {
                _selectedQuantity = value;
                OnPropertyChanged(nameof(SelectedQuantity));
            }
        }

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

        public bool IsAddServiceButtonEnabled
        {
            get => _isAddServiceButtonEnabled;
            set
            {
                _isAddServiceButtonEnabled = value;
                OnPropertyChanged(nameof(IsAddServiceButtonEnabled));
            }
        }

        public ICommand SelectReservationCommand { get; }
        public ICommand SelectServiceCommand { get; }
        public ICommand AddServiceCommand { get; }


        public ServiceReservationViewModel()
        {
            SelectReservationCommand = new RelayCommand<Reservation>(SelectReservation);
            SelectServiceCommand = new RelayCommand<Service>(SelectService);
            AddServiceCommand = new RelayCommand(AddService);


            QuantityList = new ObservableCollection<int> {1,2,3,4,5,6};
            SelectedQuantity = 1; // Postavi podrazumijevanu vrijednost


            LoadReservations();
            LoadServices();
        }

        private void AddService(object parameter)
        {
            if (SelectedReservation == null || SelectedService == null || SelectedQuantity <= 0)
                return;

            using (var context = new HotelManagementContext())
            {
                var reservationHasService = new ReservationHasService
                {
                    ReservationId = SelectedReservation.ReservationID,
                    ServiceId = SelectedService.ServiceId,
                    Quantity = SelectedQuantity,

                };

                context.Add(reservationHasService);
                context.SaveChanges();
            }

            System.Windows.MessageBox.Show("Service added to reservation successfully!", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private void SelectReservation(Reservation reservation)
        {
            SelectedReservation = reservation;
        }

        /*
        private void LoadReservations()
        {
            using (var context = new HotelManagementContext())
            {
                Reservations = new ObservableCollection<Reservation>(context.Reservations.ToList());
            }
        }*/


        private void LoadReservations()
        {
            using (var context = new HotelManagementContext())
            {
                // Dohvati sve račune (Invoices) iz baze
                var invoices = context.Invoices.ToList(); // Dohvatanje svih računa u memoriju

                // Dohvati sve rezervacije (Reservations) iz baze
                var reservations = context.Reservations.ToList(); // Dohvatanje svih rezervacija u memoriju

                // Filtriraj rezervacije koje nemaju povezane račune
                var filteredReservations = reservations
                    .Where(reservation => !invoices.Any(invoice => invoice.ReservationID == reservation.ReservationID))
                    .ToList(); // Filtriranje u memoriji

                // Postavi filtrirane rezervacije u ObservableCollection
                Reservations = new ObservableCollection<Reservation>(filteredReservations);
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
                IsAddServiceButtonEnabled = _selectedService != null;
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
