using Microsoft.EntityFrameworkCore;
using OnlineRailroad.Data;
using Route = OnlineRailroad.Models.Route;

namespace OnlineRailroad.Services
{
    public class RouteService : IRouteService
    {
        private readonly AppDbContext _context;

        public RouteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Route>> GetAllRoutesAsync()
        {
            return await _context.Routes.ToListAsync();
        }

        public async Task<Route> GetRouteByIdAsync(int id)
        {
            return await _context.Routes.FindAsync(id);
        }

        public async Task AddRouteAsync(Route route)
        {
            // Validate the route object
            if (route == null) throw new ArgumentNullException(nameof(route));
            // Add any additional validation as needed

            _context.Routes.Add(route);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRouteAsync(Route route)
        {
            // Validate the route object
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (!await RouteExists(route.RouteID)) throw new KeyNotFoundException("Route not found.");

            _context.Routes.Update(route);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRouteAsync(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route != null)
            {
                _context.Routes.Remove(route);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Route not found.");
            }
        }

        private async Task<bool> RouteExists(int id)
        {
            return await _context.Routes.AnyAsync(e => e.RouteID == id);
        }
    }
}
