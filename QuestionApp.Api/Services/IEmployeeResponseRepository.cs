using QuestionApp.Api.Models;

namespace QuestionApp.Api.Services;
public interface IEmployeeResponseRepository
{
    Task AddEmployeeResponseAsync(EmployeeResponse employeeResponse);
    Task<EmployeeResponse> GetEmployeeResponseAsync(string id);
    Task<IEnumerable<EmployeeResponse>> GetAllEmployeeResponsesAsync();
}
