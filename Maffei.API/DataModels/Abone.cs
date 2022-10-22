using System;

namespace Maffei.API.DataModels
{
    public class  Abone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime OpeningDate { get; set; } = DateTime.Now;
        public string Address { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int KdvId { get; set; }
        public Kdv Kdv { get; set; }
        public int CurrencyUnitId { get; set; }
        public CurrencyUnit CurrencyUnit { get; set; }


    }
}
