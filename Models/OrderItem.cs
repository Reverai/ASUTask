using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASUTask.Models
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Display(Name = "Номер заказа")]
        public Order Order { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Количество")]
        public decimal Quantity { get; set; }
        [Display(Name = "Ед. изм.")]
        public string Unit { get; set; }

    }
}
