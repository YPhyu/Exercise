using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCI.TaskManager.Web.Core.Models
{
    public class BaseEntity
    {
        public DateTime LastUpdated { get; set; }
        [Required]
        public string LastUpdatedBy { get; set; }

        [ForeignKey("LastUpdatedBy")]
        public virtual User ApplicationUser { get; set; }

    }
}