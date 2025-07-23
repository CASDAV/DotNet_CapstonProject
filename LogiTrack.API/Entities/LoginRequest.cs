using System;
using System.ComponentModel.DataAnnotations;

namespace LogiTrack.API.Entities;

public class LoginRequest
{
    [Required]
    public required string UserName { get; set; }
    [Required]
    public required string Password { get; set; }
}
