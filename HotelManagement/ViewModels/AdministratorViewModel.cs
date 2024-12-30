using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModels
{
    /*
    public class AdministratorViewModel : INotifyPropertyChanged
    {
        private string _selectedView = "Users"; // Default view

        public string SelectedView
        {
            get => _selectedView;
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        public ICommand ShowUsersViewCommand { get; }
        public ICommand ShowRoomsViewCommand { get; }
        public ICommand ShowItemsViewCommand { get; }
        public ICommand ShowReportsViewCommand { get; }

        public AdministratorViewModel()
        {
            ShowUsersViewCommand = new RelayCommand(_ => SelectedView = "Users");
            ShowRoomsViewCommand = new RelayCommand(_ => SelectedView = "Rooms");
            ShowItemsViewCommand = new RelayCommand(_ => SelectedView = "Items");
            ShowReportsViewCommand = new RelayCommand(_ => SelectedView = "Reports");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    */
}
