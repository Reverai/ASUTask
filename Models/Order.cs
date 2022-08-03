using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASUTask.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Number { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Provider")]
        public int ProviderId { get; set; }

        public Provider Provider { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
