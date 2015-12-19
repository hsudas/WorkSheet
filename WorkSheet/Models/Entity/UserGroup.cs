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
    [Table("dbo.UserGroup")]
    public class UserGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupId { get; set; }

        [DefaultValue(1)]
        [Index("IX_UserGroup_Status")]
        public int Status { get; set; }
        
        [MaxLength(30), Required]
        public string Name { get; set; }



        public UserGroup()
        {
            this.Status = 1;
        }
    }
}