using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace takojsnje_sporocanje{
    public partial class ChatBox : UserControl{
        public ChatBox(){
            InitializeComponent();

            emojiPicker.Selection = null;
        }

        public delegate void NewMessage(object sender, string newMessage);
        public event NewMessage? OnNewMessage;

        private void SendMessage(object sender, RoutedEventArgs e){
            if(chatSend.Text.Length>0 && (string)userName.Content != null &&((string)userName.Content).Length > 0){
                OnNewMessage?.Invoke(this, chatSend.Text);
                chatSend.Text = "";
            }
        }

        private void ChatEnter(object sender, KeyEventArgs e){
            if(e.Key == Key.Enter)
                SendMessage(sender, e);
        }

        private void EmojiPickerSelectionChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e){
            chatSend.Text += emojiPicker.Selection;
            emojiPicker.Selection = null;
        }
    }
}
