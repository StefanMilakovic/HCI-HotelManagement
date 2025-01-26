using HotelManagement.ViewModels;
using System.Windows.Controls;

namespace HotelManagement.Views
{

    public partial class NewReservationView : UserControl
    {
        public NewReservationView()
        {
            InitializeComponent();
            this.DataContext = new NewReservationViewModel();
        }
    }
}
