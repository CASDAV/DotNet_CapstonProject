using System;
using System.ComponentModel.DataAnnotations;

namespace LogiTrack.API.Entities;

public class User
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

    public string? Role { get; set; }
}
