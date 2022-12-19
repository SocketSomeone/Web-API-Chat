using Refit;
using RESTfull.Client.Models;

namespace RESTfull.Client.Interfaces;

[Headers("Accept: application/json")]
public interface IUsersClient
{
    [Get("/api/users/@me")]
    public Task<UserModel> GetCurrentUserAsync([Header("Authorization")] string token);

    [Post("/api/users")]
    public Task CreateUserAsync([Body] RegisterModel user);
}