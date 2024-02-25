using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayosferIdentity.Models;
using PayosferIdentity.Services;
using Microsoft.AspNetCore.Authorization; 

namespace PayosferIdentity.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectRequestsController : ControllerBase
    {
        private readonly ProjectRequestService _projectRequestService;

        public ProjectRequestsController(ProjectRequestService projectRequestService)
        {
            _projectRequestService = projectRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectRequest>>> GetAllProjectRequests()
        {
            var projectRequests = await _projectRequestService.GetAllProjectRequests();
            return Ok(projectRequests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectRequest>> GetProjectRequestById(int id)
        {
            var projectRequest = await _projectRequestService.GetProjectRequestById(id);
            if (projectRequest == null)
            {
                return NotFound();
            }
            return projectRequest;
        }

        [HttpPost]
        [Authorize(Roles = "Müşteri")] 
        public async Task<ActionResult<ProjectRequest>> AddProjectRequest([FromForm] ProjectRequest projectRequest, IFormFile pdfFile)
        {
            if (projectRequest == null || string.IsNullOrWhiteSpace(projectRequest.Title) || string.IsNullOrWhiteSpace(projectRequest.Description))
            {
                return BadRequest("Title and Description are required fields.");
            }

            projectRequest.CreateTime = DateTime.Now; 

            await _projectRequestService.AddProjectRequest(projectRequest, pdfFile);
            return CreatedAtAction(nameof(GetProjectRequestById), new { id = projectRequest.Id }, projectRequest);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Müşteri")] 
        public async Task<IActionResult> UpdateProjectRequest(int id, ProjectRequest projectRequest)
        {
            if (id != projectRequest.Id)
            {
                return BadRequest();
            }

            try
            {
                await _projectRequestService.UpdateProjectRequest(id, projectRequest);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Müşteri")] 
        public async Task<IActionResult> DeleteProjectRequest(int id)
        {
            try
            {
                await _projectRequestService.DeleteProjectRequest(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
