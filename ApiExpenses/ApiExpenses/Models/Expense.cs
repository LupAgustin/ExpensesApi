using System;
using System.Collections.Generic;

namespace ApiExpenses.Models;

public partial class Expense
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
