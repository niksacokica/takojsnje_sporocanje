using System.Windows;

namespace takojsnje_sporocanje{
    public partial class MainWindow : Window{
        public MainWindow(){
            InitializeComponent();
        }

        private void ExitApp(object sender, RoutedEventArgs e){
            Application.Current.Shutdown();
        }
    }
}