using KeySafe.Controller;
using KeySafe.Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KeySafe.View
{
	public partial class MenuWindow : Window
	{
		private int? _directoryID;
		private SqliteConnectionService _sqlConnectionService;
        private DirectoryController _directoryController;
        private TreeViewItem _selected;

        public MenuWindow(SqliteConnectionService sqlConnectionService)
		{
            _directoryController = new DirectoryController(sqlConnectionService);
			_sqlConnectionService = sqlConnectionService;
            InitializeComponent();

			foreach(var item in _directoryController.GetDirectory())
            {
				KeyDirectoryTree.Items.Add(item);
            }	
		}

		private static DependencyObject SearchTreeView<T>(DependencyObject source)
		{
			while (source != null && source.GetType() != typeof(T))
			{
				source = VisualTreeHelper.GetParent(source);
			}
			return source;
		}

        private void MouseRightButtonDownTreeView(object sender, MouseButtonEventArgs e)
        {
			TreeViewItem treeViewItem =
		(TreeViewItem)SearchTreeView<TreeViewItem>
		((DependencyObject)e.OriginalSource);
			if (treeViewItem != null)
			{
				treeViewItem.IsSelected = true;
				e.Handled = true;
				_selected = treeViewItem;
			}
		}

		private void Click_ContextMenu(object sender, RoutedEventArgs e)
		{
			MenuItem item = (MenuItem)e.OriginalSource;
			if (_selected != null)
			{
				switch (item.Name)
				{
					case "AddDirectory":
						_sqlConnectionService.GetKeyDirectoryID(_selected.Header.ToString());
						AddDirectory addParentDirectory = new AddDirectory(_sqlConnectionService, KeyDirectoryTree, _sqlConnectionService.GetKeyDirectoryID(_selected.Header.ToString()));
						addParentDirectory.Show();
						break;
					case "RenameDirectory":
						break;
					case "DeleteDirectory":
						if (_selected.DataContext != null)
							_directoryController.DeleteDirectory(_selected.DataContext.ToString());
						else
							_directoryController.DeleteDirectory(_selected.Header.ToString());
						KeyDirectoryTree.Items.Clear();
						foreach (var itemt in _directoryController.GetDirectory())
						{
							KeyDirectoryTree.Items.Add(itemt);
						}
						break;
				}
			}
			else if(item.Name == "AddDirectory") {
				AddDirectory addDirecotry = new AddDirectory(_sqlConnectionService, KeyDirectoryTree);
				addDirecotry.Show();
				
			}
			_selected = null;
		}

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
			var selectedItem = KeyDirectoryTree.SelectedItem as TreeViewItem;
			string selectedItemstring = KeyDirectoryTree.SelectedItem.ToString();

			if (selectedItem != null)
			{
				string itemName = null;
				if (selectedItem.Header != null)
					itemName = selectedItem.Header.ToString();
				else if (_selected.DataContext != null)
					itemName = selectedItem.DataContext.ToString();
				
			DatabaseAddWindow databaseAddWindow = new DatabaseAddWindow(_sqlConnectionService, itemName, LstViewKeys);
			databaseAddWindow.Show();
			}
			else if(selectedItemstring != null)
            {
				DatabaseAddWindow databaseAddWindow = new DatabaseAddWindow(_sqlConnectionService, selectedItemstring, LstViewKeys);
				databaseAddWindow.Show();
			}
			else
				MessageBox.Show("Please select a folder");
        }


        private void MouseLeftUp(object sender, MouseButtonEventArgs e)
        {
			TreeViewItem treeViewItem =
		(TreeViewItem)SearchTreeView<TreeViewItem>
		((DependencyObject)e.OriginalSource);
			if (treeViewItem != null)
			{
				treeViewItem.IsSelected = true;
				e.Handled = true;
				_selected = treeViewItem;
				
				if(_selected.Header != null)
					_directoryID = _sqlConnectionService.GetKeyDirectoryID(_selected.Header.ToString());
				else
					_directoryID = _sqlConnectionService.GetKeyDirectoryID(_selected.DataContext.ToString());
				List<Model.Key> keys =  _sqlConnectionService.GetKeys(_directoryID.Value);

				LstViewKeys.ItemsSource = keys;

			}
		}

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
			var key = (Model.Key)LstViewKeys.SelectedItem;
			_sqlConnectionService.DeleteKey(key.Title);

			List<Model.Key> keys = _sqlConnectionService.GetKeys(_directoryID.Value);

			LstViewKeys.ItemsSource = keys;

		}
    }
}
