using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows;

namespace takojsnje_sporocanje{
    public class Contact : INotifyPropertyChanged{
        private string name = "";
        private string email = "";
        private string phone = "";
        private string avatar = "";
        private List<Tuple<string, string>> convo = new();
        private DateTime lastMessage = DateTime.Now;

        public Contact(string name, string email, string phone, string avatar, List<Tuple<string, string>> convo, DateTime lastMessage)
        {
            Name = name;
            Email = email;
            PhoneNumber = phone;
            Avatar = avatar;
            Conversation = convo;
            LastMessage = lastMessage;
        }

        public string Name{
            get { return name; }
            set{
                if(value.Length >= 3)
                    name = value;
                else{
                    name = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    MessageBox.Show("Ime mora vsebovati vsaj 3 znake!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                NotifyPropertyChanged("Name");
            }
        }

        public string Email{
            get { return email; }
            set{
                if(new EmailAddressAttribute().IsValid(value)){
                    email = value;
                    NotifyPropertyChanged("Email");
                }else
                    MessageBox.Show("Napačen e-poštni naslov!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string PhoneNumber{
            get { return phone; }
            set{
                phone = value;
                NotifyPropertyChanged("PhoneNumber");
            }
        }

        public string Avatar{
            get { return avatar; }
            set{
                if(File.Exists(value)){
                    avatar = value;
                    NotifyPropertyChanged("Avatar");
                }else
                    MessageBox.Show("Datoteka ne obstaja!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Tuple<string, string>> Conversation{
            get { return convo; }
            set{
                convo = value;
                NotifyPropertyChanged("Conversation");
            }
        }

        public DateTime LastMessage{
            get { return lastMessage; }
            set{
                lastMessage = value;
                NotifyPropertyChanged("LastMessage");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString(){
            return Name;
        }

        private void NotifyPropertyChanged(string Name){
            if(PropertyChanged != null){
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }
    }
}