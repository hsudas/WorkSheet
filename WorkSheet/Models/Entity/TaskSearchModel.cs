using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkSheet.Models.Entity
{
    public class TaskSearchModel
    {        
        public string Name { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int JobId { get; set; }

        public int UserTypeId { get; set; }

        public int TaskId { get; set; }

        public List<Task> SearchResults { get; set; }
    }
}