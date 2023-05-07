using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace takojsnje_sporocanje{
    public class Contact : INotifyPropertyChanged{
        private string name = "";
        private string email = "";
        private string phone = "";
        private string avatar = "";
        private List<Tuple<string, string>> convo = new();
        private DateTime lastMessage = DateTime.Now;
        private string status = "";

        public Contact(string name, string email, string phone, string avatar, List<Tuple<string, string>> convo, DateTime lastMessage, string status){
            Name = name;
            Email = email;
            PhoneNumber = phone;
            Avatar = avatar;
            Conversation = convo;
            LastMessage = lastMessage;
            Status = status;
        }

        public Contact(){
            Avatar = "../../../images/user_avatar.png";
        }

        public string Name{
            get { return name; }
            set{
                name = value;

                NotifyPropertyChanged("Name");
            }
        }

        public string Email{
            get { return email; }
            set{
                email = value;
                NotifyPropertyChanged("Email");
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
                avatar = value;
                NotifyPropertyChanged("Avatar");
            }
        }

        public List<Tuple<string, string>> Conversation{
            get { return convo; }
            set{
                convo = value;
                NotifyPropertyChanged("Conversation");
                NotifyPropertyChanged("GetConversation");
            }
        }

        public DateTime LastMessage{
            get { return lastMessage; }
            set{
                lastMessage = value;
                NotifyPropertyChanged("LastMessage");
            }
        }

        public string Status{
            get { return status; }
            set{
                status = value;
                NotifyPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString(){
            return Name;
        }

        public string GetConversation{
            get { string convo = "";
                foreach(Tuple<string, string> message in Conversation)
                    convo += message.Item1 + ": " + message.Item2 + "\n";

                return convo;
            }
        }

        public Boolean IsValid(){
            return Name.Length > 2 && new EmailAddressAttribute().IsValid(Email) && File.Exists(Avatar);
        }

        private void NotifyPropertyChanged(string Name){
            if(PropertyChanged != null){
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }
    }
}