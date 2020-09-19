using System.Threading.Tasks;
using IssueTracker.Services.Issues.Application.Bug.Commands.CreateBug;
using IssueTracker.Services.Issues.Application.Bug.Commands.UpdateBug;
using IssueTracker.Services.Issues.Application.Common.Models;
using IssueTracker.Services.Issues.Application.Issue.Commands.DeleteIssue;
using IssueTracker.Services.Issues.Application.Issue.Queries.GetIssues;
using IssueTracker.Services.Issues.Application.Issue.Story.Commands.CreateStory;
using IssueTracker.Services.Issues.Application.Issue.Story.Commands.UpdateStory;
using IssueTracker.Services.Issues.Application.Task.Commands.CreateTask;
using IssueTracker.Services.Issues.Application.Task.Commands.UpdateTask;
using Microsoft.AspNetCore.Mvc;
using static IssueTracker.Services.Issues.Application.Common.Helpers.EnumHelper;

namespace IssueTracker.Services.Issues.WebUI.Controllers
{
	public class IssueController : ApiController
    {
		#region Story
		#region Commands
		[HttpPost("story/create")]
		public async Task<ActionResult<string>> Create(CreateStoryCommand command)
		{
			return await Mediator.Send(command);
		}
		[HttpPut("story/{id}")]
		public async Task<ActionResult> Update(string id, UpdateStoryCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest();
			}
			await Mediator.Send(command);

			return NoContent();
		}
		#endregion
		#region Queries

		#endregion
		#endregion

		#region Task
		#region Commands
		[HttpPost("task/create")]
		public async Task<ActionResult<string>> Create(CreateTaskCommand command)
		{
			return await Mediator.Send(command);
		}
		[HttpPut("task/{id}")]
		public async Task<ActionResult> Update(string id, UpdateTaskCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest();
			}
			await Mediator.Send(command);

			return NoContent();
		}
		#endregion
		#region Queries

		#endregion
		#endregion

		#region Bug
		#region Commands
		[HttpPost("bug/create")]
		public async Task<ActionResult<string>> Create(CreateBugCommand command)
		{
			return await Mediator.Send(command);
		}
		[HttpPut("bug/{id}")]
		public async Task<ActionResult> Update(string id, UpdateBugCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest();
			}
			await Mediator.Send(command);

			return NoContent();
		}
		#endregion
		#region Queries

		#endregion
		#endregion

		#region Issue
		#region Commands
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(string id, IssueType issueType)
		{
			await Mediator.Send(new DeleteIssueCommand { Id = id ,IssueType=issueType});

			return NoContent();
		}
		#endregion
		#region Queries
		[HttpGet("{id}")]
		public async Task<ActionResult<IssuesListDto>> Get(string id,long projectId, int pageSize, int PageNumber)
		{
			return await Mediator.Send(new GetIssuesQuery() { Id=id,ProjectId = projectId, PageNumber = PageNumber, PageSize = pageSize });
		}
		#endregion
		#endregion
	}
}