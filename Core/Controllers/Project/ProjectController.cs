using DBService.MongoDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.DataLayer.ProjectDetailModels;
using System.Security.Claims;

namespace Core.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ProjectController : ControllerBase
    {
        private readonly IMongoDBService _projectService;
        public ProjectController(IMongoDBService dbService)
        {
            _projectService = dbService;
        }
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> projectList()
        {
            string currentUser = User.FindFirstValue(ClaimTypes.Name);
            var flag = await this._projectService.checkExistProjectByUserId(currentUser);
            if (flag)
            {
                var list = await this._projectService.getProjectDetailsByUser(currentUser);
                return Ok(new { status = "success", projectDetails = list });
            }
            else
            {
                return Ok(new { status = "countZero" });
            }
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> projectCreate([FromBody] ProjectDetail project)
        {
            string currentUser = User.FindFirstValue(ClaimTypes.Name);
            await _projectService.createProject(currentUser, project.name);
            return Ok(new { status = "success" });
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> projectUpdate([FromBody] ProjectDetail project)
        {
            string currentUser = User.FindFirstValue(ClaimTypes.Name);
            await _projectService.updateProjectDetail(project.id, project.name);
            return Ok(new { status = "success" });
        }
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> projectDelete([FromBody] ProjectDetail project)
        {
            string currentUser = User.FindFirstValue(ClaimTypes.Name);
            await _projectService.deleteProject(project.id);
            return Ok(new { status = "success" });
        }
        [HttpPost]
        [Route("log/latest")]
        public async Task<IActionResult> getLatestLogs([FromBody] ProjectDetail project)
        {
            string currentUser = User.FindFirstValue(ClaimTypes.Name);
            var flag = await this._projectService.checkExistProjectById(project.id);
            if (flag)
            {
                var list = await this._projectService.getLatestLogs(project.id);
                return Ok(new { status = "success", logs = list });
            }
            else
            {
                return Ok(new { status = "fail", message = "Project does not exist" });
            }
        }
        [HttpPost]
        [Route("log/all")]
        public async Task<IActionResult> getLogs([FromBody] ProjectDetail project)
        {
            string currentUser = User.FindFirstValue(ClaimTypes.Name);
            var flag = await this._projectService.checkExistProjectById(project.id);
            if (flag)
            {
                var list = await this._projectService.getLogs(project.id);
                return Ok(new { status = "success", logs = list });
            }
            else
            {
                return Ok(new { status = "fail", message = "Project does not exist" });
            }
        }
        [HttpPost]
        [Route("log/insert")]
        public async Task<IActionResult> setLogs([FromBody] LogInsertModel model)
        {
            string currentUser = User.FindFirstValue(ClaimTypes.Name);
            var flag = await _projectService.addLog(model.log, model.project.id);
            if (flag)
            {
                return Ok(new { status = "success" });
            }
            else
            {
                return Ok(new { status = "fail", message = "Can not Add log" });
            }
        }
    }
}
