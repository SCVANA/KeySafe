using KeySafe.Controller;
using System.Windows;
using KeySafe.Service;
using System.Windows.Controls;
using System.Collections.Generic;

namespace KeySafe.View
{
    /// <summary>
    /// Interaktionslogik für DatabaseAddWindow.xaml
    /// </summary>
    public partial class DatabaseAddWindow : Window
	{
		private SqliteConnectionService sqliteConnectionService;
		private int directoryId;
		private ListView listViewKey;
		public DatabaseAddWindow(SqliteConnectionService sqliteConnectionService, string directoryName, ListView listViewKey)
		{
			this.sqliteConnectionService = sqliteConnectionService;
			this.directoryId = sqliteConnectionService.GetKeyDirectoryID(directoryName).Value;
			this.listViewKey = listViewKey;
			InitializeComponent();
		}

        private void PwdGenBtn_CLick(object sender, RoutedEventArgs e)
        {
			PwdGeneratorWindow pwdGenerator = new PwdGeneratorWindow();
			pwdGenerator.Show();
			PasswordGenerator passwordGenerator = new PasswordGenerator(pwdGenerator.getPwd(), "", "13");
			TextBoxPassword.Text = passwordGenerator.GeneratePassword();		
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {	
			Model.Key key = new Model.Key();
			key.Title = titel.Text;
			key.Username = benutzername.Text;
			key.Password = TextBoxPassword.Text;
			key.Url = url.Text;
			key.Notes = notizen.Text;
			key.DirectoryID = directoryId;
			sqliteConnectionService.SetKey(key);

			List<Model.Key> keys = sqliteConnectionService.GetKeys(directoryId);

			listViewKey.ItemsSource = keys;

			this.Close();
		}
    }
}
