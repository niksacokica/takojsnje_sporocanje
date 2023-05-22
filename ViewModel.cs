using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Windows.UI.Xaml.Controls;

namespace takojsnje_sporocanje{
    public class ViewModel : INotifyPropertyChanged{
        private Random rnd = new();
        private DispatcherTimer dt = new DispatcherTimer();

        public ObservableCollection<Contact> Contacts { get; private set; } = new ObservableCollection<Contact>();
        private Contact? currentContact = null;
        private FrameworkElement uc = null;

        public ICommand AddContact { get; private set; }
        public ICommand RemoveContact { get; private set; }
        public ICommand EditContact { get; private set; }
        public ICommand ExitApp { get; private set; }
        public ICommand OpenSettings { get; private set; }
        public ICommand LoadContacts { get; private set; }
        public ICommand ExportContacts { get; private set; }
        public ICommand AltView { get; private set; }
        public ICommand OgView { get; private set; }

        private EditContact? editContact = null;
        public ViewModel(){
            AddContact = new Command(obj => {
                var newContact = new AddContact();
                if(newContact.ShowDialog() == true)
                    Contacts.Add(newContact.Contact);
            });
            RemoveContact = new Command(obj => { if (CurrentContact != null) Contacts.Remove(CurrentContact); else MessageBox.Show("Stik ni izbran!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); });
            EditContact = new Command(obj => {
                if(editContact != null)
                    editContact.Close();

                var curContact = new Contact(currentContact.Name, currentContact.Email, currentContact.PhoneNumber, currentContact.Avatar, currentContact.Conversation, currentContact.LastMessage, currentContact.Status);
                editContact = new EditContact{
                    Owner = App.Current.MainWindow,
                    DataContext = curContact
                };

                editContact.Closed += (sender, EventArgs) => {
                    if(!editContact.cancel){
                        Contact cont = (Contact)editContact.DataContext;

                        int ind = Contacts.IndexOf(CurrentContact);
                        Contacts[ind] = new Contact(cont.Name, cont.Email, cont.PhoneNumber, cont.Avatar, cont.Conversation, cont.LastMessage, cont.Status);
                        CurrentContact = Contacts[ind];
                    }
                };
                editContact.Show();

            });
            ExitApp = new Command(obj => { Application.Current.Shutdown(); });
            OpenSettings = new Command(obj => { var settings = new SettingsWindow(); settings.ShowDialog(); dt.Interval = Properties.Settings.Default.PeriodicalSaveTimeSpan; });
            LoadContacts = new Command(obj => {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Json files (*.json)|*.json";
                openFile.ShowDialog();

                string jsonString = loadCurrentState(openFile.FileName);
                ObservableCollection<Contact> contacts = JsonSerializer.Deserialize<ObservableCollection<Contact>>(jsonString);
                if(contacts != null)
                    Contacts = contacts;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contacts"));
            });
            ExportContacts = new Command(obj => {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Json files (*.json)|*.json";
                saveFile.ShowDialog();

                saveCurrentState(saveFile.FileName);
            });

            AltView = new Command(obj => {
                UC = new NewView();
            });

            OgView = new Command(obj => {
                UC = new OgView();
            });

            string[] status = new string[] { "online", "Away", "Busy", "Unknown", "Offline" };
            for (int i=0; i<3; i++)
                Contacts.Add(new("Stik" + Contacts.Count, "stik" + Contacts.Count + "@email.com",  rnd.Next().ToString(), "../../../images/user_avatar.png", new(), DateTime.Now, status[rnd.Next(0, status.Length)]));

            string jsonString = loadCurrentState("./temp.json");
            ObservableCollection<Contact> contacts = JsonSerializer.Deserialize<ObservableCollection<Contact>>(jsonString);
            if (contacts != null)
                Contacts = contacts;

            dt.Interval = Properties.Settings.Default.PeriodicalSaveTimeSpan;
            dt.Tick += (object? sender, EventArgs e) => {
                if (!Properties.Settings.Default.PeriodicalSave)
                    return;

                saveCurrentState("./temp.json");
            };
            dt.Start();

            UC = new OgView();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Contact? CurrentContact{
            get { return currentContact; }
            set{
                if(value == null)
                    return;

                currentContact = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentContact"));

                var curContact = new Contact(value.Name, value.Email, value.PhoneNumber, value.Avatar, value.Conversation, value.LastMessage, value.Status);
                if(editContact != null)
                    editContact.DataContext = curContact;
            }
        }

        public FrameworkElement? UC{
            get { return uc; }
            set { uc = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UC")); }
        }

        private string[] messages = new string[] { "Zdravo!", "Kako si?", "Kako ti lahko pomagam?", "Pozdravljen.", "Dober dan." };
        public void OnNewMessage(string newMessage){
            var msg1 = CurrentContact.Conversation;
            msg1.Add(new Tuple<string, string>("You", newMessage));
            CurrentContact.Conversation = msg1;

            var msg2 = CurrentContact.Conversation;
            msg2.Add(new Tuple<string, string>("Them", messages[rnd.Next(0, messages.Length)]));
            CurrentContact.Conversation = msg2;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentContact"));
        }

        public void saveCurrentState(string fileName) =>
            File.WriteAllText(fileName, JsonSerializer.Serialize(Contacts, new JsonSerializerOptions { WriteIndented = true }));

        public string loadCurrentState(string fileName) =>
            File.ReadAllText(fileName);
    }
}