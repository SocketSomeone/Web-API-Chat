using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfull.API.Models;
using RESTfull.Domain;
using RESTfull.Infrastructure;

namespace RESTfull.API.Controllers;

[ApiController]
[Authorize]
[Route("api/chats")]
public class ChatsController : ControllerBase
{
    private readonly DataContext _context;

    public ChatsController(DataContext context) => _context = context;

    [HttpPost]
    public async Task<ActionResult<Chat>> CreateChat([FromBody] ChatModels.CreateChatRequest chat)
    {
        _context.Chats.Add(new Chat() { Name = chat.Name });
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
    {
        return Ok(_context.Chats
            .Include(c => c.Messages)
            .ThenInclude(m => m.Author)
            .ToArray());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Chat>> GetChat(Guid id)
    {
        var chat = _context.Chats
            .Include(c => c.Messages)
            .ThenInclude(m => m.Author)
            .FirstOrDefault(c => c.Id == id);

        if (chat == null)
        {
            return NotFound();
        }

        return chat;
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult<Chat>> UpdateChat(Guid id, [FromBody] ChatModels.UpdateChatRequest chat)
    {
        var chatToUpdate = _context.Chats.FirstOrDefault(c => c.Id == id);

        if (chatToUpdate == null)
        {
            return NotFound();
        }

        chatToUpdate.Name = chat.Name;
        await _context.SaveChangesAsync();

        return chatToUpdate;
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Chat>> DeleteChat(Guid id)
    {
        var chat = _context.Chats.FirstOrDefault(c => c.Id == id);

        if (chat == null)
        {
            return NotFound();
        }

        _context.Chats.Remove(chat);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{id}/messages")]
    public async Task<ActionResult<Chat>> CreateMessage(Guid id, [FromBody] ChatModels.CreateMessageRequest message)
    {
        var author = _context.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
        var chat = _context.Chats.FirstOrDefault(c => c.Id == id);
        if (chat == null)
        {
            return NotFound();
        }

        chat.Messages.Add(new Message() { Text = message.Text, Author = author });
        await _context.SaveChangesAsync();
        return Ok();
    }
}