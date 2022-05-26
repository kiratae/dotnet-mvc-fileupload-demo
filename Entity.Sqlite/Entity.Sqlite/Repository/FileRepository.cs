using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using My.Demo.FileUpload.Model;
using File = My.Demo.FileUpload.Model.File;

namespace My.Demo.FileUpload.Entity.Sqlite
{
    public class FileRepository : IFileRepository
    {
        private readonly ILogger<FileRepository> _logger;

        public FileRepository(ILogger<FileRepository> logger)
        {
            _logger = logger;
        }

        public List<File> GetList(FileFilter filter, ResultPaging paging)
        {
            const string func = "GetList";
            try
            {
                List<File> list = new();
                using var connection = new SqliteConnection(EntityConfigSection.ConnectionString);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT FileId, FileGuid, FileName, MimeType, FileSize, Description, CreateDate, CreateUserId
                    FROM File
                ";

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int? fileId = reader.GetFieldValue<int?>(0);
                    string fileGuid = reader.GetFieldValue<string>(1);
                    string fileName = reader.GetFieldValue<string>(2);
                    string mimeType = reader.GetFieldValue<string>(3);
                    int fileSize = reader.GetFieldValue<int>(4);
                    string description = reader.GetFieldValue<string>(5);
                    DateTime createDate = reader.GetFieldValue<DateTime>(6);
                    int? createUserId = reader.GetFieldValue<int?>(7);

                    list.Add(new File()
                    {
                        FileId = fileId,
                        FileGuid = fileGuid,
                        FileName = fileName,
                        MimeType = mimeType,
                        FileSize = fileSize,
                        Description = description,
                        CreateDate = createDate,
                        CreateUserId = createUserId
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{func}: Exception caught.");
                throw;
            }
        }

        public File GetData(int fileId)
        {
            const string func = "GetData";
            try
            {
                using var connection = new SqliteConnection(EntityConfigSection.ConnectionString);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT FileId, FileGuid, FileName, MimeType, FileSize, Description, CreateDate, CreateUserId
                    FROM File
                    WHERE FileId = @fileId
                ";
                command.Parameters.AddWithValue("@fileId", fileId);
                command.Prepare();

                File file = null;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int? _fileId = reader.GetFieldValue<int?>(0);
                    string fileGuid = reader.GetFieldValue<string>(1);
                    string fileName = reader.GetFieldValue<string>(2);
                    string mimeType = reader.GetFieldValue<string>(3);
                    int fileSize = reader.GetFieldValue<int>(4);
                    string description = reader.IsDBNull(5) ? null : reader.GetFieldValue<string>(5);
                    DateTime createDate = reader.GetFieldValue<DateTime>(6);
                    int? createUserId = reader.IsDBNull(7) ? null : reader.GetFieldValue<int?>(7);

                    file = new File()
                    {
                        FileId = _fileId,
                        FileGuid = fileGuid,
                        FileName = fileName,
                        MimeType = mimeType,
                        FileSize = fileSize,
                        Description = description,
                        CreateDate = createDate,
                        CreateUserId = createUserId
                    };
                }

                return file;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{func}: Exception caught with FileId {fileId}.");
                throw;
            }
        }

        public File SaveData(File file)
        {
            const string func = "SaveData";
            try
            {
                List<File> list = new();
                using var connection = new SqliteConnection(EntityConfigSection.ConnectionString);
                connection.Open();
                if (!file.FileId.HasValue)
                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                        INSERT INTO File (FileGuid, FileName, MimeType, FileSize, Description, CreateDate, CreateUserId, IsTemp)
                        VALUES (@fileGuid, @fileName, @mimeType, @fileSize, @description, DATE('now'), @createUserId, @isTemp);
                        SELECT FileId, CreateDate FROM File WHERE FileId = LAST_INSERT_ROWID();
                    ";
                    command.Parameters.AddWithValue("@fileGuid", file.FileGuid);
                    command.Parameters.AddWithValue("@fileName", file.FileName);
                    command.Parameters.AddWithValue("@mimeType", file.MimeType);
                    command.Parameters.AddWithValue("@fileSize", file.FileSize);
                    command.Parameters.AddWithValue("@description", !string.IsNullOrEmpty(file.Description) ? file.Description : DBNull.Value);
                    command.Parameters.AddWithValue("@createUserId", file.CreateUserId.HasValue ? file.CreateUserId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@isTemp", file.IsTemp);
                    command.Prepare();

                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int? fileId = reader.GetFieldValue<int?>(0);
                        DateTime createDate = reader.GetFieldValue<DateTime>(1);
                        file.FileId = fileId;
                        file.CreateDate = createDate;
                    }
                }
                else
                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                        UPDATE File SET FileGuid = @fileGuid,
                            FileName = @fileName,
                            MimeType = @mimeType,
                            FileSize = @fileSize,
                            Description = @description,
                            IsTemp = @isTemp
                        WHERE FileId = @fileId;
                    ";
                    command.Parameters.AddWithValue("@fileGuid", file.FileGuid);
                    command.Parameters.AddWithValue("@fileName", file.FileName);
                    command.Parameters.AddWithValue("@mimeType", file.MimeType);
                    command.Parameters.AddWithValue("@fileSize", file.FileSize);
                    command.Parameters.AddWithValue("@description", !string.IsNullOrEmpty(file.Description) ? file.Description : DBNull.Value);
                    command.Parameters.AddWithValue("@isTemp", file.IsTemp);
                    command.Parameters.AddWithValue("@fileId", file.FileId.Value);
                    command.Prepare();

                    command.ExecuteNonQuery();
                }

                return file;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{func}: Exception caught.");
                throw;
            }
        }

        public bool DeletData(int fileId)
        {
            const string func = "DeletData";
            try
            {
                using var connection = new SqliteConnection(EntityConfigSection.ConnectionString);
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM File WHERE FileId = @fileId;
                ";
                command.Parameters.AddWithValue("@fileId", fileId);
                command.Prepare();

                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{func}: Exception caught with FileId {fileId}.");
                throw;
            }
        }
    }
}
