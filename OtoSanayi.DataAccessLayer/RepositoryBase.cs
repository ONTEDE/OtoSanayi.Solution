using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoSanayi.DataAccessLayer
{
    public class RepositoryBase
    {
        protected static DatabaseContext context;
      

        protected RepositoryBase()
        {
            CreateContext();
        }

        private static void CreateContext()
        {
            if (context == null)
            {
                context = new DatabaseContext();
            }

        }
    }
}
