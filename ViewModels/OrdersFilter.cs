using ASUTask.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASUTask.ViewModels
{
    public class OrdersFilter
    {
        public OrdersFilter(List<Order> orders, DateTime? startDate, DateTime? endDate)
        {
            Orders = orders;
            StartDate = startDate;
            EndDate = endDate;
        }
        public List<Order> Orders { get; set; }
        public Order Order { get; set; }
        public SelectList Providers { get; set; }
        public SelectList OrderNumbers { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
