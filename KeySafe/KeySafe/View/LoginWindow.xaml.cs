using Microsoft.Win32;
using System;
using System.Windows;
using KeySafe.Service;

namespace KeySafe.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void GetDBBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DB file (*.db)|*.db";
            openFileDialog.InitialDirectory = @"c:\";
            openFileDialog.InitialDirectory = Environment.GetFolderPath
                                                (
                                                    Environment.SpecialFolder.MyDocuments
                                                );
            if (openFileDialog.ShowDialog() == true)
            {
                database.Text = openFileDialog.FileName;
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            SqliteConnectionService sqliteConnectionService = new SqliteConnectionService(database.Text, passwordns.Password.ToString());

            MenuWindow menuWindow = new MenuWindow(sqliteConnectionService);
            menuWindow.Show();
            this.Close();

        }

        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisteryWindow registeryWindow = new RegisteryWindow();
            registeryWindow.Show();
            this.Close();
        }

        public void setDBPath(string path)
        {
            database.Text = $"{path}\\KeySafeDB.db";
        }
    }
}
