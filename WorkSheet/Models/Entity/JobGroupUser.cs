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
    [Table("dbo.JobGroupUser")]
    public class JobGroupUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobGroupUserId { get; set; }
        

        [DefaultValue(1)]
        [Index("IX_JobGroupUser_Status")]
        public int Status { get; set; }

        public int JobGroupId { get; set; }
        public virtual JobGroup JobGroup { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public JobGroupUser()
        {
            this.Status = 1;
        }
    }
}