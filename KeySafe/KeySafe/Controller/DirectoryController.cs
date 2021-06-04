using System.Collections.Generic;
using System.Windows.Controls;
using KeySafe.Service;

namespace KeySafe.Controller
{
    class DirectoryController
    {
        private SqliteConnectionService sqlConnectionService;

        public DirectoryController(SqliteConnectionService sqlConnectionService)
        {
            this.sqlConnectionService = sqlConnectionService;
        }

        /// <summary>
        /// Sortiert die Ordnerstruktur
        /// </summary>
        /// <returns>Items fue TreeView in der richtigen reihenfolge</returns>
        public List<TreeViewItem> GetDirectory() {

            List<TreeViewItem> items = new List<TreeViewItem>();
            List<Model.KeyDirectory> mainDirectorys = sqlConnectionService.GetMainDirectorys();
            List<Model.KeyDirectory> parentDirectorys;
            foreach (var mainDirectory in mainDirectorys)
            {
                TreeViewItem main = new TreeViewItem();
                main.Header = mainDirectory.Name;
                parentDirectorys = sqlConnectionService.GetParentDirectorys(mainDirectory.ID);
                foreach (var parrentDirectory in parentDirectorys)
                {
                    main.Items.Add(parrentDirectory.Name);
                }
                items.Add(main);
            }
            return items;
        }   
        
        public void DeleteDirectory(string name)
        {
            sqlConnectionService.DeleteDirectory(name);
        }
    }
}
