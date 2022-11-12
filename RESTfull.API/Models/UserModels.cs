using System.ComponentModel.DataAnnotations;

namespace RESTfull.API.Models;

public class AuthModels
{
    public class RegisterModel
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}