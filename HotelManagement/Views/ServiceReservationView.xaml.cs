using HotelManagement.ViewModels;
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
    }
}
