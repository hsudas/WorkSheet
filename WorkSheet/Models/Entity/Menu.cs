using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkSheet.Models.Entity
{
    [Table("dbo.Menu")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }

        [DefaultValue(1)]
        [Index("IX_Menu_Status")]
        public int Status { get; set; }

        public int? RoleId { get; set; }

        public int? ParentId { get; set; }

        [MaxLength(50),Required]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Url { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        public int? OrderNumber { get; set; }

        public Menu()
        {
            this.Status = 1;
        }

  

    }
}