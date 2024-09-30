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

            var userPortfolio = _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
    }
}