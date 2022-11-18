using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfull.API.Models;
using RESTfull.Domain;
using RESTfull.Infrastructure;
using RESTfull.Infrastructure.Repositories;

namespace RESTfull.API.Controllers;

[ApiController]
[Authorize]
[Route("api/chats")]
public class ChatsController : ControllerBase
{
    private readonly IRepository<Chat> _chatRepository;

    private readonly IRepository<Message> _messageRepository;

    private readonly IRepository<User> _userRepository;

    public ChatsController(
        IRepository<Chat> chatRepository,
        IRepository<Message> messageRepository,
        IRepository<User> userRepository
    )
    {
        _chatRepository = chatRepository;
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }


    [HttpPost]
    public async Task<ActionResult<Chat>> CreateChat([FromBody] ChatModels.CreateChatRequest chat)
    {
        _chatRepository.Add(new Chat() { Name = chat.Name });
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
    {
        return Ok(_chatRepository.GetAll().ToArray());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Chat>> GetChat(Guid id)
    {
        var chat = _chatRepository.Get(c => c.Id == id);

        if (chat == null)
        {
            return NotFound();
        }

        return chat;
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Chat>> DeleteChat(Guid id)
    {
        var chat = _chatRepository.Get(c => c.Id == id);


        if (chat == null)
        {
            return NotFound();
        }

        _chatRepository.Delete(chat);
        return Ok();
    }

    [HttpPost("{id}/messages")]
    public async Task<ActionResult<Chat>> CreateMessage(Guid id, [FromBody] ChatModels.CreateMessageRequest message)
    {
        var author = _userRepository.Get(u => u.Username == User.Identity.Name);
        var chat = _chatRepository.Get(c => c.Id == id);
        if (chat == null)
        {
            return NotFound();
        }

        chat.Messages.Add(new Message() { Text = message.Text, Author = author });
        _chatRepository.SaveChanges();
        return Ok();
    }
}