using System.Data.Entity;

namespace Server
{
    public class DataBase : DbContext
    {
        public DataBase() : base("DbConnectionString")
        {
        }

        public DbSet<Message> Messages
        {
            get; set;

        }
    }

}
