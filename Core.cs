using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Khachatryan_WPF
{
    public class Core
    {
        private static PCEntities _db;

        public static PCEntities DB
        {
            get
            {
                if (_db == null)
                    _db = new PCEntities();
                return _db;
            }
        }
    }
}
