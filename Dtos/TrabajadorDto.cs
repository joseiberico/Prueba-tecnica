using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dtos;

public class TrabajadorDto
{
    public int Id { get; set; }

    public string? TipoDocumento { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? Nombres { get; set; }

    public string? Sexo { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdProvincia { get; set; }

    public int? IdDistrito { get; set; }
    [JsonIgnore]
    public virtual DepartamentoDto? IdDepartamentoNavigation { get; set; }
    [JsonIgnore]

    public virtual DistritoDto? IdDistritoNavigation { get; set; }
    [JsonIgnore]

    public virtual ProvinciaDto? IdProvinciaNavigation { get; set; }
}
