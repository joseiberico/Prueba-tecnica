using DataAccess.Context;
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
    public class DistritoRepository : IRepositoryBase<Distrito>
    {
        private readonly TrabajadoresPruebaContext _context;
        private readonly DbSet<Distrito> _dbset;

        public DistritoRepository(TrabajadoresPruebaContext context)
        {
            _context = context;
            _dbset = context.Set<Distrito>();
        }

        public async Task<Distrito> CreateAsync(Distrito entity)
        {
            Distrito distrito = new Distrito()
            {
                
                IdProvincia = entity.IdProvincia,
                NombreDistrito = entity.NombreDistrito
            };

            EntityEntry<Distrito> result = await _context.Distritos.AddAsync(distrito);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Distrito entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Distrito>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<Distrito> GetByIdAsync(int id)
        {
            return await _dbset.FirstAsync(x => x.Id ==  id);
        }

        public async Task<Distrito> UpdateAsync(Distrito entity)
        {
            var distrito = await GetByIdAsync(entity.Id);

            if (distrito == null)
            {
                return null;
            }

            distrito.IdProvincia = entity.IdProvincia;
            distrito.NombreDistrito = entity.NombreDistrito;

            await _context.SaveChangesAsync();
            return distrito;
        }
    }
}
