using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using TCI.TaskManager.Web.Core.Models;
using TCI.TaskManager.Web.Data;
using TCI.TaskManager.Web.Models;

namespace TCI.TaskManager.Web.Controllers
{
    public class TaskTypeController : TCITaskManagerControllerBase
    {
        private readonly AppDbContext _context;

        public TaskTypeController(AppDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult All()
        {
            var taskTypeModels = _context.TaskTypes
                .OrderByDescending(x => x.LastUpdated).ProjectTo<TaskTypeViewModel>();
                

            return BetterJson(taskTypeModels.ToArray());
        }

        public JsonResult Add(AddTaskTypeForm form)
        {
            if (!ModelState.IsValid)
            {
                return JsonValidationError();
            }

            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AddTaskTypeForm, TaskType>();
                cfg.CreateMap<TaskType, AddTaskTypeForm>();
                cfg.CreateMap<TaskType, TaskTypeViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            
            var taskType = mapper.Map<TaskType>(form);
            
            //Checking duplicate task type
            var prevTaskType = _context.TaskTypes.Where(tt => tt.Name == form.Name).FirstOrDefault();

            if (prevTaskType != null)
            {
                return JsonError("Duplicate value");
            }
            else
                _context.TaskTypes.Add(taskType);

            _context.SaveChanges();

            var model = _context.TaskTypes.ProjectTo<TaskTypeViewModel>().Single(x => x.Id == taskType.Id);
            return BetterJson(model);

        }

        public JsonResult Update(EditTaskTypeForm form)
        {
            if (!ModelState.IsValid)
            {
                return JsonValidationError();
            }

            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EditTaskTypeForm, TaskType>();
                cfg.CreateMap<TaskType, TaskTypeViewModel>();
            });

            IMapper mapper = config.CreateMapper();

            //Checking duplicate task type
            var prevTaskType = _context.TaskTypes.Where(tt => tt.Name == form.Name).FirstOrDefault();

            if (prevTaskType != null)
            { 
                if(prevTaskType.Id != form.Id)
                    return JsonError("Name already exists.");
            }

            var target = _context.TaskTypes.Find(form.Id);

            mapper.Map(form, target);

            _context.SaveChanges();

            var updatedTaskType = _context.TaskTypes.ProjectTo<TaskTypeViewModel>().Single(x => x.Id == form.Id);

            return BetterJson(updatedTaskType);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {  
            var taskType = _context.TaskTypes.Find(id);
            
            if (taskType == null)
            {
                return JsonError("Unable to find the task type.");
            }

            if (_context.Tasks.Any(t => t.TaskTypeId == id))
            {
                return JsonError("Task Type is in used. Please delete the tasks first.");
            }

            _context.TaskTypes.Remove(taskType);

            _context.SaveChanges();
           
            return RedirectToAction("Index");
        }

    }
}