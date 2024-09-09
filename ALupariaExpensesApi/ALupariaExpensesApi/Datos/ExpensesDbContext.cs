using Microsoft.EntityFrameworkCore;

namespace ALupariaExpensesApi.Datos
{
    public class ExpensesDbContext: DbContext
    {
        public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options):base(options) { }
    }
}
