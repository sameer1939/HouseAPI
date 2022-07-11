using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class PropertyDetailDTO : PropertyListDTO
    {
        public int SellRent { get; set; }
        public int CarpetArea { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public int FloorNo { get; set; }
        public int TotalFloor { get; set; }
        public bool ReadyToMove { get; set; }
        public string MainEntrance { get; set; }
        public int Security { get; set; } = 0;
        public bool Gated { get; set; }
        public int Maintenance { get; set; } = 0;
        public DateTime EstPossessionOn { get; set; }
        public int Age { get; set; } = 0;
        public string Description { get; set; }
        public ICollection<PhotoDTO> Photos { get; set; }
    }
}
