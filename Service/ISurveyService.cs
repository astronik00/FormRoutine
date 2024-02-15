using Domain.Entities;

namespace Service;

/// <summary>
/// Сервис для работы с репозиторием
/// </summary>
public interface ISurveyService
{
    /// <summary>
    /// Получение данных вопроса
    /// </summary>
    /// <param name="questionId"> Идентификатор вопроса </param>
    /// <param name="token"> Токен отмены </param>
    /// <returns> Вопрос с заданным идентификатором </returns>
    public Task<Question?> GetQuestion(long questionId, CancellationToken token);

    /// <summary>
    /// Сохранение результатов и получение следующего вопроса
    /// </summary>
    /// <param name="interviewId"> Идентификатор сессии </param>
    /// <param name="questionId"> Идентификатор вопроса </param>
    /// <param name="answerIds"> Список идентификаторов выбранных ответов</param>
    /// <param name="token"> Токен отмены </param>
    /// <returns> Идентификатор следующего вопроса </returns>
    public Task<long?> SaveResultAndGetNextQuestion(long interviewId, long questionId, ICollection<long> answerIds,
                                                    CancellationToken token);
}