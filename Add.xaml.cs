using Microsoft.Win32;
using System.Windows;

namespace Project1
{
    public partial class Add : Window
    {
        private int count;
        
        public string _name { get; private set; }
        public string _photo { get; private set; }
        public string _info { get; private set; }

        public Add(int count)
        {
            InitializeComponent();
            this.count = count;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _name = name.Text;
            _photo = photo.Text;
            _info = info.Text;
            DialogResult = true;

        }

        //private void SelectImage_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "Image Files (*.jpg, *.png, *.gif)|*.jpg;*.png;*.gif|All files (*.*)|*.*";
        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        string selectedImagePath = openFileDialog.FileName;
        //        photo.Image = selectedImagePath; 
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       
    }
}
