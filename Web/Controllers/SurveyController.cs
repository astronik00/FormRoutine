using Microsoft.AspNetCore.Mvc;
using Service;
using Web.Extensions;
using Web.Requests;
using Web.Responses;

namespace Web.Controllers;

/// <summary>
/// Контроллер для работы с анкетой
/// </summary>
[ApiController]
[Route("api/v1/survey")]
[Produces("application/json")]
public class SurveyController(ISurveyService surveyService) : Controller
{
    /// <summary>
    /// Возвращает данные вопроса, а также список возможных ответов на него
    /// </summary>
    /// <param name="questionId"> Идентификатор вопроса </param>
    /// <param name="token"> Токен отмены </param>
    /// <returns> Данные вопроса </returns>
    [HttpGet("{questionId:long}")]
    public async Task<QuestionResponse?> GetQuestion([FromRoute] long questionId, CancellationToken token)
    {
        var question = await surveyService.GetQuestion(questionId, token);
        return question?.ToQuestionResponse();
    }

    /// <summary>
    /// Сохраняет ответ на вопрос, возвращает идентификатор следующего вопроса анкеты
    /// </summary>
    /// <param name="request"> Параметры для сохранения ответа </param>
    /// <param name="token"> Токен отмены </param>
    /// <returns> Идентификатор следующего вопроса или -1 в случае конца </returns>
    [HttpPost("")]
    public async Task<long?> SaveResult([FromBody] SaveResultRequest request, CancellationToken token)
    {
        var saveResultAndGetNextQuestion = await surveyService.SaveResultAndGetNextQuestion(request.SessionId,
                                                request.QuestionId, request.AnswerIds,
                                                token);
        return saveResultAndGetNextQuestion;
    }
}