using SQLite;

namespace BudgetTracker.Features.Home.BudgetManagement
{
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Amount { get; set; }
    }
}
