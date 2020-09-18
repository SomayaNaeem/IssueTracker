using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Common.Exceptions
{
    public class ExistException : Exception
    {
        public ExistException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was Exist.")
        {
        }
    }
}
