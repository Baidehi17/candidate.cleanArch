using Candidate.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            this.candidateService = candidateService;
        }

        [HttpGet]
        public ActionResult<IList<Domain.Candidate>> GetAll()
        {
           return this.candidateService.GetAllCandidate();
        }
    }
}
