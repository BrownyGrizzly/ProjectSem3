using Microsoft.EntityFrameworkCore;
using OnlineRailroad.Data;
using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public class FareRuleService : IFareRuleService
    {
        private readonly AppDbContext _context;

        public FareRuleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FareRule>> GetAllFareRulesAsync()
        {
            return await _context.FareRules.ToListAsync();
        }

        public async Task<FareRule> GetFareRuleByIdAsync(int id)
        {
            return await _context.FareRules.FindAsync(id);
        }

        public async Task AddFareRuleAsync(FareRule fareRule)
        {
            if (fareRule == null) throw new ArgumentNullException(nameof(fareRule));
            // Add any additional validation as needed

            _context.FareRules.Add(fareRule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFareRuleAsync(FareRule fareRule)
        {
            if (fareRule == null) throw new ArgumentNullException(nameof(fareRule));
            if (!await FareRuleExists(fareRule.FareRuleID)) throw new KeyNotFoundException("Fare rule not found.");

            _context.FareRules.Update(fareRule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFareRuleAsync(int id)
        {
            var fareRule = await _context.FareRules.FindAsync(id);
            if (fareRule != null)
            {
                _context.FareRules.Remove(fareRule);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Fare rule not found.");
            }
        }

        private async Task<bool> FareRuleExists(int id)
        {
            return await _context.FareRules.AnyAsync(e => e.FareRuleID == id);
        }
    }
}
