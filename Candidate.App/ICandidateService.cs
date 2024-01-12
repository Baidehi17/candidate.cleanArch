using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application
{
    public interface ICandidateService
    {
        List<Domain.Candidate> GetAllCandidate();
    }
}
