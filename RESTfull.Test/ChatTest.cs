using RESTfull.Domain;
using RESTfull.Infrastructure.Repositories;
using RESTfull.Test.Fixtures;

namespace RESTfull.Test;

public class ChatTest : IClassFixture<DatabaseFixture>
{
    private readonly Repository<Chat> _repository;

    public ChatTest(DatabaseFixture fixture)
    {
        _repository = new ChatRepository(fixture.Context);
    }

    [Fact]
    public void ShouldCreateChat()
    {
        var chat = new Chat
        {
            Name = "Test Chat"
        };

        _repository.Add(chat);

        Assert.True(_repository.GetAll().ToArray().Length > 0);
    }

    [Fact]
    public void ShouldGetChat()
    {
        var chat = _repository.Get(c => c.Name == "Test Chat");

        Assert.NotNull(chat);
    }

    [Fact]
    public void ShouldUpdateChat()
    {
        var chat = _repository.Get(c => c.Name == "Test Chat");
        chat.Name = "Updated Chat";

        _repository.Update(chat);

        Assert.Equal("Updated Chat", _repository.Get(c => c.Name == "Updated Chat").Name);
    }

    [Fact]
    public void ShouldDeleteChat()
    {
        var chat = _repository.Get(c => c.Name == "Updated Chat");

        _repository.Delete(chat);

        Assert.Null(_repository.Get(c => c.Name == "Updated Chat"));
    }

    [Fact]
    public void ShouldGetAllChats()
    {
        var chats = _repository.GetAll();

        Assert.True(chats.ToArray().Length > 0);
    }
}