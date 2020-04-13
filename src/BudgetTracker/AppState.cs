using BudgetTracker.Features.Home.BudgetManagement;
using BudgetTracker.Features.Home.ExpenseManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker
{
    public class AppState
    {
        private readonly BudgetTrackerDb _budgetTrackerDb;

        public AppState()
        {
            _budgetTrackerDb = new BudgetTrackerDb(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BudgetTrackerDb.db3"));
        }

        public event Func<Task> OnChange;

        public async Task SetBudget(decimal newBudget)
        {
            _ = await _budgetTrackerDb.SaveBudgetAsync(new Budget { Amount = newBudget });
            await NotifyStateChanged();
        }

        public async Task<decimal> GetBudget() => await _budgetTrackerDb.GetBudgetAsync();

        public async Task AddExpense(Expense newExpense)
        {
            _ = await _budgetTrackerDb.SaveExpenseAsync(newExpense);
            await NotifyStateChanged();
        }

        public async Task<IReadOnlyList<Expense>> GetExpenses() => await _budgetTrackerDb.GetExpensesAsync();

        public async Task<decimal> GetCurrentBalance()
        {
            var budget = await GetBudget();
            var expenses = await GetExpenses();

            return budget - expenses.Sum(x => x.Amount);
        }

        public async Task<decimal> GetTotalExpenses()
        {
            var expenses = await GetExpenses();

            return expenses.Sum(x => x.Amount);
        }

        private async Task NotifyStateChanged() => await OnChange?.Invoke();
    }
}
