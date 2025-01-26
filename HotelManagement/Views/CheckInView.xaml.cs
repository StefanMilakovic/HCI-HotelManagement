using HotelManagement.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace HotelManagement.Views
{
    public partial class CheckInView : UserControl
    {
        public CheckInView()
        {
            InitializeComponent();
            this.DataContext = new CheckInViewModel();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }
    }
}
