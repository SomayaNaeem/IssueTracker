using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Services.Issues.Application.Participant.Commands.CreateParticipant;
using IssueTracker.Services.Issues.Application.Participant.Commands.DeleteParticipant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Services.Issues.WebUI.Controllers
{
   
    public class ParticipantController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateParticipantCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id,string email)
        {
            await Mediator.Send(new DeleteParticipantCommand { ProjectId = id,Email= email});

            return NoContent();
        }
    }
}