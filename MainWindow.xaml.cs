using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace takojsnje_sporocanje{
    public partial class MainWindow : Window{

        public MainWindow(){
            InitializeComponent();

            var vm = new ViewModel();
            WND.DataContext = vm;
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