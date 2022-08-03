using ASUTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASUTask.ViewModels
{
    public class ViewItems
    {
        public Order Order { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
