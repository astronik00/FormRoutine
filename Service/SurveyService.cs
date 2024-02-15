using Domain.Entities;
using Repository;

namespace Service;

/// <summary>
/// Базовая реализация сервиса для работы с репозиторием
/// </summary>
public sealed class SurveyService(ISurveyRepository surveyRepository) : ISurveyService
{
    private const long NoNextQuestion = -1;

    /// <summary>
    /// Получение данных вопроса
    /// </summary>
    /// <param name="questionId"> Идентификатор вопроса </param>
    /// <param name="token"> Токен отмены </param>
    /// <returns> Вопрос с заданным идентификатором </returns>
    public async Task<Question?> GetQuestion(long questionId, CancellationToken token) =>
        await surveyRepository.GetQuestion(questionId, token);

    /// <summary>
    /// Сохранение результатов и получение следующего вопроса
    /// </summary>
    /// <param name="interviewId"> Идентификатор сессии </param>
    /// <param name="questionId"> Идентификатор вопроса </param>
    /// <param name="answerIds"> Список идентификаторов выбранных ответов</param>
    /// <param name="token"> Токен отмены </param>
    /// <returns> Идентификатор следующего вопроса или -1, если больше вопросов нет </returns>
    public async Task<long?> SaveResultAndGetNextQuestion(long interviewId, long questionId,
                                                          ICollection<long> answerIds,
                                                          CancellationToken token) =>
        await surveyRepository.SaveResult(interviewId, questionId, answerIds, token) ?? NoNextQuestion;
}