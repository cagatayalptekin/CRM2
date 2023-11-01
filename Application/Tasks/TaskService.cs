using Application.Contracts.Departmans;
using Application.Contracts.Personels;
using Application.Contracts.Roles;
using Application.Contracts.Tasks;
using AutoMapper;
using Domain;
using Domain.Personels;
using Domain.Roles;
using Domain.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Domain.Task;

namespace Application.Tasks
{
    public class TaskService: ITaskService
    {
        const string cacheKey = "tasks";

        private ITaskRepository _taskRepository;
        private IPersonelRepository _personelRepository;
        private IMapper _mapper;
        private readonly IMemoryCache _memCache;

        public TaskService(ITaskRepository taskRepository, IPersonelRepository personelRepository,IMapper mapper,IMemoryCache memCache)
        {
            _taskRepository = taskRepository;
            _personelRepository = personelRepository;
            _mapper = mapper;
            _memCache = memCache;
        }

        public async Task<ActionResult<TaskDto>> AddTask(TaskDto taskDto)
        {
            var task = _mapper.Map<TaskDto, Task>(taskDto);
            //await _roleRepository.Add(role);
            //return roleDto;
            await _taskRepository.Add(task);
            _memCache.Remove(cacheKey);
            return taskDto;
        }

        public async Task<ActionResult<TaskDto>> deleteTask(int id)
        {
            var task = await _taskRepository.Delete(id);
            var taskdto = _mapper.Map<Task, TaskDto>(task);
            _memCache.Remove(cacheKey);
            return taskdto;
        }

        public async Task<ActionResult<IEnumerable<TaskDto>>> getAllTasks()
        {
            if (!_memCache.TryGetValue(cacheKey, out List<TaskDto> taskdto))
            {
                var tasks = await _taskRepository.GetAll();
                var personels = await _personelRepository.GetAll();
                 
                foreach (var item in tasks)
                {
                    var personelId = item.PersonelId;
                    var personel = personels.FirstOrDefault(x => x.Id == personelId);
                    item.Personel = personel;
                    
                    
                }
                taskdto = _mapper.Map<List<Task>, List<TaskDto>>(tasks);
                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };
                _memCache.Set(cacheKey, taskdto, cacheExpOptions);
            }
                
            
            return taskdto;
        }

        public async Task<ActionResult<TaskDto>> getTaskById(int id)
        {
            var task = await _taskRepository.Get(id);
            var taskdto = _mapper.Map<Task, TaskDto>(task);
            return taskdto;
        }

        public async Task<ActionResult<TaskDto>> Guncelle(TaskDto taskDto)
        {
            var task = _mapper.Map<TaskDto, Task>(taskDto);
            await _taskRepository.Update(task);
            _memCache.Remove(cacheKey);

            return taskDto;
        }
        public async Task<ActionResult<List<TaskDto>>> TasklariListele(int id)
        {
            var allTasks = await _taskRepository.GetAll();
            var tasks = allTasks.Where(x => x.PersonelId == id).ToList();
            var taskdto = _mapper.Map<List<Task>, List<TaskDto>>(tasks);

            return taskdto;
        }
        public async Task<ActionResult<Boolean>> isTaskExist(TaskDto taskDto)
        {
            var taskList = await _taskRepository.GetAll();
            if (taskList.Any(x => x.TaskName == taskDto.TaskName))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
