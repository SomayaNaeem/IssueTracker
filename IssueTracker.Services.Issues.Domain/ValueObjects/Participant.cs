using IssueTracker.Services.Issues.Domain.Common;
using IssueTracker.Services.Issues.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Domain.ValueObjects
{
	public class Participant : ValueObject
	{
		private Participant()
		{

		}
		public Participant(string id, string name,string email)
		{
			Id = id;
			Name = name;
			Email = email;
		}
		public string Id { get;private set; }
		public string Name { get; private set; }
		public string Email { get; private set; }
		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Id;
			yield return Name;
			yield return Email;
		}
	}
}
