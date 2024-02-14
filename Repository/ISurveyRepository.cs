using Domain.Entities;

namespace Repository;

public interface ISurveyRepository
{
    public Task<Question?> GetQuestion(long questionId, CancellationToken token);

    public Task<long?> SaveResult(long interviewId, long questionId, ICollection<long> answerIds,
                                  CancellationToken token);
}