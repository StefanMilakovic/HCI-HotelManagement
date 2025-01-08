using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManagement.Views
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class AdministratorView : Window
    {
        public AdministratorView()
        {
            InitializeComponent();
            DataContext = new AdministratorViewModel();
            LoadTheme();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var currentTheme = Application.Current.Resources.MergedDictionaries[0];

            if (currentTheme == Application.Current.Resources["LightTheme"])
            {
                // Prebaci na Dark
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["DarkTheme"];
                SaveTheme("Dark");
            }
            else if (currentTheme == Application.Current.Resources["DarkTheme"])
            {
                // Prebaci na HighContrast
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["DarkRedTheme"];
                SaveTheme("DarkRedTheme");
            }
            else if (currentTheme == Application.Current.Resources["DarkRedTheme"])
            {
                // Prebaci nazad na Light
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["OrangeDarkTheme"];
                SaveTheme("Light");
            }
            else if (currentTheme == Application.Current.Resources["OrangeDarkTheme"])
            {
                // Prebaci nazad na Light
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["LightTheme"];
                SaveTheme("Light");
            }
        }

        private void SaveTheme(string theme)
        {
            Properties.Settings.Default.SelectedTheme = theme;
            Properties.Settings.Default.Save();
        }

        private void LoadTheme()
        {
            string theme = Properties.Settings.Default.SelectedTheme;
            Debug.WriteLine($"Loaded theme: {theme}");

            if (theme == "Dark")
            {
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["DarkTheme"];
            }
            else if (theme == "Light")
            {
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["LightTheme"];
            }
            else 
            {
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["DarkRedTheme"];
            }
        }
    }
}
