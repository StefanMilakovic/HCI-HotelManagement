using HotelManagement.ViewModels;
using System.Windows.Controls;

namespace HotelManagement.Views
{
    public partial class RoomsView : UserControl
    {
        public RoomsView()
        {
            InitializeComponent();
            this.DataContext = new RoomsViewModel();
        }
    }
}
