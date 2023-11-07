using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dtos;

public class ProvinciaDto
{
    public int Id { get; set; }

    public int? IdDepartamento { get; set; }

    public string? NombreProvincia { get; set; }

    [JsonIgnore]

    public virtual ICollection<DistritoDto> Distritos { get; set; } = new List<DistritoDto>();
    [JsonIgnore]

    public virtual DepartamentoDto? IdDepartamentoNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<TrabajadorDto> Trabajadores { get; set; } = new List<TrabajadorDto>();
}
