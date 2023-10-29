using System;
using System.Collections.Generic;

namespace MvcSignalR.Entities;

public partial class HubConnection
{
    public int Id { get; set; }

    public string? ConnectionId { get; set; }

    public string? Username { get; set; }
}
