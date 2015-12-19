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
    [Table("dbo.RoleRule")]
    public class RoleRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleRuleId { get; set; }
        

        [DefaultValue(1)]
        [Index("IX_RoleRule_Status")]
        public int Status { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int RuleId { get; set; }
        public virtual Rule Rule { get; set; }

        public RoleRule()
        {
            this.Status = 1;
        }
    }
}