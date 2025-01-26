using HotelManagement.ViewModels;
using System.Windows.Controls;

namespace HotelManagement.Views
{
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
            this.DataContext = new UsersViewModel();
        }

        
    }
}
