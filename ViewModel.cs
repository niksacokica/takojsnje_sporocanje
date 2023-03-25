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

        public ViewModel(){
            AddContact = new Command(obj => { Contacts.Add(new("Stik" + Contacts.Count, "stik" + Contacts.Count + "@email.com", rnd.Next().ToString(), "../../../images/user_avatar.png", new(), DateTime.Now)); });
            RemoveContact = new Command(obj => { if (CurrentContact != null) Contacts.Remove(CurrentContact); else MessageBox.Show("Stik ni izbran!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); });
            EditContact = new Command(obj => {
                if (CurrentContact != null){
                    int ind = Contacts.IndexOf(CurrentContact);
                    //Contacts[ind].Name = "Stik" + rnd.Next();
                    Contacts[ind] = new Contact("Stik" + rnd.Next(), CurrentContact.Email, CurrentContact.PhoneNumber, CurrentContact.Avatar, CurrentContact.Conversation, CurrentContact.LastMessage);
                    CurrentContact = Contacts[ind];

                    //CurrentContact.Name = "Stik" + rnd.Next();
                }else
                    MessageBox.Show("Stik ni izbran!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            });
            ExitApp = new Command(obj => { Application.Current.Shutdown(); });
            
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