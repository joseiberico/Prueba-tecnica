using DataAccess.Context;
using Dtos;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementaciones
{
    public class TrabajadorRepository : IRepositoryBase<Trabajador>
    {
        private readonly TrabajadoresPruebaContext _context;
        private readonly DbSet<Trabajador> _dbset;

        private readonly string _cadenaSQL = "";

        public TrabajadorRepository(TrabajadoresPruebaContext context, IConfiguration configuration)
        {
            _context = context;
            _dbset = context.Set<Trabajador>();
            _cadenaSQL = configuration.GetConnectionString("CadenaSql");
        }

        public async Task<Trabajador> CreateAsync(Trabajador entity)
        {
            Trabajador trabajador = new Trabajador()
            {
                TipoDocumento = entity.TipoDocumento,
                NumeroDocumento = entity.NumeroDocumento,
                Nombres = entity.Nombres,
                Sexo = entity.Sexo,
                IdDepartamento = entity.IdDepartamento,
                IdProvincia = entity.IdProvincia,
                IdDistrito = entity.IdDistrito
            };

            EntityEntry<Trabajador> result = await _context.Trabajadores.AddAsync(trabajador);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Trabajador entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Trabajador>> GetAll()
        {
            List<Trabajador> _lista = new List<Trabajador>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_ListadoTrabajadores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Trabajador
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            TipoDocumento = dr["TipoDocumento"].ToString(),
                            NumeroDocumento = dr["NumeroDocumento"].ToString(),
                            Nombres = dr["Nombres"].ToString(),
                            Sexo = dr["Sexo"].ToString(),
                            IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]),
                            IdProvincia = Convert.ToInt32(dr["IdProvincia"]),
                            IdDistrito = Convert.ToInt32(dr["IdDistrito"])
                        });
                    }

                }

            }

            return _lista;
        }

        public async Task<Trabajador> GetByIdAsync(int id)
        {
            return await _dbset.FirstAsync(x=> x.Id == id);
        }

        public async Task<Trabajador> UpdateAsync(Trabajador entity)
        {
            var trabajador = await GetByIdAsync(entity.Id);

            if (trabajador == null)
            {
                return null;
            }

            trabajador.TipoDocumento = entity.TipoDocumento;
            trabajador.NumeroDocumento = entity.NumeroDocumento;
            trabajador.Nombres = entity.Nombres;
            trabajador.Sexo = entity.Sexo;
            trabajador.IdDepartamento = entity.IdDepartamento;
            trabajador.IdProvincia = entity.IdProvincia;
            trabajador.IdDistrito = entity.IdDistrito;


            await _context.SaveChangesAsync();
            return trabajador;

        }

    }
}
