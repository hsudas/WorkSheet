using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkSheet.Models.Entity
{
    [Table("dbo.Task")]
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [DefaultValue(1)]
        [Index("IX_Task_Status")]
        public int Status { get; set; }

        [Index("IX_Task_User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public Nullable<int> JobId { get; set; }
        public virtual Job Job { get; set; }

        [MaxLength(150)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int Point { get; set; }

        public int? Quantity { get; set; }

        public int? Check200 { get; set; }

        public int? NewItemsAutomatic { get; set; }

        public int? PriceCheck150 { get; set; }

        public int? MPCheckChange { get; set; }

        public int? Hour { get; set; }

        public int? Minute { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public IEnumerable<Task> OrderDetail { get; set; }

        public int CreateUser { get; set; }

        [MaxLength(17)]
        public string CreateDate { get; set; }


        public Task()
        {
            this.Status = 1;
            this.EventDate = DateTime.Now.Date;
            this.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

    }
}