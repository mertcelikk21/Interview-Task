using Maffei.API.DataModels;
using System;

namespace Maffei.API.Dtos
{
    public class AddAboneDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
       
        public string Address { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int KdvId { get; set; }
        public int CurrencyUnitId { get; set; }

    }
}
