using Microsoft.AspNetCore.Mvc;
using QuestionApp.Api.Models;
using QuestionApp.Api.Services;

namespace QuestionApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeResponsesController : ControllerBase
{
    private readonly IEmployeeResponseRepository _employeeRepository;

    public EmployeeResponsesController(IEmployeeResponseRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeResponse([FromBody] EmployeeResponse employeeResponse)
    {
        await _employeeRepository.AddEmployeeResponseAsync(employeeResponse);
        return CreatedAtAction(nameof(GetEmployeeResponse), new { id = employeeResponse.Id }, employeeResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeResponse(string id)
    {
        var response = await _employeeRepository.GetEmployeeResponseAsync(id);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployeeResponses()
    {
        var responses = await _employeeRepository.GetAllEmployeeResponsesAsync();
        return Ok(responses);
    }
}
