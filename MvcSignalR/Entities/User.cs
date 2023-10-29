using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSignalR.Entities;

public partial class User
{
    [Key]
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Dept { get; set; }
}
