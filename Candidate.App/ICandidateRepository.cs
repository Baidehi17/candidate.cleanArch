namespace Candidate.App
{
    public interface ICandidateRepository
    {
        List<Domain.Candidate> GetAllCandidate();
    }
}