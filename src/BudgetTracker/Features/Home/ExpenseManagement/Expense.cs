using SQLite;

namespace BudgetTracker.Features.Home.ExpenseManagement
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string AmountAsString { get; set; }
    }
}
