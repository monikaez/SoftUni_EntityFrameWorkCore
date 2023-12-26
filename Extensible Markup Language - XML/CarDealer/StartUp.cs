using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Dynamic;
using System.IO;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();
            //9
            //string inputSupplierXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            //Console.WriteLine(ImportSuppliers(context,inputSupplierXml));
            //10
            //string inputPartXml = File.ReadAllText("../../../Datasets/parts.xml");
            //Console.WriteLine(ImportParts(context, inputPartXml));
            //11
            //string inputCarsXml = File.ReadAllText("../../../Datasets/cars.xml");
            //Console.WriteLine(ImportCars(context, inputCarsXml));
            //12
            string inputCustomerXml = File.ReadAllText("../../../Datasets/customers.xml");
            Console.WriteLine(ImportCustomers(context, inputCustomerXml));

        }

        private static Mapper GetMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<CarDealerProfile>());

            return new Mapper(cfg);
        }

        //9
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            //1.create xml serialazer
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportSupplierDTO[]), new XmlRootAttribute("Suppliers"));
            //2.make to string
            using var reader = new StringReader(inputXml);
            ImportSupplierDTO[] importSuppliersDTOs = (ImportSupplierDTO[])xmlSerializer.Deserialize(reader);

            //3.mapping
            var mapper = GetMapper();
            Supplier[] suppliers = mapper.Map<Supplier[]>(importSuppliersDTOs);

            //4.Add to Ef context

            context.AddRange(suppliers);
            context.SaveChanges();

            //5.Commit changes
            return $"Successfully imported {suppliers.Length}";
            //$"Successfully imported {suppliers.Count}";
        }

        //10
        //Query 10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        { //1.create xml serialazer
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPartsDTO[]), new XmlRootAttribute("Parts"));

            //2.make to string
            using var reader = new StringReader(inputXml);
            ImportPartsDTO[] importPartsDTOs = (ImportPartsDTO[])xmlSerializer.Deserialize(reader);

            var supplierIds = context.Suppliers
                .Select(x => x.Id)
                .ToArray();


            //3.mapping
            var mapper = GetMapper();
            Part[] parts = mapper.Map<Part[]>(importPartsDTOs
                .Where(p => supplierIds.Contains(p.SupplierId)));

            //4.Add to Ef context
            context.AddRange(parts);
            context.SaveChanges();

            //5.Commit chnges
            return $"Successfully imported {parts.Length}";
            //$"Successfully imported {parts.Count}";;
        }

        //Query 11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportCarDTO[]), new XmlRootAttribute("Cars"));

            using StringReader stringReader = new StringReader(inputXml);

            ImportCarDTO[] importCarDTOs = (ImportCarDTO[])xmlSerializer.Deserialize(stringReader);

            var mapper = GetMapper();
            List<Car> cars = new List<Car>();

            foreach (var carDTO in importCarDTOs)
            {
                Car car = mapper.Map<Car>(carDTO);

                int[] carPartIds = carDTO.PartsIds
                    .Select(x => x.Id)
                    .Distinct()
                    .ToArray();

                var carParts = new List<PartCar>();

                foreach (var id in carPartIds)
                {
                    carParts.Add(new PartCar
                    {
                        Car = car,
                        PartId = id
                    });
                }

                car.PartsCars = carParts;
                cars.Add(car);
            }

            context.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            //1.create xml serialazer
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCustomerDTO[]), new XmlRootAttribute("Customers"));

            //2.make to string
            using var reader = new StringReader(inputXml);
            ImportCustomerDTO[] importCustomerDTOs = (ImportCustomerDTO[])xmlSerializer.Deserialize(reader);
            //3.mapping
            var mapper = GetMapper();
            Customer[] customers = mapper.Map<Customer[]>(importCustomerDTOs);
            //Customer[] customers = mapper.Map<Customer[]>(importCustomerDTOs);

            //4.Add to Ef context
            context.AddRange(customers);
            context.SaveChanges();
            //5.Commit changes
            return $"Successfully imported {customers.Length}";

        }

    }
}
