using System;
using System.Collections.Generic;

namespace MvcSignalR.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Dept { get; set; }
}
