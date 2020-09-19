using AutoMapper;
using IssueTracker.Services.Identity.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Services.Identity.Application.Profile.Queries
{
	public class GetProfileInfoQuery:IRequest<ProfileInfoDto>
	{
		public string Email { get; set; }
	}
	public  class GetProfileInfoQueryHandler : IRequestHandler<GetProfileInfoQuery, ProfileInfoDto>
	{
		private readonly IIdentityService _identityService;
		private readonly IMapper _mapper;
		public GetProfileInfoQueryHandler(IIdentityService identityService, IMapper mapper)
		{
			_identityService = identityService;
			_mapper = mapper;
		}
		public async Task<ProfileInfoDto> Handle(GetProfileInfoQuery request, CancellationToken cancellationToken)
		{
			
			return _mapper.Map<ProfileInfoDto>(await _identityService.GetUser(request.Email));

		}
	}
}
