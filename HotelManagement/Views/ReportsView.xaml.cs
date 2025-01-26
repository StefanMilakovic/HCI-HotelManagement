using HotelManagement.ViewModels;
using System.Windows.Controls;

namespace HotelManagement.Views
{
    public partial class ReportsView : UserControl
    {
        public ReportsView()
        {
            InitializeComponent();
            this.DataContext = new ReportsViewModel();
        }
    }
}
