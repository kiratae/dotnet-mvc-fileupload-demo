using Microsoft.Data.Sqlite;

namespace My.Demo.FileUpload.Entity.Sqlite
{
    public class EntityHelper
    {
        public static void Init()
        {
            using var connection = new SqliteConnection(EntityConfigSection.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            CreateFileTable(command);
            CreateBannerTable(command);
        }

        protected static void CreateFileTable(SqliteCommand command)
        {
            command.CommandText = "DELETE FROM Banner";
            command.ExecuteNonQuery();
            command.CommandText = "DROP TABLE IF EXISTS File";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE File (
                FileId INTEGER PRIMARY KEY,
                FileGuid NVARCHAR(40) NOT NULL,
                FileName NVARCHAR(200) NOT NULL,
                MimeType NVARCHAR(100) NOT NULL,
                FileSize INT NOT NULL,
                Description NVARCHAR(500),
                CreateDate DATETIME NOT NULL,
                CreateUserId INT,
                IsTemp BIT NOT NULL
            )";
            command.ExecuteNonQuery();

            Console.WriteLine("Table 'File' was created.");
        }

        protected static void CreateBannerTable(SqliteCommand command)
        {
            command.CommandText = "DROP TABLE IF EXISTS Banner";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE Banner (
                BannerId INTEGER PRIMARY KEY,
                Title NVARCHAR(200) NOT NULL,
                FileId INT NOT NULL,
                CreateDate DATETIME NOT NULL,
                CreateUserId INT,
                ModifyDate DATETIME NOT NULL,
                ModifyUserId INT,
                FOREIGN KEY(FileId) REFERENCES File(FileId)
            )";
            command.ExecuteNonQuery();

            Console.WriteLine("Table 'Banner' was created.");
        }
    }
}
