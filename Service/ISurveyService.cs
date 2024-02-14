using Domain.Entities;

namespace Service;

public interface ISurveyService
{
    public Task<Question?> GetQuestion(long questionId, CancellationToken token);

    public Task<long?> SaveResultAndGetNextQuestion(long sessionId, long questionId, ICollection<long> answerIds,
                                                    CancellationToken token);
}