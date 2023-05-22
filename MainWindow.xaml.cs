using System;
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
        }
    }
}