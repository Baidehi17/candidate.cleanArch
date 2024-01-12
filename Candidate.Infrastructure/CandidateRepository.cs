using Candidate.App;
using System;
using System.Collections.Generic;
using Candidate.Domain;

namespace Candidate.Infrastructure
{
    public class CandidateRepository : ICandidateRepository
    {
        public static List<Domain.Candidate> candidates = new List<Domain.Candidate>
        {
            new Domain.Candidate{Id=1,Name="ram",Location="hyd",Age=12},
            new Domain.Candidate{Id=2,Name="shaym",Location="hyd",Age=12},
            new Domain.Candidate{Id=3,Name="mohan",Location="hyd",Age=12},
            new Domain.Candidate{Id=1,Name="niti",Location="hyd",Age=12}

        };
        public List<Domain.Candidate> GetAllCandidate()
        {
            return candidates;
        }
 
    }
}