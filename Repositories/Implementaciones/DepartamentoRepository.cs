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
    public class DepartamentoRepository : IRepositoryBase<Departamento>
    {
        private readonly TrabajadoresPruebaContext _context;
        private readonly DbSet<Departamento> _dbset;

        public DepartamentoRepository(TrabajadoresPruebaContext context)
        {
            _context = context;
            _dbset = context.Set<Departamento>();
        }

        public async Task<Departamento> CreateAsync(Departamento entity)
        {
            Departamento departamento = new Departamento()
            {
                NombreDepartamento = entity.NombreDepartamento
            };

            EntityEntry<Departamento> result = await _context.Departamentos.AddAsync(departamento);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Departamento entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Departamento>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _dbset.FirstAsync(x => x.Id == id);
        }

        public async Task<Departamento> UpdateAsync(Departamento entity)
        {
            var departamento = await GetByIdAsync(entity.Id);

            if (departamento == null)
            {
                return null;
            }

            departamento.NombreDepartamento = entity.NombreDepartamento;

            await _context.SaveChangesAsync();
            return departamento;
        }
    }
}
