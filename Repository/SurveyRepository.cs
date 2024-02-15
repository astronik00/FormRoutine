using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public sealed class SurveyRepository(ApplicationContext context) : ISurveyRepository
{
    /// <summary>
    /// Получение данных вопроса
    /// </summary>
    /// <param name="questionId"> Идентификатор вопроса </param>
    /// <param name="token"> Токен отмены </param>
    /// <returns> Вопрос с заданным идентификатором </returns>
    public async Task<Question?> GetQuestion(long questionId, CancellationToken token) =>
        !context.Questions.Any()
            ? null
            : await context.Questions
                           .Include(question => question.Answers)
                           .FirstOrDefaultAsync(question => question.Id == questionId, token);

    /// <summary>
    /// Сохранение результатов и получение следующего вопроса
    /// </summary>
    /// <param name="interviewId"> Идентификатор сессии </param>
    /// <param name="questionId"> Идентификатор вопроса </param>
    /// <param name="answerIds"> Список идентификаторов выбранных ответов</param>
    /// <param name="token"> Токен отмены </param>
    /// <returns> Идентификатор следующего вопроса </returns>
    public async Task<long?> SaveResult(long interviewId, long questionId, ICollection<long> answerIds,
                                        CancellationToken token)
    {
        var question = await GetQuestion(questionId, token);

        if (question == null)
            throw new QuestionException($"Question with id={questionId} not found.");

        if (!question.AllowedManyAnswers && answerIds.Count > 1)
            throw new QuestionException("Violation of question constraint. Only one answer is allowed.");

        if (question.Mandatory && answerIds.Count < 1)
            throw new QuestionException("Violation of question constraint. Question is mandatory.");

        var results = answerIds.Select(id =>
        {
            if (question.Answers.Any(answer => answer.Id == id))
                return new Result
                       {
                           InterviewId = interviewId,
                           AnswerId = id
                       };

            throw new
                QuestionException($"Answer with id={id} does not correspond to the question with id={questionId}.");
        }).ToList();

        await context.Results.AddRangeAsync(results, token);
        await context.SaveChangesAsync(token);

        var interview = await context.Interviews.FirstAsync(i => i.Id == interviewId, token);

        var nextQuestion = await context.Questions
                                        .FirstOrDefaultAsync(x =>
                                                                 x.SurveyId == interview.SurveyId &&
                                                                 x.OrderNo == question.OrderNo + 1,
                                                             token);

        return nextQuestion?.Id;
    }
}