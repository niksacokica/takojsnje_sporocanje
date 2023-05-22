using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace takojsnje_sporocanje{
    public partial class EditContact : Window{
        public bool cancel = true;

        public EditContact(){
            InitializeComponent();
        }

        private void OpenAvatar(object sender, RoutedEventArgs e){
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image files (*.png;*.jpg;*jpeg)|*.png;*.jpg;*.jpeg";

            if(openFile.ShowDialog() == true){
                FileName.Content = openFile.SafeFileName;
                ((Contact)DataContext).Avatar = openFile.FileName;
            }
        }

        private void OkClick(object sender, RoutedEventArgs e){
            var contact = (Contact)DataContext;

            if(contact.IsValid()){
                cancel = false;
                Close();
            }
            
            if(contact.Name.Length < 3)
                name.Background = Brushes.Red;
            else
                name.Background = Brushes.White;

            if(!new EmailAddressAttribute().IsValid(contact.Email))
                email.Background = Brushes.Red;
            else
                email.Background = Brushes.White;
        }

        private void Cancel(object sender, RoutedEventArgs e){
            Close();
        }
    }
}
