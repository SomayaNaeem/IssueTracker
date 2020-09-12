using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Services.Identity.Application.Common.Interfaces
{
	public interface IApplicationDbContext
	{
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
