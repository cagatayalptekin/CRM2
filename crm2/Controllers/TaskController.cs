using Application.Contracts.Personels;
using Application.Contracts.Roles;
using Application.Contracts.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace crm2.Controllers
{
    public class TaskController : Controller
    {
        private ITaskService _taskService;
        private IPersonelService _personelService;
        public TaskController(ITaskService taskService, IPersonelService personelService)
        {
            _personelService = personelService;
            _taskService = taskService;
        }
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.getAllTasks();
            var taskList = tasks.Value.ToList();

            return View("~/Views/Task/GetAll.cshtml", taskList);
        }
        public async Task<IActionResult> Yeni()
        {
            var personels = await _personelService.getAllPersonels();
            var depslist = personels.Value.ToList();
            

            var model = new TaskFormViewModel
            {
                Personels=depslist,
                TaskDto=new TaskDto()
            };
            return View("Yeni", model);
        }

        public async Task<IActionResult> Guncelle(int id)
        {
            var personels = await _personelService.getAllPersonels();
            var depslist = personels.Value.ToList();
            var taskDto = await _taskService.getTaskById(id);
            var tas = taskDto.Value;

            var model = new TaskFormViewModel
            {
                Personels = depslist,
                TaskDto = tas
            };
            return View("Yeni", model);
        }
        public async Task<IActionResult> Kaydet(TaskDto TaskDto)
        {
            if(!ModelState.IsValid)
            {
                var personels = await _personelService.getAllPersonels();
                var depslist = personels.Value.ToList();


                var model = new TaskFormViewModel
                {
                    Personels = depslist,
                    TaskDto = new TaskDto()
                };
                return View("Yeni", model);
            }
            
            if (TaskDto.Id == 0)
            {
                var result = await _taskService.isTaskExist(TaskDto);

                if (result.Value == true)
                {
                    return View("Eklenemedi");
                }
                else
                {
                    await _taskService.AddTask(TaskDto);
                }
            }
            else
            {
                await _taskService.Guncelle(TaskDto);
            }

            return RedirectToAction("Index");


        }
        public async Task<ActionResult<TaskDto>> deleteTask(int id)
        {
            var deleted = await _taskService.deleteTask(id);

            return RedirectToAction("Index");
        }
        public async Task<ActionResult<List<TaskDto>>> TasklariListele(int id)
        {
            var tasks = await _taskService.TasklariListele(id);

            return PartialView(tasks.Value.ToList());
        }


    }
}
