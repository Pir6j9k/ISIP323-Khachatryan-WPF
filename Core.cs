using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Khachatryan_WPF
{
    using System;
    public class Core
    {
        private static TheBeautySalonEntities _db;

        public static TheBeautySalonEntities DB => GetContext();

        public static TheBeautySalonEntities GetContext()
        {
            if (_db == null)
            {
                _db = new TheBeautySalonEntities();
            }
            return _db;
        }

        public static User AuthUser = null;
        public static List<Product> SelectedProducts = new List<Product>();
    }
}
