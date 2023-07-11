using System;
using System.ComponentModel.DataAnnotations;

namespace CSClub.Data;

public class CredentialModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
