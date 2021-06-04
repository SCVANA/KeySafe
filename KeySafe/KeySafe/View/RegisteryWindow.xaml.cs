using System.Windows;
using KeySafe.Service;

namespace KeySafe.View
{
    public partial class RegisteryWindow : Window
    {
        public RegisteryWindow()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void GetDBBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.InitialDirectory = @"c:\";
            dialog.Title = "Select a Directory";
            dialog.Filter = "Directory|*.this.directory";
            dialog.FileName = "select";
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                path = path.Replace("\\select.this.directory", "");
                path = path.Replace(".this.directory", "");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                database.Text = path;
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if(passwordOne.Password == passwordTwo.Password && !string.IsNullOrEmpty(passwordOne.Password) && !string.IsNullOrEmpty(passwordTwo.Password) && !string.IsNullOrWhiteSpace(database.Text))
            {
                CreateDB createDB = new CreateDB(database.Text, "KeySafeDB", passwordOne.Password.ToString());
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.setDBPath(database.Text);
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
