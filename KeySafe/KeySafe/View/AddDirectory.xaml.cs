using KeySafe.Controller;
using KeySafe.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KeySafe.View
{
    /// <summary>
    /// Interaktionslogik für AddDirectory.xaml
    /// </summary>
    public partial class AddDirectory : Window
    {
        private SqliteConnectionService sqliteConnectionService;
        private TreeView tree;
        private int? mainDirectoryID;
        public AddDirectory(SqliteConnectionService sqlConnectionService, TreeView tree)
        {
            this.tree = tree;
            this.sqliteConnectionService = sqlConnectionService;
            InitializeComponent();
        }

        public AddDirectory(SqliteConnectionService sqlConnectionService, TreeView tree, int? mainDirectoryID)
        {
            this.mainDirectoryID = mainDirectoryID;
            this.tree = tree;
            this.sqliteConnectionService = sqlConnectionService;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(mainDirectoryID != null)
                sqliteConnectionService.SetKeyDirectorys(TextBoxDirectoryName.Text, mainDirectoryID.Value);               
            else
            {
                sqliteConnectionService.SetKeyDirectorys(TextBoxDirectoryName.Text); 
            }

            tree.Items.Clear();
            DirectoryController directoryController = new DirectoryController(sqliteConnectionService);
            foreach (var itemt in directoryController.GetDirectory())
            {
                // Items hinzufuegen
                tree.Items.Add(itemt);
            }
            Close();
        }
    }
}
