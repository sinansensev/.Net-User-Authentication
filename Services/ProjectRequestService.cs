// ProjectRequestService.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PayosferIdentity.Data;
using PayosferIdentity.Models;

namespace PayosferIdentity.Services
{
    public class ProjectRequestService
    {
        private readonly AppDbContext _context;

        public ProjectRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectRequest>> GetAllProjectRequests()
        {
            return await _context.ProjectRequests.ToListAsync();
        }

        public async Task<ProjectRequest> GetProjectRequestById(int id)
        {
            return await _context.ProjectRequests.FindAsync(id);
        }

        public async Task AddProjectRequest(ProjectRequest projectRequest, IFormFile pdfFile)
        {
            projectRequest.CreateTime = DateTime.Now; 
            if (pdfFile != null && pdfFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await pdfFile.CopyToAsync(memoryStream);
                    projectRequest.PdfContent = memoryStream.ToArray();
                }
            }

            _context.ProjectRequests.Add(projectRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectRequest(int id, ProjectRequest projectRequest)
        {
            var existingProjectRequest = await _context.ProjectRequests.FindAsync(id);
            if (existingProjectRequest == null)
            {
                throw new ArgumentException("Project request not found");
            }

            existingProjectRequest.Title = projectRequest.Title;
            existingProjectRequest.Description = projectRequest.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectRequest(int id)
        {
            var projectRequest = await _context.ProjectRequests.FindAsync(id);
            if (projectRequest == null)
            {
                throw new ArgumentException("Project request not found");
            }

            _context.ProjectRequests.Remove(projectRequest);
            await _context.SaveChangesAsync();
        }
    }
}
