using ASUTask.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public SelectList Names { get; set; }
        public SelectList Units { get; set; }
    }
}
