using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace takojsnje_sporocanje{
    public partial class MainWindow : Window{
        private ViewModel vm;

        public MainWindow(){
            InitializeComponent();

            vm = new ViewModel();
            WND.DataContext = vm;

            Binding name = new("Name"){
                Source = Properties.Settings.Default
            };
            username.SetBinding(Label.ContentProperty, name);

            Binding avatar = new("Avatar"){
                Source = Properties.Settings.Default
            };
            useravatar.SetBinding(Image.SourceProperty, avatar);
        }

        private void stiki_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e){
            var item = (ListView) sender;
            if(item != null && item.SelectedItem != null){

                MessageBox.Show(item.SelectedItem.ToString());
            }
        }

        private void ChatBox_OnNewMessage(object sender, string newMessage){
            vm.OnNewMessage(newMessage);
        }
    }
}