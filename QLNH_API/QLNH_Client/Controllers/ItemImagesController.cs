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
    public class ItemImagesController : ControllerBase
    {
        private readonly IItemImageRepository _itemImageRepository;
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public ItemImagesController(IItemImageRepository itemImageRepository, IMapper mapper, IItemRepository itemRepository)
        {
            _itemImageRepository = itemImageRepository;
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<List<ItemImageDTO>> GetAllItemImage()
        {
            var getAllItemImage = await _itemImageRepository.GetAllAsync();
            return _mapper.Map<List<ItemImageDTO>>(getAllItemImage);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemImageDTO>> GetItemImageById(int id)
        {
            var getItemImage = await _itemImageRepository.GetByIdAsync(id);
            if (getItemImage == null) return BadRequest("ItemImage is not existed!");
            return _mapper.Map<ItemImageDTO>(getItemImage);
        }

        [HttpPost]
        public async Task<ActionResult<ItemImageDTO>> CreateItemImage(ItemImageDTO itemImage)
        {
            if (await _itemRepository.GetByIdAsync((int)itemImage.ItemId) == null) return BadRequest("Item is not existed!");
            var newItemImage = await _itemImageRepository.CreateAsync(_mapper.Map<ItemImage>(itemImage));
            return _mapper.Map<ItemImageDTO>(newItemImage);
        }

        [HttpPut]
        public async Task<ActionResult<ItemImageDTO>> UpdateItemImage(ItemImageDTO itemImage)
        {
            if (await _itemRepository.GetByIdAsync((int)itemImage.ItemId) == null) return BadRequest("Item is not existed!");
            if (await _itemImageRepository.GetByIdAsync(itemImage.Id) == null) return BadRequest("ItemImage is not existed!");
            var updatedItemImage = await _itemImageRepository.UpdateAsync(_mapper.Map<ItemImage>(itemImage));
            return _mapper.Map<ItemImageDTO>(updatedItemImage);
        }

        [HttpPut("RestoreItemImage/{id}")]
        public async Task<ActionResult<ItemImageDTO>> RestoreItemImage(int id)
        {
            if (await _itemImageRepository.GetByIdAsync(id) == null) return BadRequest("ItemImage is not existed!");
            var restoredItemImage = await _itemImageRepository.RestoreAsync(id);
            return _mapper.Map<ItemImageDTO>(restoredItemImage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemImageDTO>> DeleteItemImage(int id)
        {
            if (await _itemImageRepository.GetByIdAsync(id) == null) return BadRequest("ItemImage is not existed!");
            var deletedItemImage = await _itemImageRepository.DeleteAsync(id);
            return _mapper.Map<ItemImageDTO>(deletedItemImage);
        }
    }
}
