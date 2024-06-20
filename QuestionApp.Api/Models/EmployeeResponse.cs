public class EmployeeResponse
{
    public string Id { get; set; } // Unique identifier for the response
    public string EmployeeName { get; set; }
    public string EmployeeEmail { get; set; }
    public List<QuestionResponse> Responses { get; set; }
}