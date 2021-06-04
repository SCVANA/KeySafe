using System.Data.SQLite;

namespace KeySafe.Service
{
    class CreateDB
    {
        public CreateDB(string path, string name, string password)
        {
            string pathDB = $@"{path}\{name}.db";
            SQLiteConnection.CreateFile(pathDB);
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={pathDB}"))
            {
                connection.SetPassword(password);

                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE 'KeyDirectorys' ('ID' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 'Name' VARCHAR(50) NOT NULL, 'ParentID' INTEGER, FOREIGN KEY('ParentID') REFERENCES 'KeyDirectorys');";
                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE 'Keys' ('ID' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,'Title' VARCHAR(50) NOT NULL,'Username'  VARCHAR(50) NOT NULL,'Password'  VARCHAR(50) NOT NULL,'Url' VARCHAR(255),'Notes' VARCHAR(255), 'DirectoryID' INTEGER NOT NULL, FOREIGN KEY('DirectoryID') REFERENCES KeyDirectorys(ID));";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
