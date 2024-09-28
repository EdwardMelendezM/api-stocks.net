using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetStocks();
        Task<Stock> GetStockById(int id);
        Task<Stock> CreateStock(Stock stock);
        Task<Stock> UpdateStock(int id, UpdateStockRequestDto stock);
        Task<Stock> DeleteStock(int id);
        Task<bool> StockExists(int id);
    }
}