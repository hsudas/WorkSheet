using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkSheet.Models.Entity
{
    [Table("dbo.JobUserType")]
    public class JobUserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobUserId { get; set; }

        [DefaultValue(1)]
        [Index("IX_JobUser_Status")]
        public int Status { get; set; }
        public int JobId { get; set; }

        public int UserTypeId { get; set; }
    }
}