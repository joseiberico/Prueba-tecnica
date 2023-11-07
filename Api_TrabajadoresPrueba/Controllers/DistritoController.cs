using Dtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Api_TrabajadoresPrueba.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistritoController : Controller
    {
        private readonly IRepositoryBase<Distrito> _repository;

        public DistritoController(IRepositoryBase<Distrito> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDistritos()
        {
            var distrito = await _repository.GetAll();
            var datos = distrito.Select(p => new DistritoDto
            {
                Id = p.Id,
                IdProvincia = p.Id,
                NombreDistrito = p.NombreDistrito
            });
            return Ok(datos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DistritoDto))]
        public async Task<IActionResult> CreateDistritos(DistritoDto distritoDto)
        {
            Distrito distrito = new Distrito
            {
                Id = distritoDto.Id,
                IdProvincia = distritoDto.IdProvincia,
                NombreDistrito = distritoDto.NombreDistrito
            };

            Distrito result = await _repository.CreateAsync(distrito);
            return new CreatedResult($"https://localhost:7195/api/Distrito/{result.Id}", null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteDistritos(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return new OkObjectResult(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DistritoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDistritos(DistritoDto distritoDto)
        {
            Distrito distrito = new Distrito
            {
                Id = distritoDto.Id, 
                IdProvincia = distritoDto.IdProvincia,
                NombreDistrito = distritoDto.NombreDistrito

            };

            // Llama al método UpdateAsync en el repositorio
            Distrito result = await _repository.UpdateAsync(distrito);

            if (result == null)
                return new NotFoundResult();
            else
            {
                // Convierte la entidad Distrito actualizada a un DitritoDto
                DistritoDto updatedDto = new DistritoDto
                {
                    Id = result.Id,
                    IdProvincia = result.IdProvincia,
                    NombreDistrito = result.NombreDistrito
                };

                return new OkObjectResult(updatedDto);
            }

        }
    }
}
