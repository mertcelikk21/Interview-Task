using Maffei.API.DataModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace Maffei.API.Repositories
{
    public interface IAboneRepository 
    {
      
        Task<List<Abone>> ListAllAboneAsync();
        Task<List<CurrencyUnit>> ListCurrencyUnit();
        Task<List<Kdv>> ListKdv();
        Task<Abone> UpdateAbone(int id , Abone abone);
        Task<Abone> GetByIdAbone(int id);
        Task<bool> Exist(int id);
        Task<bool> ExistAboneNumber(string aboneNumber);
        void AddAbone(Abone abone);
        Task<List<IndexCalculation>> GetAboneBorc(int aboneId);
        void AddTuketilenHesabi(IndexCalculation indexCalculation);
        Task<IndexCalculation> GetLastData();

    }
}
