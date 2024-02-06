using Candidate.API.DTO;
using Candidate.API.Utils;
using Candidate.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace Candidate.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        private readonly ICandidateService candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            this.candidateService = candidateService;
        }

        [HttpGet]
        [Authorize(Policy = AuthorizationPolicies.AssignmentToTaskAdminRoleRequired)]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetCandidates()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var candidates = await this.candidateService.GetAllCandidates();
            IEnumerable<CandidateDTO> lists = AutoMapper<Domain.Candidate, CandidateDTO>.Map(candidates);
            return candidates == null ? NotFound() : Ok(lists.ToList());
        }

        [HttpGet("id")]
        [Authorize(Policy = AuthorizationPolicies.AssignmentToTaskAdminRoleRequired)]
        public async Task<ActionResult<CandidateDTO>> GetCandidateById(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            CandidateDTO candidateDetail = AutoMapper<Domain.Candidate, CandidateDTO>.Map(await this.candidateService.GetCandidateById(id));
            return candidateDetail == null ? NotFound() : Ok(candidateDetail);   
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationPolicies.AssignmentToTaskAdminRoleRequired)]
        public async Task updateCandidate(CandidateDTO candidate)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            Domain.Candidate candidateDetail = AutoMapper<CandidateDTO, Domain.Candidate>.Map(candidate);
            await this.candidateService.UpdateCandidate(candidateDetail);
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.AssignmentToTaskAdminRoleRequired)]
        public async Task AddCandidate(CandidateDTO candidate)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            Domain.Candidate candidateDetail = AutoMapper<CandidateDTO, Domain.Candidate>.Map(candidate);
            await this.candidateService.AddCandidate(candidateDetail);
        }

        [HttpDelete]
        [Authorize(Policy = AuthorizationPolicies.AssignmentToTaskAdminRoleRequired)]
        public async Task DeleteCandidate(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            await this.candidateService.DeleteCandidateById(id);
        }

    }
}
