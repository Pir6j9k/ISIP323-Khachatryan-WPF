using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Khachatryan_WPF
{
    public partial class Product
    {
        public bool IsHighDiscount => DiscountPercentage > 15;

    }
}
