using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Services.Issues.Application.Project.Commands.CreateProject;
using IssueTracker.Services.Issues.Application.Project.Commands.DeleteProject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Services.Issues.WebUI.Controllers
{
    public class ProjectController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateProjectCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteProjectCommand { Id = id });

            return NoContent();
        }
    }
}