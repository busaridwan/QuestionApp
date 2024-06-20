using Microsoft.AspNetCore.Mvc;
using QuestionApp.Api.Models;
using QuestionApp.Api.Services;

namespace QuestionApp.Api.Controllers;

[ApiController]
[Route("[controler]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;
    public QuestionsController(IQuestionRepository questionRepository){
        _questionRepository = questionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(){
        var results = await _questionRepository.GetQuestionsAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, QuestionType type){
        var result = await _questionRepository.GetQuestionAsync(id, type);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Question question){
        var result = await _questionRepository.CreateQuestionAsync(question);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] Question question){
        var result = await _questionRepository.UpdateQuestionAsync(id, question);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, QuestionType type){
        var result = await _questionRepository.DeleteQuestion(id, type);
        if (result)
            return NoContent();

        return BadRequest();    
    }
}