using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSignalR.Entities;

public partial class HubConnection
{
    [Key]
    public int Id { get; set; }

    public string? ConnectionId { get; set; }

    public string? Username { get; set; }
}
