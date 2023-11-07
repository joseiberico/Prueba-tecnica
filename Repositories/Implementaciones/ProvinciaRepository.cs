using DataAccess.Context;
using Dtos;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementaciones
{
    public class ProvinciaRepository : IRepositoryBase<Provincia>
    {
        private readonly TrabajadoresPruebaContext _context;
        private readonly DbSet<Provincia> _dbset;

        public ProvinciaRepository(TrabajadoresPruebaContext context)
        {
            _context = context;
            _dbset = context.Set<Provincia>();
        }

        public async Task<Provincia> CreateAsync(Provincia entity)
        {
            Provincia provincia = new Provincia()
            {
                IdDepartamento = entity.IdDepartamento,
                NombreProvincia = entity.NombreProvincia
            };

            EntityEntry<Provincia> result = await _context.Provincia.AddAsync(provincia);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Provincia entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Provincia>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<Provincia> GetByIdAsync(int id)
        {
            return await _dbset.FirstAsync(x => x.Id == id);
        }

        public async Task<Provincia> UpdateAsync(Provincia entity)
        {
            var provincia = await GetByIdAsync(entity.Id);

            if (provincia == null)
            {
                return null;
            }

            provincia.IdDepartamento = entity.IdDepartamento;
            provincia.NombreProvincia = entity.NombreProvincia;

            await _context.SaveChangesAsync();
            return provincia;
        }
    }
}
