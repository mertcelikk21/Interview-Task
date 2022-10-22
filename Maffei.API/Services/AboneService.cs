using Maffei.API.Dtos;
using Maffei.API.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Maffei.API.Services
{
    public class AboneService : IAboneService
    {
        private readonly IAboneRepository _aboneRepository;
        public AboneService(IAboneRepository aboneRepository)
        {
            _aboneRepository = aboneRepository;
        }

        public float AmountConsumption(float amount, float unitPrice)
        {
            return amount / unitPrice;
        }

        public float ConsumptionAmount(float lastIndex, float firstIndex)
        {
            float consumptionAmount = lastIndex - firstIndex;
            return consumptionAmount;
        }

        public List<DovizKurDto> GetExchangesByName(string exchangeName)
        {
            #region TCMB XML Kur Okuma
            List<DovizKurDto> dovizKurDtos = new List<DovizKurDto>();
            XmlTextReader xmlDocument = new XmlTextReader("http://www.tcmb.gov.tr/kurlar/today.xml");
            int type = 0;
            while (xmlDocument.Read())
            {
                if (xmlDocument.NodeType == XmlNodeType.Element)
                {

                    if (xmlDocument.GetAttribute("CurrencyCode") == "USD")
                    {
                        type = 1;
                    }

                    if ((type == 1) && (xmlDocument.Name == "BanknoteSelling"))
                    {
                        xmlDocument.Read();
                        type = 0;
                        dovizKurDtos.Add(new DovizKurDto
                        {
                            DovizAd = "USD",
                            Kur = float.Parse(xmlDocument.Value, CultureInfo.InvariantCulture)
                        });
                    }
                }
                if (xmlDocument.NodeType == XmlNodeType.Element)
                {
                    if (xmlDocument.GetAttribute("CurrencyCode") == "EUR")
                    {
                        type = 2;
                    }
                    if ((type == 2) && (xmlDocument.Name == "BanknoteSelling"))
                    {
                        xmlDocument.Read();
                        type = 0;

                        dovizKurDtos.Add(new DovizKurDto
                        {
                            DovizAd = "EUR",
                            Kur = float.Parse(xmlDocument.Value, CultureInfo.InvariantCulture)
                        });
                    }
                }
                if (xmlDocument.NodeType == XmlNodeType.Element)
                {
                    if (xmlDocument.GetAttribute("CurrencyCode") == "GBP")
                    {
                        type = 3;
                    }
                    if ((type == 3) && (xmlDocument.Name == "BanknoteSelling"))
                    {
                        xmlDocument.Read();
                        type = 0;

                        dovizKurDtos.Add(new DovizKurDto
                        {
                            DovizAd = "GBP",
                            Kur = float.Parse(xmlDocument.Value, CultureInfo.InvariantCulture)
                        });
                    }
                }
            }
            #endregion

            return dovizKurDtos;
        }

        public async Task<float> GetFirstIndex(int aboneId)
        {
            float ilkIndeks = 0;

            var hesaplananlarListesi = await _aboneRepository.GetAboneBorc(aboneId);

            if (hesaplananlarListesi != null && hesaplananlarListesi.Count > 0)
            {
                ilkIndeks = hesaplananlarListesi.OrderByDescending(x => x.CalculationDate).FirstOrDefault().LastIndex;
            }
            return ilkIndeks;
        }



        public float RecipeTotalPrice(float tuketimMiktari, float birimFiyat, int aboneId)
        {
            float aboneDovizKur = 0;
            var aboneParaBirimi = _aboneRepository.GetByIdAbone(aboneId).Result.CurrencyUnit.Money;
            if (aboneParaBirimi != "TL")
            {
                 aboneDovizKur = GetExchangesByName(aboneParaBirimi).Single(x => x.DovizAd == aboneParaBirimi).Kur;
            }
            if (aboneDovizKur != 0)
            {
                birimFiyat = 3 / aboneDovizKur;
            }
            float recipeTotalPrice = tuketimMiktari * birimFiyat;
            return recipeTotalPrice;
        }


        public async Task<float> TariffKdvPrice(float lastIndex, float kdv, int aboneId)
        {
            float aboneDovizKur = 0;
            float birimFiyat = 3;

            var aboneParaBirimi = _aboneRepository.GetByIdAbone(aboneId).Result.CurrencyUnit.Money;
            if (aboneParaBirimi != "TL")
            {
                aboneDovizKur = GetExchangesByName(aboneParaBirimi).Single(x => x.DovizAd == aboneParaBirimi).Kur;
            }
            if (aboneDovizKur != 0)
            {
                 birimFiyat = 3/aboneDovizKur;
            }
              
            
            float ilkIndex = await GetFirstIndex(aboneId);
            float tuketimMiktari = lastIndex - ilkIndex;
            float toplamFiyat = tuketimMiktari * kdv * birimFiyat;
            return toplamFiyat;

        }
    }
}
