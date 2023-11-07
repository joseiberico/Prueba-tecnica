using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dtos;

public class DistritoDto
{
    public int Id { get; set; }

    public int? IdProvincia { get; set; }

    public string? NombreDistrito { get; set; }
    [JsonIgnore]

    public virtual ProvinciaDto? IdProvinciaNavigation { get; set; }
    [JsonIgnore]

    public virtual ICollection<TrabajadorDto> Trabajadores { get; set; } = new List<TrabajadorDto>();
}
