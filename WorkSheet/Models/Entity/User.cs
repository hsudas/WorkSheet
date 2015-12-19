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
    [Table("dbo.User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [DefaultValue(1)]
        [Index("IX_User_Status")]
        public int Status { get; set; }

        //public UserGroup? UserGroupId { get; set; }

        public int UserTypeId { get; set; }
        //public virtual UserType UserType { get; set; }

        [MaxLength(30), Required]
        public string Name { get; set; }

        [MaxLength(30), Required]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
		
		[MaxLength(10)]
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        [MaxLength(15)]
        public string CreateUser { get; set; }

        [MaxLength(17)]
        public string CreateDate { get; set; }


        public User()
        {
            this.Status = 1;
            this.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
    }
}