using KeySafe.Controller;
using KeySafe.Service;
using System.Windows;
using System.Windows.Controls;

namespace KeySafe.View
{
    public partial class AddDirectory : Window
    {
        private SqliteConnectionService _sqliteConnectionService;
        private TreeView _tree;
        private int? _mainDirectoryID;

        public AddDirectory(SqliteConnectionService sqlConnectionService, TreeView tree)
        {
            _tree = tree;
            _sqliteConnectionService = sqlConnectionService;
            InitializeComponent();
        }

        public AddDirectory(SqliteConnectionService sqlConnectionService, TreeView tree, int? mainDirectoryID)
        {
            _mainDirectoryID = mainDirectoryID;
            _tree = tree;
            _sqliteConnectionService = sqlConnectionService;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(_mainDirectoryID != null)
                _sqliteConnectionService.SetKeyDirectorys(TextBoxDirectoryName.Text, _mainDirectoryID.Value);               
            else
            {
                _sqliteConnectionService.SetKeyDirectorys(TextBoxDirectoryName.Text); 
            }

            _tree.Items.Clear();
            DirectoryController directoryController = new DirectoryController(_sqliteConnectionService);
            foreach (var item in directoryController.GetDirectory())
            {
                _tree.Items.Add(item);
            }
            Close();
        }
    }
}
