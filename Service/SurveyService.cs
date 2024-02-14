using Domain.Entities;
using Repository;

namespace Service;

public class SurveyService(ISurveyRepository surveyRepository) : ISurveyService
{
    public async Task<Question?> GetQuestion(long questionId, CancellationToken token)
    {
        var question = await surveyRepository.GetQuestion(questionId, token);
        return question;
    }

    public async Task<long?> SaveResultAndGetNextQuestion(long sessionId, long questionId, ICollection<long> answerIds,
                                                          CancellationToken token)
    {
        var nextQuestionId = await surveyRepository.SaveResult(sessionId, questionId, answerIds, token);
        return nextQuestionId;
    }
}