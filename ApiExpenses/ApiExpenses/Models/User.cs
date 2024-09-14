using System;
using System.Collections.Generic;

namespace ApiExpenses.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
