using Domain.Entities;
using Web.Responses;

namespace Web.Extensions;

/// <summary>
/// Класс для преобразования классов-сущностей в response
/// </summary>
public static class DataConverterExtension
{
    /// <summary>
    /// Преобразует вопрос в response
    /// </summary>
    public static QuestionResponse ToQuestionResponse(this Question question)
    {
        return new QuestionResponse
               {
                   Id = question.Id,
                   SurveyId = question.SurveyId,
                   Mandatory = question.Mandatory,
                   AllowedManyAnswers = question.AllowedManyAnswers,
                   Name = question.Name,
                   OrderNo = question.OrderNo,
                   Answers = question.Answers.Select(answer => answer.ToAnswersResponse()).ToList()
               };
    }

    /// <summary>
    /// Преобразует ответ в response
    /// </summary>
    public static AnswerResponse ToAnswersResponse(this Answer answer)
    {
        return new AnswerResponse
               {
                   AnswerId = answer.Id,
                   QuestionId = answer.QuestionId,
                   Content = answer.Content
               };
    }
}