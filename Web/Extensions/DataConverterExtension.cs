using Domain.Entities;
using Web.Responses;

namespace Web.Extensions;

public static class DataConverterExtension
{
    public static QuestionResponse? ToQuestionResponse(this Question question)
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