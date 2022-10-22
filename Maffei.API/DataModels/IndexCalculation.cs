using System;

namespace Maffei.API.DataModels
{
    public class IndexCalculation
    {
        public int Id { get; set; }
        public DateTime  CalculationDate { get; set; }
        public int AboneId { get; set; }
        public Abone Abone { get; set; }
        public float UnitPrice { get; set; } = 3;
        public int CalculationTypeId { get; set; }
        public CalculationType CalculationType { get; set; }
        public float FirstIndex { get; set; } = 0;
        public float LastIndex { get; set; }   
        public float ConsumptionAmount { get; set; }
        public float RecipeTotalPrice { get; set; }
        public float TariffKdvPrice { get; set; } 
    }
}
