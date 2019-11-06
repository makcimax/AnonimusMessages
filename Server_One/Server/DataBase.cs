using System.Data.Entity;

namespace Server
{
    public class DataBase : DbContext
    {
        public DataBase() : base("DbConnectionString1")
        {
        }

        public DbSet<Message> Messages
        {
            get; set;

        }
    }

}
