using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Khachatryan_WPF
{
    internal class Core
    {
        private static CinemaEntities _context;
        public static CinemaEntities Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new CinemaEntities();
                }
                return _context;
            }
        }

        public static Users CurrentUser { get; set; }
    }
}
