using System.ComponentModel.DataAnnotations;
namespace Spotify.Models
{
    public class LoginViewModel
{
    [Required]
    public string? UsernameOrEmail { get; set; }

    [Required]
    public string? Password { get; set; }
}

}