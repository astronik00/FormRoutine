using Domain.Entities;
using Repository;

namespace Service;

public sealed class SurveyService(ISurveyRepository surveyRepository) : ISurveyService
{
    public async Task<Question?> GetQuestion(long questionId, CancellationToken token) =>
        await surveyRepository.GetQuestion(questionId, token);


    public async Task<long?> SaveResultAndGetNextQuestion(long sessionId, long questionId, ICollection<long> answerIds,
                                                          CancellationToken token) =>
        await surveyRepository.SaveResult(sessionId, questionId, answerIds, token);
}