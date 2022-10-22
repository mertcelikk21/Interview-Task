using Maffei.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maffei.API.Services
{
    public interface IAboneService
    {
        Task<float> GetFirstIndex(int aboneId);
        Task<float> TariffKdvPrice(float lastIndex, float kdv,int aboneId);
        float RecipeTotalPrice(float tuketimMiktari, float birimFiyat, int aboneId);
        float ConsumptionAmount(float lastIndex, float firstIndex);
        float AmountConsumption(float amount, float unitPrice);
        List<DovizKurDto> GetExchangesByName(string exchangeName);

    }
}
