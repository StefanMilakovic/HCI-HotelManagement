using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    public class ReportsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Reservation> _reservations;
        private ObservableCollection<Invoice> _invoices;

        public ObservableCollection<Reservation> Reservations
        {
            get => _reservations;
            set
            {
                _reservations = value;
                OnPropertyChanged(nameof(Reservations));
            }
        }

        public ObservableCollection<Invoice> Invoices
        {
            get => _invoices;
            set
            {
                _invoices = value;
                OnPropertyChanged(nameof(Invoices));
            }
        }

        public ICommand DeleteReservationCommand { get; }
        public ICommand DeleteInvoiceCommand { get; }

        public ReportsViewModel()
        {
            LoadReservations();
            LoadInvoices();
            DeleteReservationCommand = new RelayCommand(DeleteReservation, CanDeleteReservation);
            DeleteInvoiceCommand = new RelayCommand(DeleteInvoice, CanDeleteInvoice);
        }

        private void DeleteInvoice(object parameter)
        {
            var invoiceToDelete = parameter as Invoice;
            if (invoiceToDelete == null) return;

            using (var context = new HotelManagementContext())
            {
                context.Invoices.Remove(invoiceToDelete);
                context.SaveChanges();
            }

            Invoices.Remove(invoiceToDelete);
        }

        private bool CanDeleteInvoice(object parameter)
        {
            return parameter is Invoice; // Osiguraj da je validan parametar
        }

        private bool CanDeleteReservation(object parameter)
        {
            return parameter is Reservation;
        }

        private void DeleteReservation(object parameter)
        {
            var reservationToDelete = parameter as Reservation;
            if (reservationToDelete == null) return;

            using (var context = new HotelManagementContext())
            {
                context.Reservations.Remove(reservationToDelete);
                context.SaveChanges();
            }

            Reservations.Remove(reservationToDelete);
        }


        private void LoadReservations()
        {
            using (var context = new HotelManagementContext())
            {
                Reservations = new ObservableCollection<Reservation>(context.Reservations.ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadInvoices()
        {
            using (var context = new HotelManagementContext())
            {
                Invoices = new ObservableCollection<Invoice>(context.Invoices.ToList());
            }
        }
        
    }
}
