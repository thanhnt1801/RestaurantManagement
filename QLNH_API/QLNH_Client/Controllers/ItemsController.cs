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
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemsController(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ItemDTO>> GetAllItem()
        {
            var getAllItem = await _itemRepository.GetAllAsync();
            return _mapper.Map<List<ItemDTO>>(getAllItem);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemById(int id)
        {
            var getItem = await _itemRepository.GetByIdAsync(id);
            if (getItem == null) return BadRequest("Item is not existed!");
            return _mapper.Map<ItemDTO>(getItem);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItem(ItemDTO item)
        {
            var newItem = await _itemRepository.CreateAsync(_mapper.Map<Item>(item));
            return _mapper.Map<ItemDTO>(newItem);
        }

        [HttpPut]
        public async Task<ActionResult<ItemDTO>> UpdateItem(ItemDTO item)
        {
            if (await _itemRepository.GetByIdAsync(item.Id) == null) return BadRequest("Item is not existed!");
            var updatedItem = await _itemRepository.UpdateAsync(_mapper.Map<Item>(item));
            return _mapper.Map<ItemDTO>(updatedItem);
        }

        [HttpPut("RestoreItem/{id}")]
        public async Task<ActionResult<ItemDTO>> RestoreItem(int id)
        {
            if (await _itemRepository.GetByIdAsync(id) == null) return BadRequest("Item is not existed!");
            var restoredItem = await _itemRepository.RestoreAsync(id);
            return _mapper.Map<ItemDTO>(restoredItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemDTO>> DeleteItem(int id)
        {
            if (await _itemRepository.GetByIdAsync(id) == null) return BadRequest("Item is not existed!");
            var deletedItem = await _itemRepository.DeleteAsync(id);
            return _mapper.Map<ItemDTO>(deletedItem);
        }
    }
}
