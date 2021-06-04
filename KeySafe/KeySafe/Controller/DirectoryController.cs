using System.Collections.Generic;
using System.Windows.Controls;
using KeySafe.Service;

namespace KeySafe.Controller
{
    class DirectoryController
    {
        private SqliteConnectionService _sqlConnectionService;

        public DirectoryController(SqliteConnectionService sqlConnectionService)
        {
            _sqlConnectionService = sqlConnectionService;
        }

        public List<TreeViewItem> GetDirectory()
        {

            List<TreeViewItem> items = new List<TreeViewItem>();
            List<Model.KeyDirectory> mainDirectorys = _sqlConnectionService.GetMainDirectorys();
            List<Model.KeyDirectory> parentDirectorys;
            foreach (var mainDirectory in mainDirectorys)
            {
                TreeViewItem main = new TreeViewItem();
                main.Header = mainDirectory.Name;
                parentDirectorys = _sqlConnectionService.GetParentDirectorys(mainDirectory.ID);
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
            _sqlConnectionService.DeleteDirectory(name);
        }
    }
}
