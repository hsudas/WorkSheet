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
    [Table("dbo.JobGroup")]
    public class JobGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobGroupId { get; set; }

        [DefaultValue(1)]
        [Index("IX_JobGroup_Status")]
        public int Status { get; set; }

        //public int JobId { get; set; }
        //public virtual Job Job { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        
        [NotMapped, MaxLength(150)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public JobGroup()
        {
            this.Status = 1;
        }
    }
}