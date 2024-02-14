using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public sealed class SurveyRepository(ApplicationContext context) : ISurveyRepository
{
    private const long NoNextQuestion = -1;

    public async Task<Question?> GetQuestion(long questionId, CancellationToken token)
    {
        if (context.Questions.Any())
            return await context.Questions
                                .Include(question => question.Answers)
                                .FirstOrDefaultAsync(question => question.Id == questionId, token);

        return null;
    }

    public async Task<long?> SaveResult(long interviewId, long questionId, ICollection<long> answerIds,
                                        CancellationToken token)
    {
        var question = await GetQuestion(questionId, token);


        if (question == null)
            throw new QuestionException($"Question with id {questionId} not found.");

        if (!question.AllowedManyAnswers && answerIds.Count > 1)
            throw new QuestionException("Violation of question constraint. Only one answer is allowed.");

        if (question.Mandatory && answerIds.Count < 1)
            throw new QuestionException("Violation of question constraint. Question is mandatory.");

        var results = answerIds.Select(id =>
                                           new Result
                                           {
                                               InterviewId = interviewId,
                                               AnswerId = id
                                           }
                                      ).ToList();

        await context.Results.AddRangeAsync(results, token);
        await context.SaveChangesAsync(token);

        var interview = await context.Interviews.FirstAsync(i => i.Id == interviewId, token);

        var nextQuestion = await context.Questions
                                        .FirstOrDefaultAsync(x =>
                                                                 x.SurveyId == interview.SurveyId &&
                                                                 x.OrderNo == question.OrderNo + 1,
                                                             token);

        return nextQuestion?.Id ?? NoNextQuestion;
    }
}