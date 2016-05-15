using System;
using System.Collections.Generic;
using AutoMapper;
using Heroic.AutoMapper;
using TCI.TaskManager.Web.Core.Models;

namespace TCI.TaskManager.Web.Models
{
	public class TaskTypeViewModel : IMapFrom<TaskType>
    {
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
        public DateTime LastUpdated { get; set; }

       public string LastUpdatedBy { get; set; }
               
       public virtual User ApplicationUser { get; set; }

        public string FullName { get { return this.ApplicationUser.UserName; } }
    }
}