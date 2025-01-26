using HotelManagement.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace HotelManagement.Views
{
    public partial class ServiceReservationView : UserControl
    {
        public ServiceReservationView()
        {
            InitializeComponent();
            this.DataContext = new ServiceReservationViewModel();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }
    }
}
