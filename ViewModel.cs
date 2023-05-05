using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace takojsnje_sporocanje{
    public class ViewModel : INotifyPropertyChanged{
        private Random rnd = new();

        public ObservableCollection<Contact> Contacts { get; private set; } = new ObservableCollection<Contact>();
        private Contact? currentContact = null;

        public ICommand AddContact { get; private set; }
        public ICommand RemoveContact { get; private set; }
        public ICommand EditContact { get; private set; }
        public ICommand ExitApp { get; private set; }
        public ICommand OpenSettings { get; private set; }
        public ICommand LoadContacts { get; private set; }
        public ICommand ExportContacts { get; private set; }

        public ViewModel(){
            AddContact = new Command(obj => { Contacts.Add(new("Stik" + Contacts.Count, "stik" + Contacts.Count + "@email.com", rnd.Next().ToString(), "../../../images/user_avatar.png", new(), DateTime.Now)); });
            RemoveContact = new Command(obj => { if (CurrentContact != null) Contacts.Remove(CurrentContact); else MessageBox.Show("Stik ni izbran!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); });
            EditContact = new Command(obj => {
                if (CurrentContact != null)
                    CurrentContact.Name = "Stik" + rnd.Next();
                else
                    MessageBox.Show("Stik ni izbran!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            });
            ExitApp = new Command(obj => { Application.Current.Shutdown(); });
            OpenSettings = new Command(obj => { var settings = new SettingsWindow(); settings.ShowDialog(); });
            LoadContacts = new Command(obj => {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Json files (*.json)|*.json";
                openFile.ShowDialog();
            });
            ExportContacts = new Command(obj => {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Json files (*.json)|*.json";
                saveFile.ShowDialog();
            });

            for ( int i=0; i<3; i++ )
                Contacts.Add(new("Stik" + Contacts.Count, "stik" + Contacts.Count + "@email.com",  rnd.Next().ToString(), "../../../images/user_avatar.png", new(), DateTime.Now));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Contact? CurrentContact{
            get { return currentContact; }
            set{
                currentContact = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentContact"));
            }
        }
    }
}