using QuestionApp.Api.Models;

namespace QuestionApp.Api.Services;
public interface IQuestionRepository
{
    Task<IEnumerable<Question>> GetQuestionsAsync();
    Task<Question> GetQuestionAsync(string id, QuestionType type);
    Task<Question> CreateQuestionAsync(Question question);
    Task<Question> UpdateQuestionAsync(string id, Question question);
    Task<bool> DeleteQuestion(string id, QuestionType type);
}