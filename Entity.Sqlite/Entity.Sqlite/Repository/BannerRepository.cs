using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using My.Demo.FileUpload.Model;
using File = My.Demo.FileUpload.Model.File;

namespace My.Demo.FileUpload.Entity.Sqlite
{
    public class BannerRepository : IBannerRepository
    {
        private readonly ILogger<BannerRepository> _logger;

        public BannerRepository(ILogger<BannerRepository> logger)
        {
            _logger = logger;
        }

        public List<Banner> GetList(BannerFilter filter, ResultPaging paging)
        {
            const string func = "GetList";
            try
            {
                List<Banner> list = new();
                using var connection = new SqliteConnection(EntityConfigSection.ConnectionString);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT BannerId, Title, FileId
                    FROM Banner
                    WHERE (@keyword IS NULL
                        OR LOWER(Title) LIKE LOWER(@keyword))
                ";
                command.Parameters.AddWithValue("@keyword", !string.IsNullOrEmpty(filter.Keyword) ? filter.Keyword : DBNull.Value);
                command.Prepare();

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int? bannerId = reader.GetFieldValue<int?>(0);
                    string title = reader.GetFieldValue<string>(1);
                    int fileId = reader.GetFieldValue<int>(2);

                    list.Add(new Banner()
                    {
                        BannerId = bannerId,
                        Title = title,
                        FileId = fileId
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught.", func);
                throw;
            }
        }

        public Banner GetData(int bannerId)
        {
            const string func = "GetData";
            try
            {
                using var connection = new SqliteConnection(EntityConfigSection.ConnectionString);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT BannerId, Title, FileId
                    FROM Banner
                    WHERE BannerId = @bannerId
                ";
                command.Parameters.AddWithValue("@bannerId", bannerId);
                command.Prepare();

                Banner banner = null;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int _bannerId = reader.GetFieldValue<int>(0);
                    string title = reader.GetFieldValue<string>(1);
                    int fileId = reader.GetFieldValue<int>(2);

                    banner = new Banner()
                    {
                        BannerId = _bannerId,
                        Title = title,
                        FileId = fileId
                    };
                }

                return banner;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught with banner id {bannerId}.", func, bannerId);
                throw;
            }
        }

        public Banner SaveData(Banner banner)
        {
            const string func = "SaveData";
            try
            {
                List<File> list = new();
                using var connection = new SqliteConnection(EntityConfigSection.ConnectionString);
                connection.Open();
                if (!banner.BannerId.HasValue)
                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                        INSERT INTO Banner (Title, FileId, CreateDate)
                        VALUES (@title, @fileId, DATE('now'));
                        SELECT LAST_INSERT_ROWID();
                    ";
                    command.Parameters.AddWithValue("@title", banner.Title);
                    command.Parameters.AddWithValue("@fileId", banner.FileId);
                    command.Prepare();

                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int bannerId = reader.GetFieldValue<int>(0);
                        banner.BannerId = bannerId;
                    }
                }
                else
                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                        UPDATE Banner SET Title = @title,
                            FileId = @fileName,
                            ModifyDate = DATE('now')
                        WHERE BannerId = @bannerId;
                    ";
                    command.Parameters.AddWithValue("@title", banner.Title);
                    command.Parameters.AddWithValue("@fileId", banner.FileId);
                    command.Prepare();

                    command.ExecuteNonQuery();
                }

                return banner;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught.", func);
                throw;
            }
        }

        public bool DeletData(int bannerId)
        {
            const string func = "DeletData";
            try
            {
                using var connection = new SqliteConnection(EntityConfigSection.ConnectionString);
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM Banner WHERE BannerId = @bannerId;
                ";
                command.Parameters.AddWithValue("@bannerId", bannerId);
                command.Prepare();

                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught with banner id {bannerId}.", func, bannerId);
                throw;
            }
        }
    }
}
