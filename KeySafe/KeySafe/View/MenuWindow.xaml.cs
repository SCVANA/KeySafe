using KeySafe.Controller;
using KeySafe.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KeySafe.View
{
	
	/// <summary>
	/// Interaktionslogik für MenuWindow.xaml
	/// </summary>
	public partial class MenuWindow : Window
	{
		private int? directoryID;
		private SqliteConnectionService sqlConnectionService;
        private DirectoryController directoryController;
		public MenuWindow(SqliteConnectionService sqlConnectionService)
		{
            directoryController = new DirectoryController(sqlConnectionService);
			this.sqlConnectionService = sqlConnectionService;
            InitializeComponent();

			// TreeView füllen mit daten von der DB
			foreach(var item in directoryController.GetDirectory())
            {
				// Items hinzufuegen
				KeyDirectoryTree.Items.Add(item);
            }	
		}

		private TreeViewItem selected;
		/// <summary>
		/// Heraussuchen welcher Ordner Angeklickt wurde
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		private static DependencyObject SearchTreeView<T>(DependencyObject source)
		{
			while (source != null && source.GetType() != typeof(T))
			{
				source = VisualTreeHelper.GetParent(source);
			}
			return source;
		}

		/// <summary>
		/// MouseRightClick event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void MouseRightButtonDownTreeView(object sender, MouseButtonEventArgs e)
        {
			TreeViewItem treeViewItem =
		(TreeViewItem)SearchTreeView<TreeViewItem>
		((DependencyObject)e.OriginalSource);
			if (treeViewItem != null)
			{
				treeViewItem.IsSelected = true;
				e.Handled = true;
				selected = treeViewItem;
			}
		}

		/// <summary>
		/// Heraussuchen welcher auswahl im Contextmenue angewaelt wurde
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Click_ContextMenu(object sender, RoutedEventArgs e)
		{
			MenuItem item = (MenuItem)e.OriginalSource;
			if (selected != null)
			{
				
				//KeyDirectory selected =
				//		(KeyDirectory)KeyDirectoryTree.SelectedValue;
				switch (item.Name)
				{
					case "AddDirectory":
						sqlConnectionService.GetKeyDirectoryID(selected.Header.ToString());
						AddDirectory addParentDirectory = new AddDirectory(sqlConnectionService, KeyDirectoryTree, sqlConnectionService.GetKeyDirectoryID(selected.Header.ToString()));
						addParentDirectory.Show();
						break;
					case "RenameDirectory":
						break;
					case "DeleteDirectory":
						if (selected.DataContext != null)
							directoryController.DeleteDirectory(selected.DataContext.ToString());
						else
							directoryController.DeleteDirectory(selected.Header.ToString());
						KeyDirectoryTree.Items.Clear();
						foreach (var itemt in directoryController.GetDirectory())
						{
							// Items hinzufuegen
							KeyDirectoryTree.Items.Add(itemt);
						}
						break;
				}
			}
			else if(item.Name == "AddDirectory") {
				AddDirectory addDirecotry = new AddDirectory(sqlConnectionService, KeyDirectoryTree);
				addDirecotry.Show();
				
			}
			selected = null;
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			var selectedItem = KeyDirectoryTree.SelectedItem as TreeViewItem;
			string selectedItemstring = KeyDirectoryTree.SelectedItem.ToString();

			if (selectedItem != null)
			{
				string itemName = null;
				if (selectedItem.Header != null)
					itemName = selectedItem.Header.ToString();
				else if (selected.DataContext != null)
					itemName = selectedItem.DataContext.ToString();
				
			DatabaseAddWindow databaseAddWindow = new DatabaseAddWindow(sqlConnectionService, itemName, LstViewKeys);
			databaseAddWindow.Show();
			}
			else if(selectedItemstring != null)
            {
				DatabaseAddWindow databaseAddWindow = new DatabaseAddWindow(sqlConnectionService, selectedItemstring, LstViewKeys);
				databaseAddWindow.Show();
			}
			else
				MessageBox.Show("Bitte einen Ordner auswählen");
        }

		/// <summary>
		/// Key zu der ListView hinzufuegen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void MousLeftUp(object sender, MouseButtonEventArgs e)
        {
			TreeViewItem treeViewItem =
		(TreeViewItem)SearchTreeView<TreeViewItem>
		((DependencyObject)e.OriginalSource);
			if (treeViewItem != null)
			{
				treeViewItem.IsSelected = true;
				e.Handled = true;
				selected = treeViewItem;
				
				if(selected.Header != null)
					directoryID = sqlConnectionService.GetKeyDirectoryID(selected.Header.ToString());
				else
					directoryID = sqlConnectionService.GetKeyDirectoryID(selected.DataContext.ToString());
				List<Model.Key> keys =  sqlConnectionService.GetKeys(directoryID.Value);

				LstViewKeys.ItemsSource = keys;

			}
		}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
			var key = (Model.Key)LstViewKeys.SelectedItem;
			sqlConnectionService.DeleteKey(key.Title);

			List<Model.Key> keys = sqlConnectionService.GetKeys(directoryID.Value);

			LstViewKeys.ItemsSource = keys;

		}
    }
}
