using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AmzErp.DataAccess
{
    public class DbInitializer
    {
        public static void Initializer(AmzErpDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.User.Find(1) == null)
            {
                context.User.Add(new Entity.User { UserName = "linv2", PassWord = "libing",DisplayName="linv2" });
            }
            context.SaveChanges();
        }
    }
}
