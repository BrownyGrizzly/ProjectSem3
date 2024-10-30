using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public interface IFareRuleService
    {
        Task<List<FareRule>> GetAllFareRulesAsync();
        Task<FareRule> GetFareRuleByIdAsync(int id);
        Task AddFareRuleAsync(FareRule fareRule);
        Task UpdateFareRuleAsync(FareRule fareRule);
        Task DeleteFareRuleAsync(int id);
    }
}
