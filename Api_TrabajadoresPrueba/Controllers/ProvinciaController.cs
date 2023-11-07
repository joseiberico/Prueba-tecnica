using Dtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Api_TrabajadoresPrueba.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvinciaController : Controller
    {
        private readonly IRepositoryBase<Provincia> _repository;

        public ProvinciaController(IRepositoryBase<Provincia> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProvincias()
        {
            var provincia = await _repository.GetAll();
            var datos = provincia.Select(p => new ProvinciaDto
            {
                Id = p.Id,
                IdDepartamento = p.Id,
                NombreProvincia = p.NombreProvincia
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProvinciaDto))]
        public async Task<IActionResult> CreateProvincias(ProvinciaDto provinciaDto)
        {
            Provincia provincia = new Provincia
            {
                Id = provinciaDto.Id,
                IdDepartamento = provinciaDto.IdDepartamento,
                NombreProvincia = provinciaDto.NombreProvincia
            };
            Provincia result = await _repository.CreateAsync(provincia);
            return new CreatedResult($"https://localhost:7195/api/Provincia/{result.Id}", null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteProvincias(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return new OkObjectResult(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProvinciaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProvincias(ProvinciaDto provinciaDto)
        {
            Provincia provincia = new Provincia
            {
                Id = provinciaDto.Id,
                IdDepartamento = provinciaDto.IdDepartamento,
                NombreProvincia = provinciaDto.NombreProvincia

            };

            // Llama al método UpdateAsync en el repositorio
            Provincia result = await _repository.UpdateAsync(provincia);

            if (result == null)
                return new NotFoundResult();
            else
            {
                // Convierte la entidad Provincia actualizada a un ProvinciaDto
                ProvinciaDto updatedDto = new ProvinciaDto
                {
                    Id = result.Id,
                    IdDepartamento = result.IdDepartamento,
                    NombreProvincia = result.NombreProvincia
                };

                return new OkObjectResult(updatedDto);
            }


        }
    }
}
