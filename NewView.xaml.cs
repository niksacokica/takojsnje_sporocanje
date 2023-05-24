using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace takojsnje_sporocanje{
    public partial class NewView : UserControl{
        private Storyboard story = new Storyboard();

        public NewView(){
            InitializeComponent();

            Binding name = new("Name"){
                Source = Properties.Settings.Default
            };
            username.SetBinding(Label.ContentProperty, name);

            Binding avatar = new("Avatar"){
                Source = Properties.Settings.Default
            };
            useravatar.SetBinding(Image.SourceProperty, avatar);

            TypewriteName();
        }

        private void stiki_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e){
            var item = (ListView)sender;
            if (item != null && item.SelectedItem != null)
                MessageBox.Show(item.SelectedItem.ToString());
        }

        private void ChatBox_OnNewMessage(object sender, string newMessage) =>
            ((ViewModel)DataContext).OnNewMessage(newMessage);

        public void NameChanged() =>
            TypewriteName();

        private void TypewriteName(){
            string textToAnimate = Properties.Settings.Default.Name;

            story.Stop();
            story.RepeatBehavior = RepeatBehavior.Forever;

            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
            stringAnimationUsingKeyFrames.Duration = new Duration(TimeSpan.FromSeconds(textToAnimate.Length / 2));

            string tmp = string.Empty;
            foreach (char c in textToAnimate){
                DiscreteStringKeyFrame discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                tmp += c;
                discreteStringKeyFrame.Value = tmp;
                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }

            while (!string.IsNullOrEmpty(tmp)){
                DiscreteStringKeyFrame discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                discreteStringKeyFrame.Value = tmp;
                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);

                tmp = tmp.Remove(tmp.Length - 1);
            }

            Storyboard.SetTargetName(stringAnimationUsingKeyFrames, username.Name);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath(ContentProperty));
            
            story.Children.Add(stringAnimationUsingKeyFrames);
            story.Begin(username);
        }
    }
}
