﻿namespace ApiExpenses.DTOs
{
    public class ExpenseDtoCreate
    {
        public string CategoryId { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
