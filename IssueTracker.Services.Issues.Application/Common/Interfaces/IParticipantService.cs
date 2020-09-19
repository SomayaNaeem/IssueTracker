using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Services.Issues.Application.Common.Interfaces
{
	public interface IParticipantService
	{
		Task<Domain.Entities.Participant> GetParticipant(string email);
	}
}
