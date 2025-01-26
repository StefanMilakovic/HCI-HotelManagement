using HotelManagement.ViewModels;
using System.Windows.Controls;

namespace HotelManagement.Views
{
    public partial class GuestsView : UserControl
    {
        public GuestsView()
        {
            InitializeComponent();
            this.DataContext = new GuestsViewModel();
        }
    }
}
