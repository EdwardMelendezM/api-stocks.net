using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interface;
using api.interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(
            UserManager<AppUser> userManager,
            IStockRepository stockRepository,
            IPortfolioRepository portfolioRepository
            )
        {
            _userManager = userManager;
            _stockRepository  = stockRepository;
            _portfolioRepository = portfolioRepository;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetuserPortfolio()
        {
            var username = User.GetUseName();
            if (username == null) return Unauthorized();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null) return Unauthorized();

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUseName();
            if (username == null) return Unauthorized();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null) return Unauthorized();

            var stock = await _stockRepository.GetStockBySymbol(symbol);
            if (stock == null) return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if(userPortfolio.Any(p => p.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Stock already in portfolio");

            var portfolio = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };

            await _portfolioRepository.AddStockToPortfolio(portfolio);

            if (portfolio == null) return StatusCode(500, "Failed to add stock to portfolio");

            return Ok(portfolio);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> RemovePortfolio(string symbol)
        {
            var username = User.GetUseName();
            if (username == null) return Unauthorized();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null) return Unauthorized();

            var stock = await _stockRepository.GetStockBySymbol(symbol);
            if (stock == null) return BadRequest("Stock not found");

            var stocks = await _portfolioRepository.GetUserPortfolio(appUser);
            if(!stocks.Any(p => p.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Stock not in portfolio");

            var filteredStock = stocks.Where(p => p.Symbol.ToLower() == symbol.ToLower());
            
            if (filteredStock.Count() == 1){
                await _portfolioRepository.RemoveStockFromPortfolio(appUser, symbol);
            }
            else  {
                return BadRequest("Stock not in your portfolio");
            }
            return Ok();
        }
    }
}