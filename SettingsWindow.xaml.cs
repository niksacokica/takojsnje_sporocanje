using Microsoft.Win32;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace takojsnje_sporocanje{

    public partial class SettingsWindow : Window{
        private String lastFile;

        public SettingsWindow(){
            InitializeComponent();

            name.Text = Properties.Settings.Default.Name;
            email.Text = Properties.Settings.Default.Email;
            phone.Text = Properties.Settings.Default.Phone;
            sex.SelectedIndex = Properties.Settings.Default.Sex == 'M' ? 0 : 1;
            dob.SelectedDate = Properties.Settings.Default.DOB;
            FileName.Content = Path.GetFileName(Properties.Settings.Default.Avatar);

            lastFile = Properties.Settings.Default.Avatar;
        }

        private void OpenAvatar(object sender, RoutedEventArgs e){
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image files (*.png;*.jpg;*jpeg)|*.png;*.jpg;*.jpeg";

            if(openFile.ShowDialog() == true){
                FileName.Content = openFile.SafeFileName;
                lastFile = openFile.FileName;
            }
        }

        private void OkClick(object sender, RoutedEventArgs e){
            bool ok = true;

            if(name.Text.Length < 3){
                name.Background = Brushes.Red;
                ok = false;
            }else
                name.Background = Brushes.White;

            if(!new EmailAddressAttribute().IsValid(email.Text)){
                email.Background = Brushes.Red;
                ok = false;
            }else
                email.Background = Brushes.White;

            if(dob.SelectedDate > DateTime.Now.AddYears(-15)){
                dob.Background = Brushes.Red;
                ok = false;
            }else
                dob.Background = Brushes.White;

            if(ok){
                Properties.Settings.Default.Name = name.Text;
                Properties.Settings.Default.Email = email.Text;
                Properties.Settings.Default.Phone = phone.Text;
                Properties.Settings.Default.Sex = sex.SelectedIndex == 0 ? 'M' : 'F';
                Properties.Settings.Default.DOB = (DateTime)dob.SelectedDate;
                Properties.Settings.Default.Avatar = lastFile;

                Properties.Settings.Default.Save();
                DialogResult = true;
            }
        }
    }
}
