using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core_projekt2.DTO
{
    [Serializable]
    public class Item_DTO
    {
        [Ignore]
        public int id_item { get; set; }
        public string item_name { get; set; }
        public int item_count { get; set; }
        public decimal item_price { get; set; }
        public int item_category { get; set; }
    }
}
