using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNH_Client.DTOs;
using QLNH_Client.Models;
using QLNH_Client.Repositories;

namespace QLNH_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly IPriceRepository _priceRepository;
        private readonly IMapper _mapper;

        public PricesController(IPriceRepository priceRepository, IMapper mapper)
        {
            _priceRepository = priceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<PriceDTO>> GetAllPrice()
        {
            var getAllPrice = await _priceRepository.GetAllAsync();
            return _mapper.Map<List<PriceDTO>>(getAllPrice);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PriceDTO>> GetPriceById(int id)
        {
            var getPrice = await _priceRepository.GetByIdAsync(id);
            if (getPrice == null) return BadRequest("Price is not existed!");
            return _mapper.Map<PriceDTO>(getPrice);
        }

        [HttpPost]
        public async Task<ActionResult<PriceDTO>> CreatePrice(PriceDTO price)
        {
            var newPrice = await _priceRepository.CreateAsync(_mapper.Map<Price>(price));
            return _mapper.Map<PriceDTO>(newPrice);
        }

        [HttpPut]
        public async Task<ActionResult<PriceDTO>> UpdatePrice(PriceDTO price)
        {
            if (await _priceRepository.GetByIdAsync(price.Id) == null) return BadRequest("Price is not existed!");
            var updatedPrice = await _priceRepository.UpdateAsync(_mapper.Map<Price>(price));
            return _mapper.Map<PriceDTO>(updatedPrice);
        }

        [HttpPut("RestorePrice/{id}")]
        public async Task<ActionResult<PriceDTO>> RestorePrice(int id)
        {
            if (await _priceRepository.GetByIdAsync(id) == null) return BadRequest("Price is not existed!");
            var restoredPrice = await _priceRepository.RestoreAsync(id);
            return _mapper.Map<PriceDTO>(restoredPrice);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PriceDTO>> DeletePrice(int id)
        {
            if (await _priceRepository.GetByIdAsync(id) == null) return BadRequest("Price is not existed!");
            var deletedPrice = await _priceRepository.DeleteAsync(id);
            return _mapper.Map<PriceDTO>(deletedPrice);
        }
    }
}
