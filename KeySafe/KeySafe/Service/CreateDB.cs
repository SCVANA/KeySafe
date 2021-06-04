using System.Data.SQLite;

namespace KeySafe.Service
{
    /// <summary>
    /// Erstellen der DB fuer neuen User
    /// </summary>
    class CreateDB
    {
        /// <summary>
        /// Datenbank ertstellen
        /// </summary>
        /// <param name="path">Wo die DB gespeichert werden soll</param>
        /// <param name="name">Name der DB</param>
        /// <param name="password">Passwort</param>
        public CreateDB(string path, string name, string password)
        {
            // Zusammengesetzter Pfad
            string pathDB = $@"{path}\{name}.db";
            // DB Erstellen
            SQLiteConnection.CreateFile(pathDB);
            // Connection zur DB aufbauen
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={pathDB}"))
            {
                // Password setzten
                connection.SetPassword(password);

                // Connection öffnen
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    // Tabelle Für Ordner TreeView erstellen
                    command.CommandText = "CREATE TABLE 'KeyDirectorys' ('ID' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 'Name' VARCHAR(50) NOT NULL, 'ParentID' INTEGER, FOREIGN KEY('ParentID') REFERENCES 'KeyDirectorys');";
                    command.ExecuteNonQuery();
                    // Tabelle für Schluessel erstellen
                    command.CommandText = "CREATE TABLE 'Keys' ('ID' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,'Title' VARCHAR(50) NOT NULL,'Username'  VARCHAR(50) NOT NULL,'Password'  VARCHAR(50) NOT NULL,'Url' VARCHAR(255),'Notes' VARCHAR(255), 'DirectoryID' INTEGER NOT NULL, FOREIGN KEY('DirectoryID') REFERENCES KeyDirectorys(ID));";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
