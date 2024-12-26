﻿using MaterialDesignThemes.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            // Dobavljanje trenutne teme
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        { // Preuzimanje vrednosti iz TextBox i PasswordBox
          string username = usernameTextBox.Text; // Pretpostavljam da je ime TextBox-a "usernameTextBox"
          string password = passwordBox.Password; // Pretpostavljam da je ime PasswordBox-a "passwordBox" 
          // Ispisivanje vrednosti u konzolu


          Console.WriteLine($"Username: {username}, Password: {password}"); }
        }
}