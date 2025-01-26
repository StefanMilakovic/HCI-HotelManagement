using System.Windows.Controls;

namespace HotelManagement.Views
{

    public partial class ReservationView : UserControl
    {
        public ReservationView()
        {
            InitializeComponent();
            DataContext = new ReservationsViewModel();
        }
    }
}
