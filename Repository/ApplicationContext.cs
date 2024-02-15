using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Options;

namespace Repository;

/// <summary>
/// Класс, предоставляющий контекст БД
/// </summary>
public class ApplicationContext(IOptions<DbConnectionOptions> dbConnectionOptions) : DbContext
{
    private readonly DbConnectionOptions _options = dbConnectionOptions.Value;

    public DbSet<Answer> Answers { get; set; }

    public DbSet<Interview> Interviews { get; set; }

    public DbSet<Person> People { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Result> Results { get; set; }

    public DbSet<Survey> Surveys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_trgm");

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("answer_pkey");

            entity.ToTable("answer");

            entity.HasIndex(e => e.QuestionId, "idx_answer_question_id");

            entity.Property(e => e.Id)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");
            entity.Property(e => e.Content)
                  .HasColumnType("character varying")
                  .HasColumnName("content");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                  .HasForeignKey(d => d.QuestionId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("answer_question_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.HasIndex(e => e.Name, "idx_category_name")
                  .HasMethod("gin")
                  .HasOperators(new[] { "gin_trgm_ops" });

            entity.Property(e => e.Id)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");
            entity.Property(e => e.Name)
                  .HasColumnType("character varying")
                  .HasColumnName("name");
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("interview_pkey");

            entity.ToTable("interview");

            entity.HasIndex(e => e.PersonId, "idx_interview_person_id");

            entity.HasIndex(e => e.SurveyId, "idx_interview_survey_id");

            entity.Property(e => e.Id)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.SurveyId).HasColumnName("survey_id");

            entity.HasOne(d => d.PersonNavigation).WithMany(p => p.Interviews)
                  .HasForeignKey(d => d.SurveyId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("interview_survey_id_fkey1");

            entity.HasOne(d => d.SurveyNavigation).WithMany(p => p.Interviews)
                  .HasForeignKey(d => d.SurveyId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("interview_survey_id_fkey");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_pkey");

            entity.ToTable("person");

            entity.Property(e => e.Id)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");
            entity.Property(e => e.Email)
                  .HasColumnType("character varying")
                  .HasColumnName("email");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("question_pkey");

            entity.ToTable("question");

            entity.HasIndex(e => e.OrderNo, "idx_question_orderno").IsUnique();

            entity.HasIndex(e => e.SurveyId, "idx_question_survey_id");

            entity.Property(e => e.Id)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");
            entity.Property(e => e.AllowedManyAnswers).HasColumnName("allowed_many_answers");
            entity.Property(e => e.Mandatory).HasColumnName("mandatory");
            entity.Property(e => e.Name)
                  .HasColumnType("character varying")
                  .HasColumnName("name");
            entity.Property(e => e.OrderNo).HasColumnName("orderno");
            entity.Property(e => e.SurveyId).HasColumnName("survey_id");

            entity.HasOne(d => d.Survey).WithMany(p => p.Questions)
                  .HasForeignKey(d => d.SurveyId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("question_survey_id_fkey");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("result_pkey");

            entity.ToTable("result");

            entity.HasIndex(e => e.AnswerId, "idx_result_answer_id");

            entity.HasIndex(e => e.InterviewId, "idx_result_interview_id");

            entity.Property(e => e.Id)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");
            entity.Property(e => e.AnswerId).HasColumnName("answer_id");
            entity.Property(e => e.InterviewId).HasColumnName("interview_id");

            entity.HasOne(d => d.Answer).WithMany(p => p.Results)
                  .HasForeignKey(d => d.AnswerId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("result_answer_id_fkey");

            entity.HasOne(d => d.Interview).WithMany(p => p.Results)
                  .HasForeignKey(d => d.InterviewId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("result_interview_id_fkey");
        });

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("survey_pkey");

            entity.ToTable("survey");

            entity.HasIndex(e => e.CategoryId, "idx_survey_category_id");

            entity.Property(e => e.Id)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                  .HasColumnType("character varying")
                  .HasColumnName("description");
            entity.Property(e => e.Name)
                  .HasColumnType("character varying")
                  .HasColumnName("name");
            entity.Property(e => e.OnlyOnce).HasColumnName("only_once");
            entity.Property(e => e.QuestionNumber).HasColumnName("question_number");

            entity.HasOne(d => d.Category).WithMany(p => p.Surveys)
                  .HasForeignKey(d => d.CategoryId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("survey_category_id_fkey");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"Host={_options.Host};" +
                                 $"Port={_options.Port};" +
                                 $"Database={_options.Database};" +
                                 $"Username={_options.Username};" +
                                 $"Password={_options.Password}");
    }
}