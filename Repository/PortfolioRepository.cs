using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> AddStockToPortfolio(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return  portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(p => p.AppUserId == user.Id)
                .Select(p => p.Stock)
                .ToListAsync();
        }

        public async Task<Portfolio?> RemoveStockFromPortfolio(AppUser user, string symbol)
        {
            var portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(p => p.AppUserId == user.Id && p.Stock.Symbol == symbol);
            if (portfolio == null) return null;
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }
    }
}