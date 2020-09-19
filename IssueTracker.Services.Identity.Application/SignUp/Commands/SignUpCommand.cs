using IssueTracker.Services.Identity.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Services.Identity.Application.SignUp.Commands
{
    public class SignUpCommand : IRequest<SignUpResult>
	{
		public string Email { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
	}
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpResult>
    {
        private readonly IIdentityService _identityService;
        public SignUpCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<SignUpResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var (result, id) = await _identityService.CreateUserAsync(request);
            return new SignUpResult(result.Success, result.Errors, id);
        }
    }
}
