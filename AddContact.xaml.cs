using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace takojsnje_sporocanje{
    public partial class AddContact : Window{
        private Contact contact = new();
        public Contact Contact { get { return contact; } }

        public AddContact(){
            InitializeComponent();

            DataContext = contact;
        }

        private void OpenAvatar(object sender, RoutedEventArgs e){
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image files (*.png;*.jpg;*jpeg)|*.png;*.jpg;*.jpeg";

            if(openFile.ShowDialog() == true){
                FileName.Content = openFile.SafeFileName;
                contact.Avatar = openFile.FileName;
                Avatar.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(openFile.FileName);
            }
        }

        private void OkClick(object sender, RoutedEventArgs e){
            if(Contact.IsValid())
                DialogResult = true;
            
            if(Contact.Name.Length < 3)
                name.Background = Brushes.Red;
            else
                name.Background = Brushes.White;

            if(!new EmailAddressAttribute().IsValid(Contact.Email))
                email.Background = Brushes.Red;
            else
                email.Background = Brushes.White;
        }
    }
}
