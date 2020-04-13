using BudgetTracker.Features.Home.BudgetManagement;
using BudgetTracker.Features.Home.ExpenseManagement;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetTracker
{
    public class BudgetTrackerDb
    {
        private readonly SQLiteAsyncConnection _database;

        public BudgetTrackerDb(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Budget>().Wait();
            _database.CreateTableAsync<Expense>().Wait();
        }

        public async Task<int> SaveBudgetAsync(Budget newBudget)
        {
            var result = await _database.InsertAsync(newBudget);

            return result;
        }

        public async Task<decimal> GetBudgetAsync()
        {
            // This is nasty but as we're only going to have one budget for now so we'll let it slide
            var result = await _database.Table<Budget>().FirstOrDefaultAsync(x => x.Amount > 0);

            return result?.Amount ?? 0;
        }
        public async Task<int> SaveExpenseAsync(Expense newExpense)
        {
            var result = await _database.InsertAsync(newExpense);

            return result;
        }

        public Task<List<Expense>> GetExpensesAsync()
        {
            return _database.Table<Expense>().ToListAsync();
        }

    }
}
