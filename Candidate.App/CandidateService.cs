using Candidate.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }
        public List<Domain.Candidate> GetAllCandidate()
        {
            return this.candidateRepository.GetAllCandidate();
        }
    }
}
