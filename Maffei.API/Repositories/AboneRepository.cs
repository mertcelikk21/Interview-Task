using Maffei.API.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Policy;

namespace Maffei.API.Repositories
{
    public class AboneRepository : IAboneRepository
    {
        private readonly AdminContext _context;

        public AboneRepository(AdminContext context)
        {
            _context = context;
        }
        public async Task<bool> Exist(int id)
        {
            return await _context.Abone.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> ExistAboneNumber(string aboneNumber)
        {
            return await _context.Abone.AnyAsync(x => x.Number == aboneNumber);
        }
        public void AddAbone(Abone abone)
        {
            _context.Abone.Add(abone);
            _context.SaveChangesAsync();

        }


        public async Task<List<IndexCalculation>> GetAboneBorc(int aboneId)
        {
            return await _context.IndexCalculation.Include(x=>  x.Abone.Kdv )
                .Include(x=>x.Abone.CurrencyUnit).Include(nameof(CalculationType))
                .Where(x => x.AboneId == aboneId).OrderByDescending(x=>x.CalculationDate).ToListAsync();
        }

        public async Task<Abone> GetByIdAbone(int id)
        {
            return await _context.Abone.Include(nameof(Kdv)).Include(nameof(CurrencyUnit)).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Abone>> ListAllAboneAsync()
        {
            return await _context.Abone.Include(nameof(Kdv)).Include(nameof(CurrencyUnit)).OrderByDescending(x=>x.OpeningDate).ToListAsync();
        }

        public async Task<List<CurrencyUnit>> ListCurrencyUnit()
        {
            return await _context.CurrencyUnit.ToListAsync();
        }

        public async Task<List<Kdv>> ListKdv()
        {
            return await _context.Kdv.ToListAsync();
        }

        public async Task<Abone> UpdateAbone(int id , Abone abone)
        {
            var existingAbone = await GetByIdAbone(id);
            if (existingAbone!=null)
            {
                existingAbone.Number = abone.Number;
                existingAbone.Address = abone.Address;
                existingAbone.Name = abone.Name;
                existingAbone.Surname = abone.Surname;
                existingAbone.CurrencyUnitId = abone.CurrencyUnitId;
                existingAbone.KdvId = abone.KdvId;
               await _context.SaveChangesAsync();
                return existingAbone;
            }
            return null;
           
        }

        public void AddTuketilenHesabi(IndexCalculation indexCalculation)
        {
            _context.IndexCalculation.Add(indexCalculation);
            _context.SaveChangesAsync();
        }

        public async Task<IndexCalculation> GetLastData()
        {
            return await _context.IndexCalculation.OrderBy(x => x.CalculationDate).LastAsync();
        }
    }
}
