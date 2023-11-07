using Dtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Api_TrabajadoresPrueba.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrabajadorController : Controller
    {
        private readonly IRepositoryBase<Trabajador> _repository;

        public TrabajadorController(IRepositoryBase<Trabajador> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrabajadores()
        {
            var trabajadores = await _repository.GetAll();
            var datos = trabajadores.Select(p => new TrabajadorDto
            {
                Id = p.Id,
                TipoDocumento = p.TipoDocumento,
                NumeroDocumento = p.NumeroDocumento,
                Nombres = p.Nombres,
                Sexo = p.Sexo,
                IdDepartamento = p.IdDepartamento,
                IdProvincia = p.IdProvincia,
                IdDistrito = p.IdDistrito
            });
            return Ok(datos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trabajador = await _repository.GetByIdAsync(id);
            return Ok(trabajador);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrabajadorDto))]
        public async Task<IActionResult> CreateTrabajadores(TrabajadorDto trabajadorDto)
        {
            Trabajador trabajador = new Trabajador
            {
                TipoDocumento = trabajadorDto.TipoDocumento,
                NumeroDocumento = trabajadorDto.NumeroDocumento,
                Nombres = trabajadorDto.Nombres,
                Sexo = trabajadorDto.Sexo,
                IdDepartamento = trabajadorDto.IdDepartamento,
                IdProvincia = trabajadorDto.IdProvincia,
                IdDistrito = trabajadorDto.IdDistrito
            };

            Trabajador result = await _repository.CreateAsync(trabajador);
            return new CreatedResult($"https://localhost:7195/api/Trabajador/{result.Id}", null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteTrabajadores(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return new OkObjectResult(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrabajadorDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTrabajadores(TrabajadorDto trabajadorDto)
        {
            // Convierte el TrabajadorDto a una entidad Trabajador
            Trabajador trabajador = new Trabajador
            {
                Id = trabajadorDto.Id, // Asegúrate de incluir el ID en el DTO
                TipoDocumento = trabajadorDto.TipoDocumento,
                NumeroDocumento = trabajadorDto.NumeroDocumento,
                Nombres = trabajadorDto.Nombres,
                Sexo = trabajadorDto.Sexo,
                IdDepartamento = trabajadorDto.IdDepartamento,
                IdProvincia = trabajadorDto.IdProvincia,
                IdDistrito = trabajadorDto.IdDistrito
            };

            // Llama al método UpdateAsync en el repositorio
            Trabajador result = await _repository.UpdateAsync(trabajador);

            if (result == null)
                return new NotFoundResult();
            else
            {
                // Convierte la entidad Trabajador actualizada a un TrabajadorDto
                TrabajadorDto updatedDto = new TrabajadorDto
                {
                    Id = result.Id,
                    TipoDocumento = result.TipoDocumento,
                    NumeroDocumento = result.NumeroDocumento,
                    Nombres = result.Nombres,
                    Sexo = result.Sexo,
                    IdDepartamento = result.IdDepartamento,
                    IdProvincia = result.IdProvincia,
                    IdDistrito = result.IdDistrito
                };

                return new OkObjectResult(updatedDto);
            }
        }

    }
}
