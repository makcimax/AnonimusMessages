using System;
using System.Data.Entity;

namespace Server
{
   public class DataBase : DbContext
    {
        public DataBase() : base("DbConnectionString")
        {
        }

        public DbSet<MessageDb> Messages
        {
            get; set;

        }
    }

}
