using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WorkSheet.Models.Enum;

namespace WorkSheet.Models.Entity
{
    [Table("dbo.Job")]
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }

        [DefaultValue(1)]
        [Index("IX_Job_Status")]
        public int Status { get; set; }

        //todo: tipi UserType yapılacak  (Enum )
        public int UserTypeId { get; set; }
        //public virtual UserType UserType { get; set; }

        public JobType JobTypeId { get; set; }
        //public virtual JobType JobType { get; set; }

        public int JobGroupId { get; set; }
        public virtual JobGroup JobGroup { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public int Point { get; set; }

        [NotMapped]
        public int? Quantity { get; set; }

        [NotMapped]
        public int? Check200 { get; set; }

        [NotMapped]
        public int? NewItemsAutomatic { get; set; }

        [NotMapped]
        public int? PriceCheck150 { get; set; }

        [NotMapped]
        public int? MPCheckChange { get; set; }

        [NotMapped]
        public int? Minute { get; set; }

        [NotMapped, MaxLength(150)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Job()
        {
            this.Status = 1;
            this.Minute = 0;
            this.Quantity = 0;
            this.Check200 = 0;
            this.NewItemsAutomatic = 0;
            this.PriceCheck150 = 0;
            this.MPCheckChange = 0;
        }
    }
}