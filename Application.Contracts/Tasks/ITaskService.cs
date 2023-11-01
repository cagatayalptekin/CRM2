using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Tasks
{
    public interface ITaskService
    {
        public Task<ActionResult<IEnumerable<TaskDto>>> getAllTasks();
        public Task<ActionResult<TaskDto>> deleteTask(int id);
        public Task<ActionResult<TaskDto>> AddTask(TaskDto taskDto);
        public Task<ActionResult<TaskDto>> Guncelle(TaskDto taskDto);
        public Task<ActionResult<TaskDto>> getTaskById(int id);
        public Task<ActionResult<List<TaskDto>>> TasklariListele(int id);
        public Task<ActionResult<Boolean>> isTaskExist(TaskDto taskDto);



    }
}
