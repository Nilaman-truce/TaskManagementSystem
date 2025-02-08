using System.Data;
using Dapper;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Data.SqlClient;
using TaskManagementSystem.Models;
using TaskManagementSystem.Repositories;
using System.Data.Common;
using System.Threading.Tasks;

namespace TaskManagementSystem.Controllers
{
    public class TaskController : Controller
    {

        private readonly ITaskRepository _repository;

        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(string searchTerm = "", int page = 1)
        {
                ViewBag.CurrentSearch = searchTerm;
                ViewBag.CurrentPage = page;
                var task = await _repository.GetAllTask(searchTerm, page);
                return View(task);
           
        }
        [HttpGet]
        public IActionResult CreateTask()
        {
                var status = _repository.GetStatus();
                var priority = _repository.GetPriority();
                ViewBag.Status = status;
                ViewBag.Priority = priority;

                return PartialView();
           
        }

        [HttpPost]
        public async Task<IActionResult> SaveTask([FromBody] TasksModel task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.AddTask(task);
                    return Json(new { IsSuccess = true, ReturnMessage = "Saved Successfully", ReturnInteger = task.Id });
                }
                return Json(new { IsSuccess = false, ReturnMessage = "There was an error saving the task." });
            }


            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }


        public async Task<IActionResult> EditTask(int id)
        {
                var task = await _repository.GetTaskById(id);
                var status = _repository.GetStatus();
                var priority = _repository.GetPriority();
                ViewBag.Status = status;
                ViewBag.Priority = priority;

                return PartialView(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] TasksModel task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.UpdateTask(task);
                    return Json(new { IsSuccess = true, ReturnMessage = "Edited Successfully", ReturnInteger = task.Id });
                }
                return Json(new { IsSuccess = false, ReturnMessage = "There was an error editing the task." });
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteTask(id);
                return Json(new { IsSuccess = true, ReturnMessage = "Deleted Successfully", ReturnInteger = id });
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }


    }
}
