using Route = OnlineRailroad.Models.Route;

namespace OnlineRailroad.Services
{
    public interface IRouteService
    {
        Task<List<Route>> GetAllRoutesAsync();
        Task<Route> GetRouteByIdAsync(int id);
        Task AddRouteAsync(Route route);
        Task UpdateRouteAsync(Route route);
        Task DeleteRouteAsync(int id);
    }
}
