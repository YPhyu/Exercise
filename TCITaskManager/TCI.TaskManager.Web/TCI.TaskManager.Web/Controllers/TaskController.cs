using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using TCI.TaskManager.Web.Core.Models;
using TCI.TaskManager.Web.Data;
using TCI.TaskManager.Web.Models;

namespace TCI.TaskManager.Web.Controllers
{
    public class TaskController : TCITaskManagerControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult All()
        {
            var taskModels = _context.Tasks
                .OrderByDescending(x => x.LastUpdated).ProjectTo<TaskViewModel>();

            return BetterJson(taskModels.ToArray());
        }

        public JsonResult Add(AddTaskForm form)
        {
            if (!ModelState.IsValid)
            {
                return JsonValidationError();
            }

            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AddTaskForm, Task>();
                cfg.CreateMap<Task, AddTaskForm>();
                cfg.CreateMap<Task, TaskViewModel>();
            });

            IMapper mapper = config.CreateMapper();

            var task = mapper.Map<Task>(form);

            //Checking duplicate task
            var prevTask = _context.Tasks.Where(tt => tt.Name == form.Name).FirstOrDefault();

            if (prevTask != null)
            {
                return JsonError("Duplicate value");
            }
            else
                _context.Tasks.Add(task);

            _context.SaveChanges();

            var model = _context.Tasks.Where(x => x.Id == task.Id).ProjectTo<TaskViewModel>().Single();
            return BetterJson(model);

        }

        public JsonResult Update(EditTaskForm form)
        {
            if (!ModelState.IsValid)
            {
                return JsonValidationError();
            }

            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EditTaskForm, Task>();
                cfg.CreateMap<Task, TaskViewModel>();
            });

            IMapper mapper = config.CreateMapper();

            //Checking duplicate task
            var prevTask = _context.Tasks.Where(tt => tt.Name == form.Name).FirstOrDefault();

            if (prevTask != null)
            {
                if (prevTask.Id != form.Id)
                    return JsonError("Name already exists.");
            }

            var target = _context.Tasks.Find(form.Id);

            mapper.Map(form, target);

            _context.SaveChanges();

            var updatedTask = _context.Tasks.ProjectTo<TaskViewModel>().Single(x => x.Id == form.Id);

            return BetterJson(updatedTask);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return JsonError("Unable to find the task.");
            }

            _context.Tasks.Remove(task);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}