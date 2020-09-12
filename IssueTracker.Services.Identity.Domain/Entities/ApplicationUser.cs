using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
namespace IssueTracker.Services.Identity.Domain.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
