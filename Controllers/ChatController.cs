namespace ChatBotApp.Controllers;

using ChatBotApp.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly ChatBotService _chatBot;

    public ChatController(ChatBotService chatBot)
    {
        _chatBot = chatBot;
    }

    [HttpGet("ask")]
    public IActionResult Ask([FromQuery] string question)
    {
        var answer = _chatBot.GetResponse(question);
        return Ok(new { question, answer });
    }
}


