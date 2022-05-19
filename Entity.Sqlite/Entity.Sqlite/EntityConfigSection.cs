
namespace My.Demo.FileUpload.Entity.Sqlite
{
    internal class EntityConfigSection
    {
        public static readonly string ConnectionStringName = "Demo";

        public static string ConnectionString
        {
            get { return "Data Source=C:\\my\\sqlite\\demo.db"; }
        }
    }
}
