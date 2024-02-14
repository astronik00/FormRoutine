using Microsoft.AspNetCore.Mvc;
using Service;
using Web.Extensions;
using Web.Requests;
using Web.Responses;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/survey")]
[Produces("application/json")]
public class SurveyController(ISurveyService surveyService) : Controller
{
    [HttpGet("{questionId:long}")]
    public async Task<QuestionResponse?> GetQuestion([FromRoute] long questionId, CancellationToken token)
    {
        var question = await surveyService.GetQuestion(questionId, token);
        return question?.ToQuestionResponse();
    }

    [HttpPost("")]
    public async Task<long?> SaveResult([FromBody] SaveResultRequest request, CancellationToken token)
    {
        var saveResultAndGetNextQuestion = await surveyService.SaveResultAndGetNextQuestion(request.SessionId,
                                            request.QuestionId, request.AnswerIds,
                                            token);
        return saveResultAndGetNextQuestion;
    }
}