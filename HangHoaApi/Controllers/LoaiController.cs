using HangHoaApi.Entity;
using HangHoaApi.Models;
using HangHoaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangHoaApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private ILoaiRepository _loaiRepository;

        public LoaiController(ILoaiRepository loaiRepository)
        {
            _loaiRepository = loaiRepository;
        }
        [HttpGet("all")]
        public IActionResult GetAllLoai()
        {
            try
            {
                var loai = _loaiRepository.GetAll();
                return Ok(loai);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }
        [HttpGet("{id}")]
        public IActionResult GetByIdLoai(int id)
        {
            try
            {
                var loai = _loaiRepository.GetById(id);
                if (loai != null)
                {
                    return Ok(loai);
                }
                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("timkiem")]
        public IActionResult GetLoai(string search, string sortBy)
        {
            try
            {
                var allloai =_loaiRepository.GetAllLoai(search, sortBy);
                return Ok(allloai);
            }
            catch
            {
                return BadRequest("Ko tim thay loai hnag hoa");

            }
        }
        [HttpPost]
        public IActionResult Create(LoaiModels2 loaiModels2)
        {
            try
            {
                var loai = _loaiRepository.Add(loaiModels2);
                return Ok(loai);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }
        [HttpPut("{id}")]
        public IActionResult Edit(LoaiModels loaiModels,int id)
        {
            try
            {
                if (id == loaiModels.MaLoai)
                {
                    _loaiRepository.Upate(loaiModels);
                    return NoContent();
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }
        [HttpDelete("id")]
        public IActionResult DeleteLoai(int id)
        {
            try
            {
                _loaiRepository.Delete(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }
    }
}
