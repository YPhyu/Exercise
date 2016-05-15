using System.ComponentModel.DataAnnotations;
using Heroic.AutoMapper;
using TCI.TaskManager.Web.Core.Models;

namespace TCI.TaskManager.Web.Models
{
	public class AddTaskTypeForm : IMapTo<TaskType>
	{
		[Required, Display(Name = "Task Type Name", Prompt = "Task type name (ex: Other)")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }		
	}
}