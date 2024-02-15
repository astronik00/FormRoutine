namespace Domain;

/// <summary>
/// Класс исключений, возникающих при работе с вопросом анкеты
/// </summary>
/// <param name="s"> Сообщение исключения </param>
public class QuestionException(string s) : ArgumentException(s);