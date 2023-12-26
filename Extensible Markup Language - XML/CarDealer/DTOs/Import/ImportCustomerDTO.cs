using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    [XmlType("Customer")]
    public class ImportCustomerDTO
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("birthDate")]
        public DateTime BirthDate { get; set; }

        [XmlElement("isYoungDriver")]
        public bool IsYoungDriver { get; set; }

    }
}
 //< Customer >
 //       < name > Emmitt Benally </ name >
 //       < birthDate > 1993 - 11 - 20T00: 00:00 </ birthDate >
 //       < isYoungDriver > true </ isYoungDriver >
 //   </ Customer >
