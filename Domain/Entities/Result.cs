namespace Domain.Entities;

public class Result
{
    public long Id { get; set; }

    public long InterviewId { get; set; }

    public long AnswerId { get; set; }

    public Answer Answer { get; set; } = null!;

    public Interview Interview { get; set; } = null!;
}