using HotelManagement.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme;

namespace HotelManagement.Views
{
    public partial class ReceptionistView : Window
    {
        
        public ReceptionistView()
        {
            InitializeComponent();
            DataContext = new ReceptionistViewModel();
            LoadTheme();
            LoadLanguage();
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
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["DarkTheme"];
                SaveTheme("Dark");
            }
            else if (currentTheme == Application.Current.Resources["DarkTheme"])
            {
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["DarkRedTheme"];
                SaveTheme("DarkRedTheme");
            }
            else if (currentTheme == Application.Current.Resources["DarkRedTheme"])
            {
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["OrangeDarkTheme"];
                SaveTheme("OrangeDarkTheme");
            }
            else if (currentTheme == Application.Current.Resources["OrangeDarkTheme"])
            {
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
            else if (theme == "DarkRedTheme")
            {
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["DarkRedTheme"];
            }
            else if (theme == "OrangeDarkTheme")
            {
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["OrangeDarkTheme"];
            }
            else
            {
                Debug.WriteLine("Unknown theme, defaulting to LightTheme.");
                Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)Application.Current.Resources["LightTheme"];
                Properties.Settings.Default.SelectedTheme = "Light";
                Properties.Settings.Default.Save();
            }
        }





        private void LanguageToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var languageDictionary = Application.Current.Resources.MergedDictionaries
        .FirstOrDefault(d => d.Source != null &&
                             (d.Source.OriginalString.Contains("Dictionary-SR.xaml") ||
                              d.Source.OriginalString.Contains("Dictionary-EN.xaml")));

            if (languageDictionary != null && languageDictionary.Source.OriginalString.Contains("Dictionary-SR.xaml"))
            {
                var newDictionary = new ResourceDictionary { Source = new Uri("Resources/Dictionary-EN.xaml", UriKind.Relative) };
                Application.Current.Resources.MergedDictionaries.Remove(languageDictionary);
                Application.Current.Resources.MergedDictionaries.Add(newDictionary);
                SaveLanguage("EN");
                ((ReceptionistViewModel)DataContext).LoadLanguageImage("EN");
            }
            else if (languageDictionary != null && languageDictionary.Source.OriginalString.Contains("Dictionary-EN.xaml"))
            {
                var newDictionary = new ResourceDictionary { Source = new Uri("Resources/Dictionary-SR.xaml", UriKind.Relative) };
                Application.Current.Resources.MergedDictionaries.Remove(languageDictionary);
                Application.Current.Resources.MergedDictionaries.Add(newDictionary);
                SaveLanguage("SR");
                ((ReceptionistViewModel)DataContext).LoadLanguageImage("SR");
            }
        }

        private void SaveLanguage(string language)
        {
            Properties.Settings.Default.Language = language;
            Properties.Settings.Default.Save();
        }


        private void LoadLanguage()
        {
            string language = Properties.Settings.Default.Language;
            Debug.WriteLine($"Loaded language: {language}");

            if (language == "SR")
            {
                var currentDictionary = Application.Current.Resources.MergedDictionaries
                    .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Dictionary-SR.xaml"));

                if (currentDictionary == null)
                {
                    var dictionarySR = new ResourceDictionary { Source = new Uri("Resources/Dictionary-SR.xaml", UriKind.Relative) };
                    Application.Current.Resources.MergedDictionaries.Add(dictionarySR);
                }
            }
            else if (language == "EN")
            {
                var currentDictionary = Application.Current.Resources.MergedDictionaries
                    .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Dictionary-EN.xaml"));

                if (currentDictionary == null)
                {
                    var dictionaryEN = new ResourceDictionary { Source = new Uri("Resources/Dictionary-EN.xaml", UriKind.Relative) };
                    Application.Current.Resources.MergedDictionaries.Add(dictionaryEN);
                }
            }
            else
            {
                Properties.Settings.Default.Language = "EN";
                Properties.Settings.Default.Save();

                var dictionaryEN = new ResourceDictionary { Source = new Uri("Resources/Dictionary-EN.xaml", UriKind.Relative) };
                Application.Current.Resources.MergedDictionaries.Add(dictionaryEN);
            }
        }


        

    }
}

