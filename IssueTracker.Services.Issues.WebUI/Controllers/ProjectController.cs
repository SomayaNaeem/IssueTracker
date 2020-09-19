using System.Threading.Tasks;
using IssueTracker.Services.Issues.Application.Common.Models;
using IssueTracker.Services.Issues.Application.Project.Commands.CreateProject;
using IssueTracker.Services.Issues.Application.Project.Commands.DeleteProject;
using IssueTracker.Services.Issues.Application.Project.Queries.GetProjectParticipants;
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

        #region Queries
        [HttpGet("{id}")]
        public async Task<ActionResult<PagedList<ParticipantsListDto>>> Get(long id,int pageSize, int PageNumber)
        {
            return await Mediator.Send(new GetProjectParticipantsQuery() { ProjectId=id,PageNumber = PageNumber, PageSize = pageSize });
        }
        #endregion
    }
}