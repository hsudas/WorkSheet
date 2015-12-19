using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSheet.Models.Entity
{
    [Table("dbo.Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DefaultValue(1)]
        [Index("IX_Product_Status")]
        public int Status { get; set; }

        [MaxLength(15)]
        public string ProductId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public double? Amount { get; set; }

        public int? Quantity { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(17)]
        public string CreateDate { get; set; }


        public Product()
        {
            this.Status = 1;
            this.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

    }
}
