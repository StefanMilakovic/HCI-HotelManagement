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
    public class InvoicesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Invoice> _invoices;
        public ObservableCollection<Invoice> Invoices
        {
            get => _invoices;
            set
            {
                _invoices = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddInvoiceCommand { get; }
        public ICommand DeleteInvoiceCommand { get; }

        public int InvoiceID { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int GuestID { get; set; }
        public int ReservationID { get; set; }

        public InvoicesViewModel()
        {
            AddInvoiceCommand = new RelayCommand(AddInvoice, CanAddInvoice);
            DeleteInvoiceCommand = new RelayCommand(DeleteInvoice, CanDeleteInvoice);

            LoadInvoices();
        }

        private void LoadInvoices()
        {
            using (var context = new HotelManagementContext())
            {
                Invoices = new ObservableCollection<Invoice>(context.Invoices.ToList());
            }
        }

        private bool CanAddInvoice(object parameter)
        {
            return Date != default &&
                   TotalAmount > 0 &&
                   GuestID > 0 &&
                   ReservationID > 0; // Validacija unosa
        }

        private void AddInvoice(object parameter)
        {
            using (var context = new HotelManagementContext())
            {
                var newInvoice = new Invoice
                {
                    IssuedDate = this.Date,
                    TotalAmount = this.TotalAmount,
                    GuestID = this.GuestID,
                    ReservationID = this.ReservationID
                };

                context.Invoices.Add(newInvoice);
                context.SaveChanges();

                Invoices.Add(newInvoice);

                // Reset unosa
                Date = default;
                TotalAmount = 0;
                GuestID = 0;
                ReservationID = 0;

                OnPropertyChanged(nameof(Date));
                OnPropertyChanged(nameof(TotalAmount));
                OnPropertyChanged(nameof(GuestID));
                OnPropertyChanged(nameof(ReservationID));
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
