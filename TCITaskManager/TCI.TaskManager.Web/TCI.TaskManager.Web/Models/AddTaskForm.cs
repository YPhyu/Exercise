using System.ComponentModel.DataAnnotations;
using Heroic.AutoMapper;
using TCI.TaskManager.Web.Core.Models;

namespace TCI.TaskManager.Web.Models
{
	public class AddTaskForm : IMapTo<Task>
	{
		[Required, Display(Name = "Task Name", Prompt = "Task name (ex: Task 1)")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }

        [Required]
        public int TaskTypeId { get; set; }

        [Range(typeof(bool), "false", "true")]
        public bool IsActive { get; set; }
    }
}