using KeySafe.Controller;
using System.Windows;
using KeySafe.Service;
using System.Windows.Controls;
using System.Collections.Generic;

namespace KeySafe.View
{
    public partial class DatabaseAddWindow : Window
	{
		private SqliteConnectionService _sqliteConnectionService;
		private int _directoryId;
		private ListView _listViewKey;

		public DatabaseAddWindow(SqliteConnectionService sqliteConnectionService, string directoryName, ListView listViewKey)
		{
			_sqliteConnectionService = sqliteConnectionService;
			_directoryId = sqliteConnectionService.GetKeyDirectoryID(directoryName).Value;
			_listViewKey = listViewKey;
			InitializeComponent();
		}

        private void PwdGenBtn_CLick(object sender, RoutedEventArgs e)
        {
			PwdGeneratorWindow pwdGenerator = new PwdGeneratorWindow();
			pwdGenerator.Show();
			PasswordGenerator passwordGenerator = new PasswordGenerator(pwdGenerator.getPwd(), "", "13");
			TextBoxPassword.Text = passwordGenerator.GeneratePassword();		
        }

        private void setupBtn_Click(object sender, RoutedEventArgs e)
        {	
			Model.Key key = new Model.Key();
			key.Title = titel.Text;
			key.Username = benutzername.Text;
			key.Password = TextBoxPassword.Text;
			key.Url = url.Text;
			key.Notes = notizen.Text;
			key.DirectoryID = _directoryId;
			_sqliteConnectionService.SetKey(key);

			List<Model.Key> keys = _sqliteConnectionService.GetKeys(_directoryId);

			_listViewKey.ItemsSource = keys;

			Close();
		}
    }
}
