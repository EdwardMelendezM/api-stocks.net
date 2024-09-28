using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Stock>> GetStocks(QueryObject query)
        {
            var stocks = _context.Stocks.AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsAscdening ? stocks.OrderBy(s => s.Symbol) : stocks.OrderByDescending(s => s.Symbol);
                }
                if(query.SortBy.Equals("companyName", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsAscdening ? stocks.OrderBy(s => s.CompanyName) : stocks.OrderByDescending(s => s.CompanyName);
                }
            }
            var skip = (query.Page - 1) * query.PageSize;
            
            return stocks.Include(c => c.Comments).Skip(skip).Take(query.PageSize).ToListAsync();
        }
       

        public async Task<Stock> CreateStock(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> DeleteStock(int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if(stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock> GetStockById(int id)
        {
            var stockModel =  await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
            return stockModel;
        }

        public async Task<Stock> UpdateStock(int id, UpdateStockRequestDto stock)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if(existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stock.Symbol;
            existingStock.CompanyName = stock.CompanyName;
            existingStock.Purchase = stock.Purchase;
            existingStock.LastDiv = stock.LastDiv;
            existingStock.Industry = stock.Industry;
            existingStock.MarketCap = stock.MarketCap;
            await _context.SaveChangesAsync();
            return existingStock;

        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}