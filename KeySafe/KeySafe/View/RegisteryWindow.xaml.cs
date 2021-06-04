using System.Windows;
using KeySafe.Service;

namespace KeySafe.View
{
    /// <summary>
    /// Interaktionslogik für RegisteryWindow.xaml
    /// </summary>
    public partial class RegisteryWindow : Window
    {
        public RegisteryWindow()
        {
            InitializeComponent();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void GetDBBtn_Click(object sender, RoutedEventArgs e)
        {
            // Create a "Save As" dialog for selecting a directory (HACK)
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.InitialDirectory = @"c:\"; // Use current value for initial dir
            dialog.Title = "Select a Directory"; // instead of default "Save As"
            dialog.Filter = "Directory|*.this.directory"; // Prevents displaying files
            dialog.FileName = "select"; // Filename will then be "select.this.directory"
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                // Remove fake filename from resulting path
                path = path.Replace("\\select.this.directory", "");
                path = path.Replace(".this.directory", "");
                // If user has changed the filename, create the new directory
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                // Our final value is in path
                database.Text = path;
            }
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
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
