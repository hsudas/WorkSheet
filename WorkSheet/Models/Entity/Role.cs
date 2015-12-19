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
    [Table("dbo.Role")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [DefaultValue(1)]
        [Index("IX_Role_Status")]
        public int Status { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        
        [NotMapped, MaxLength(150)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Role()
        {
            this.Status = 1;
        }
    }
}