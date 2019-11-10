using System.Data.Entity;

namespace Server
{
    public class DataBase : DbContext
    {
        public DataBase() : base("ConnectionStringToDbOfMsg")
        {
        }

        public DbSet<Message> Messages
        {
            get; set;
        }
    }


    public class DataBaseOfAbonents : DbContext
    {
        public DataBaseOfAbonents() : base("ConnectionStringToDbOfAbonents")
        {

        }

        public DbSet<Abonent> Abonents
        {
            get; set;

        }

    }

}
