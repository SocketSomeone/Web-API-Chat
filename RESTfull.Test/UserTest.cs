using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using RESTfull.Infrastructure;
using RESTfull.Infrastructure.Repositories;
using RESTfull.Test.Fixtures;

namespace Test;

public class UserTest : IClassFixture<DatabaseFixture>
{
    private readonly Repository<User> _repository;

    public UserTest(DatabaseFixture fixture)
    {
        _repository = new UserRepository(fixture.Context);
    }

    [Fact]
    public void Create()
    {
        var user = new User()
        {
            Username = "test",
            Password = "test"
        };

        _repository.Add(user);

        Assert.Equal(_repository.Get(u => u.Username == user.Username), user);
    }

    [Fact]
    public void GetAll()
    {
        var users = _repository.GetAll();
        Assert.Equal(0, users.ToArray().Length);
    }
    
    [Fact]
    public void Get()
    {
        var user = _repository.Get(u => u.Username == "test");
        Assert.Equal("test", user.Username);
    }
    
    [Fact]
    public void Delete()
    {
        var user = _repository.Get(u => u.Username == "test");
        
        if (user != null)
        {
            _repository.Delete(user);
        }
        
        Assert.Null(_repository.Get(u => u.Username == "test"));
        Assert.Equal(0, _repository.GetAll().ToArray().Length);
    }
}