using HotelManagement.ViewModels;
using System.Windows.Controls;

namespace HotelManagement.Views
{
    public partial class InvoicesView : UserControl
    {
        public InvoicesView()
        {
            InitializeComponent();
            this.DataContext = new InvoicesViewModel();
        }
    }
}
