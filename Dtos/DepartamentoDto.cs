using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dtos;

public class DepartamentoDto
{
    public int Id { get; set; }

    public string? NombreDepartamento { get; set; }

    [JsonIgnore]

    public virtual ICollection<ProvinciaDto> Provincia { get; set; } = new List<ProvinciaDto>();
    [JsonIgnore]

    public virtual ICollection<TrabajadorDto> Trabajadores { get; set; } = new List<TrabajadorDto>();
}
