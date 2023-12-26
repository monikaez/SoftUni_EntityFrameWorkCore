using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    [XmlType("Car")]
    public class ImportCarDTO
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("traveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public ImportPartIdDTO[] PartsIds { get; set; }
    }
}
 //< Car >
 //   < make > Opel </ make >
 //   < model > Astra </ model >
 //   < traveledDistance > 516628215 </ traveledDistance >
 //   < parts >
 //     < partId id = "39" />
 //     < partId id = "62" />
 //     < partId id = "72" />
 //   </ parts >
 // </ Car >
