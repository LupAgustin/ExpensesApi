using System;
using System.Collections.Generic;

namespace ALupariaExpensesApi.Models;

public partial class User
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public DateTime CreatedAt { get; set; }
}
