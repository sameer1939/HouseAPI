using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Property : BaseEntity
    {
        public int SellRent { get; set; }
        public string Name { get; set; }
        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        public int BHK { get; set; }
        public int FurnitureTypeId { get; set; }
        public FurnitureType FurnitureType { get; set; }
        public int Price { get; set; }
        public int BuiltArea { get; set; }
        public int CarpetArea { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int FloorNo { get; set; }
        public int TotalFloor { get; set; }
        public bool ReadyToMove { get; set; }
        public string MainEntrance { get; set; }
        public int Security { get; set; }
        public bool Gated { get; set; }
        public int Maintenance { get; set; }
        public DateTime EstPossessionOn { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public DateTime PostedOn { get; set; } = DateTime.Now;
        [ForeignKey("User")]
        public int PostedBy { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
