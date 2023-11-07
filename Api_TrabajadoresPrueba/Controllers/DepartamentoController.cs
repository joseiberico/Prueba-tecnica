using Dtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Api_TrabajadoresPrueba.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : Controller
    {
        private readonly IRepositoryBase<Departamento> _repository;

        public DepartamentoController(IRepositoryBase<Departamento> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartamentos()
        {
            var departamento = await _repository.GetAll();
            var datos = departamento.Select(p => new DepartamentoDto
            {
                Id = p.Id,
                NombreDepartamento = p.NombreDepartamento
            });

        return Ok (datos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DepartamentoDto))]
        public async Task<IActionResult> CreateDepartamentos(DepartamentoDto departamentoDto)
        {
            Departamento departamento = new Departamento
            {
                Id = departamentoDto.Id,
                NombreDepartamento = departamentoDto.NombreDepartamento
            };

            Departamento result = await _repository.CreateAsync(departamento);
            return new CreatedResult($"https://localhost:7195/api/Departamento/{result.Id}", null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteDepartamentos(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return new OkObjectResult(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartamentoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDepartamentos(DepartamentoDto departamentoDto)
        {
            Departamento departamento = new Departamento
            {
                Id = departamentoDto.Id,
                NombreDepartamento = departamentoDto.NombreDepartamento

            };

            // Llama al método UpdateAsync en el repositorio
            Departamento result = await _repository.UpdateAsync(departamento);

            if (result == null)
                return new NotFoundResult();
            else
            {
                // Convierte la entidad Departamento actualizada a un DepartamentoDto
                DepartamentoDto updatedDto = new DepartamentoDto
                {
                    Id = result.Id,
                    NombreDepartamento = result.NombreDepartamento
                };

                return new OkObjectResult(updatedDto);
            }

        }

    }
}
