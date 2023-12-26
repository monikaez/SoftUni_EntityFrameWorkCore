using AutoMapper;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {//09.Import Suppliers
            CreateMap<ImportSupplierDTO, Supplier>();
           
            //10.Import Parts
            CreateMap<ImportPartsDTO, Part>();

            //11.Import Cars
            CreateMap<ImportCarDTO, Car>();

            //CreateMap<Car, ExportCarsWithDistance>();
            CreateMap<ImportCustomerDTO, Customer>();
            //CreateMap<ImportSaleDTO, Sale>();



        }
    }
}
