using AutoMapper;
using Maffei.API.DataModels;
using Maffei.API.Dtos;
using Maffei.API.Error;
using Maffei.API.Repositories;
using Maffei.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Maffei.API.Controllers
{
    [ApiController]
    public class AboneController : Controller
    {
        private readonly IAboneRepository _aboneRepository;
        private readonly IMapper _mapper;
        private readonly IAboneService _aboneService;
        public AboneController(IAboneRepository aboneRepository, IMapper mapper, IAboneService aboneService)
        {
            _aboneRepository = aboneRepository;
            _mapper = mapper;
            _aboneService = aboneService;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAbones()
        {

            var abones = await _aboneRepository.ListAllAboneAsync();
            return Ok(_mapper.Map<List<AboneDto>>(abones));
        }

        [HttpGet]
        [Route("[controller]/firstIndex/{aboneId}")]
        public async Task<IActionResult> GetByFirstIndex([FromRoute] int aboneId)
        {
            float ilkIndex = await _aboneService.GetFirstIndex(aboneId);
            return Ok(ilkIndex);
        }

        [HttpGet]
        [Route("[controller]/borc/{aboneId}")]
        public async Task<IActionResult> CalculationModelId([FromRoute] int aboneId)
        {

            var data = await _aboneRepository.GetAboneBorc(aboneId);

            return Ok(_mapper.Map<List<IndexCalculation>>(data));
        }

        [HttpGet]
        [Route("[controller]/kur/{aboneId}")]
        public  float GetAnlıkKur([FromRoute] int aboneId)
        {
            var aboneParaBirimi =  _aboneRepository.GetByIdAbone(aboneId).Result.CurrencyUnit.Money;
            float aboneDovizKur = 0;
            if (aboneParaBirimi != "TL")
            {
                 aboneDovizKur =  _aboneService.GetExchangesByName(aboneParaBirimi).Single(x => x.DovizAd == aboneParaBirimi).Kur;
            }
            if (aboneDovizKur != 0)
            {
                return aboneDovizKur;
            }
            return 0;
            //<zx

        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> AddAbone([FromBody] AddAboneDto aboneDto)
        {

            if (await _aboneRepository.ExistAboneNumber(aboneDto.Number))
            {
                return BadRequest("Girdiğiniz Abone Numarası Başka Birisine Aittir Lütfen Başka Bir Abone Numarası Giriniz");
            }

            var data = _mapper.Map<Abone>(aboneDto);
            if (data != null)
            {
                await Task.Run(() => _aboneRepository.AddAbone(data));
            }

            return Ok(data);

        }
        [HttpPost]
        [Route("[controller]/TuketilenHesap/{aboneId}")]
        public async Task<IActionResult> AddTuketilenHesap([FromBody] TuketilenHesapDto tuketilenHesapDto, [FromRoute] int aboneId)
        {
            if (tuketilenHesapDto.LastIndex <= tuketilenHesapDto.FirstIndex)
            {
                return BadRequest("Last Index Değeri, İlk Index değerinden küçük veya eşit olamaz");
            }
            float birimFiyat = 3;
            var abone = await _aboneRepository.GetByIdAbone(aboneId);
            float aboneDovizKur = 0;
            var aboneParaBirimi =  _aboneRepository.GetByIdAbone(aboneId).Result.CurrencyUnit.Money;
            if (aboneParaBirimi != "TL")
            {
                aboneDovizKur = _aboneService.GetExchangesByName(aboneParaBirimi).Single(x => x.DovizAd == aboneParaBirimi).Kur;
            }
            if (aboneDovizKur != 0)
            {
                birimFiyat = 3 / aboneDovizKur;
            }

            float consumptionAmount = _aboneService.ConsumptionAmount(tuketilenHesapDto.LastIndex, tuketilenHesapDto.FirstIndex);

            float tariffKdvPrice = await _aboneService.TariffKdvPrice(tuketilenHesapDto.LastIndex, abone.Kdv.KdvRatio, aboneId);

            float recipeTotalPrice = _aboneService.RecipeTotalPrice(consumptionAmount, birimFiyat, aboneId);

            IndexCalculation indexCalculation = new IndexCalculation()
            {
                AboneId = abone.Id,
                CalculationDate = DateTime.Now,
                ConsumptionAmount = consumptionAmount,
                RecipeTotalPrice = recipeTotalPrice,
                FirstIndex = tuketilenHesapDto.FirstIndex,
                LastIndex = tuketilenHesapDto.LastIndex,
                TariffKdvPrice = tariffKdvPrice,
                UnitPrice = 3,
                CalculationTypeId = 1,
            };

            await Task.Run(() => _aboneRepository.AddTuketilenHesabi(indexCalculation));
            return Ok(indexCalculation);
        }

        [HttpPost]
        [Route("[controller]/GirilenPara/{aboneId}")]
        public async Task<IActionResult> GirilenUcretHesabı([FromBody] GirilenUcretDto girilenUcretDto, [FromRoute] int aboneId)
        {
            float birimFiyat = 3;
            float aboneDovizKur = 0;

            var aboneParaBirimi = _aboneRepository.GetByIdAbone(aboneId).Result.CurrencyUnit.Money;
            var kdvUcreti = _aboneRepository.GetByIdAbone(aboneId).Result.Kdv.KdvRatio;
            if (aboneParaBirimi != "TL")
            {
                aboneDovizKur = _aboneService.GetExchangesByName(aboneParaBirimi).Single(x => x.DovizAd == aboneParaBirimi).Kur;
            }
            if (aboneDovizKur != 0)
            {
                birimFiyat = 3 / aboneDovizKur;
            }
            float tuketimMiktarı = _aboneService.AmountConsumption(girilenUcretDto.Amount, birimFiyat) / kdvUcreti;
            float lastIndex = girilenUcretDto.FirstIndex + tuketimMiktarı;

            float recipeTotalPrice = _aboneService.RecipeTotalPrice(tuketimMiktarı, birimFiyat, aboneId) / kdvUcreti * kdvUcreti;

            IndexCalculation indexCalculation = new IndexCalculation()
            {
                AboneId = aboneId,
                CalculationDate = DateTime.Now,
                CalculationTypeId = 2,
                FirstIndex = girilenUcretDto.FirstIndex,
                LastIndex = lastIndex,
                ConsumptionAmount = tuketimMiktarı,
                RecipeTotalPrice = recipeTotalPrice,
                TariffKdvPrice = girilenUcretDto.Amount,
                UnitPrice = 3
            };

            await Task.Run(() => _aboneRepository.AddTuketilenHesabi(indexCalculation));
            return Ok(indexCalculation);
        }


        [HttpGet]
        [Route("[controller]/kdv")]
        public async Task<IActionResult> KdvList()
        {
            var kdv = await _aboneRepository.ListKdv();
            return Ok(_mapper.Map<List<KdvDto>>(kdv));
        }

        [HttpGet]
        [Route("[controller]/money")]
        public async Task<IActionResult> MoneyList()
        {
            var currencyUnits = await _aboneRepository.ListCurrencyUnit();
            return Ok(_mapper.Map<List<CurrencyUnitDto>>(currencyUnits));
        }


        [HttpGet]
        [Route("[controller]/{aboneId}")]
        public async Task<IActionResult> GetByIdAbone([FromRoute] int aboneId)
        {
            
            var data = await _aboneRepository.GetByIdAbone(aboneId);
            return Ok(_mapper.Map<AboneDto>(data));
        }

        [HttpGet]
        [Route("[controller]/LastData")]
        public async Task<IActionResult> GetLastDataBor()
        {

            var data = await _aboneRepository.GetLastData();
            return Ok(data);
        }



        [HttpPut]
        [Route("[controller]/{aboneId}")]
        public async Task<IActionResult> UpdateAbone([FromRoute] int aboneId, [FromBody] UpdateAboneDto aboneDto)
        {
            if (await _aboneRepository.Exist(aboneId))
            {
                var mappingAbone = _mapper.Map<Abone>(aboneDto);
                var updatedAbone = await _aboneRepository.UpdateAbone(aboneId, mappingAbone);
                if (updatedAbone != null)
                {
                    return Ok(_mapper.Map<Abone>(updatedAbone));
                }
            }
            return NotFound();

        }
    }
}
