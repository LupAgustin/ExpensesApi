using System;
using System.Collections.Generic;

namespace ApiExpenses.Models;

public partial class Category
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
