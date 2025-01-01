using HotelManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    public class ServicesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Service> _services;
        public ObservableCollection<Service> Services
        {
            get => _services;
            set
            {
                _services = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddServiceCommand { get; }
        public ICommand DeleteServiceCommand { get; }

        public string Name { get; set; }
        public double Price { get; set; }

   

        public ServicesViewModel()
        {
            AddServiceCommand = new RelayCommand(AddService, CanAddService);
            DeleteServiceCommand = new RelayCommand(DeleteService, CanDeleteService);
            LoadServices();
        }

        private void LoadServices()
        {
            using (var context = new HotelManagementContext())
            {
                Services = new ObservableCollection<Service>(context.Services.ToList());
            }
        }

        private bool CanAddService(object parameter)
        {
            return !string.IsNullOrEmpty(Name) && Price > 0;
        }

        private void AddService(object parameter)
        {
            using (var context = new HotelManagementContext())
            {
                var newService = new Service
                {
                    Name = this.Name,
                    Price = this.Price
                };

                context.Services.Add(newService);
                context.SaveChanges();

                Services.Add(newService);

                Name = string.Empty;
                Price = 0;

                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Price));
            }
        }

        private void DeleteService(object parameter)
        {
            var serviceToDelete = parameter as Service;
            if (serviceToDelete == null) return;

            using (var context = new HotelManagementContext())
            {
                context.Services.Remove(serviceToDelete);
                context.SaveChanges();
            }

            Services.Remove(serviceToDelete);
        }

        private bool CanDeleteService(object parameter)
        {
            return parameter is Service;  // Ensure that there is a valid service to delete
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
