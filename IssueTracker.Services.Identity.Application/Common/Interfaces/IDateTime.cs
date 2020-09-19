using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Identity.Application.Common.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
