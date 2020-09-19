using AutoMapper;
using IssueTracker.Services.Issues.Application.Common.Mappings;
using IssueTracker.Services.Issues.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static IssueTracker.Services.Issues.Application.Common.Helpers.EnumHelper;

namespace IssueTracker.Services.Issues.Application.Issue.Queries.GetIssues
{
	public class IssueDto : Common.Models.Issue 
	{
		public string Id { get; set; }
		
	}

}
