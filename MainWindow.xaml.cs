using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace takojsnje_sporocanje{
    public partial class MainWindow : Window{
        ObservableCollection<Contact> contacts = new();

        public MainWindow(){
            InitializeComponent();

            contacts.Add(new("Mama", "mama@email.com", "123456789", "../../../images/user_avatar.png", new(), DateTime.Now));
            contacts.Add(new("Ata", "ata@email.com", "987654321", "../../../images/user_avatar.png", new(), DateTime.Now));
            contacts.Add(new("Sestra", "sestra@email.com", "1593574862", "../../../images/user_avatar.png", new(), DateTime.Now));

            stiki.ItemsSource = contacts;
        }

        private void ExitApp(object sender, RoutedEventArgs e){
            Application.Current.Shutdown();
        }

        private void stiki_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e){
            var item = (ListView) sender;
            if(item != null && item.SelectedItem != null){

                MessageBox.Show(item.SelectedItem.ToString());
            }
        }
    }
}