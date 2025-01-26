using System.ComponentModel;
using System.Windows;

namespace HotelManagement.Languages
{
    public class LanguageManager : INotifyPropertyChanged
    {
        private static LanguageManager _instance;
        public static LanguageManager Instance => _instance ??= new LanguageManager();

        private ResourceDictionary _currentDictionary;
        public ResourceDictionary CurrentDictionary
        {
            get => _currentDictionary;
            set
            {
                _currentDictionary = value;
                OnPropertyChanged(nameof(CurrentDictionary));
                OnPropertyChanged(nameof(GuestIdHeader));
                OnPropertyChanged(nameof(FirstNameHeader));
                // Dodajte sva svojstva za Header-e.
            }
        }

        public string GuestIdHeader => (string)CurrentDictionary["GuestIdHeader"];
        public string FirstNameHeader => (string)CurrentDictionary["FirstNameHeader"];
        public string LastNameHeader => (string)CurrentDictionary["LastNameHeader"];
        public string PassportNumberHeader => (string)CurrentDictionary["PassportNumberHeader"];
        public string EmailHeader => (string)CurrentDictionary["EmailHeader"];
        public string PhoneNumberHeader => (string)CurrentDictionary["PhoneNumberHeader"];







        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private LanguageManager()
        {
            LoadLanguage("SR"); // Podrazumevani jezik
        }

        public void LoadLanguage(string language)
        {
            var dictionaryPath = language == "SR"
                ? "Resources/Dictionary-SR.xaml"
                : "Resources/Dictionary-EN.xaml";

            CurrentDictionary = new ResourceDictionary { Source = new Uri(dictionaryPath, UriKind.Relative) };
        }
    }

}