using Business.Services;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("projects")]
    public class ProjectsController(IProjectService projectService) : Controller
    {
        private readonly IProjectService _projectService = projectService;

        [HttpGet("")]
        public async Task<IActionResult> Projects()
        {
            var viewModel = new ProjectsViewModel
            {
                ProjectList = await PopulateProjectList(),
            };
            return View(viewModel);
        }


        private async Task<IEnumerable<ProjectViewModel>> PopulateProjectList()
        {
            var result = await _projectService.GetAllAsync();
            var projects = result.Data;
            var projectViewModels = new List<ProjectViewModel>();

            if (projects == null)
            {
                return projectViewModels;
            }

            foreach (var project in projects) // TODO: maybe a null check here?
            {
                var viewModel = project.ToViewModel();
                projectViewModels.Add(viewModel);
                // TODO: refactor this 
            }
            return projectViewModels;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProjectViewModel viewModel) 
        {
            ViewBag.ErrorMessage = null;

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var addProjectFormData = viewModel.MapTo<AddProjectFormData>();
            var result = await _projectService.AddAsync(addProjectFormData);
            if (result.Succeeded)
            {
                return View();
            }
            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(viewModel);

        }

        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(EditProjectViewModel viewModel)
        {
            ViewBag.ErrorMessage = null;
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var updateProjectFormData = viewModel.MapTo<EditProjectFormData>();
            var result = await _projectService.UpdateAsync(updateProjectFormData, viewModel.Id);
            if (result.Succeeded)
            {
                return View();
            }
            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(viewModel);
        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string projectId)
        {
            return View();
        }
   
    }
}
