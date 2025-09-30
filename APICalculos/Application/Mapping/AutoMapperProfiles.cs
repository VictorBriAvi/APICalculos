using APICalculos.Application.DTOs;
using APICalculos.Domain.Entidades;
using AutoMapper;
using System.Data;

namespace APICalculos.Application.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        { 
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoCreacionDTO, Producto>();

            CreateMap<Client, ClientDTO>();
            CreateMap<ClientCreationDTO, Client>();

            CreateMap<CustomerHistory, CustomerHistoryDTO>()
                .ForMember(dto => dto.ClientName, ent => ent.MapFrom(prop => prop.Client.Name));
            CreateMap<CustomerHistoryCreationDTO, CustomerHistory>();
                
                
            CreateMap<CustomerHistoryUpdateDTO, CustomerHistory>()
                .ForMember(dest => dest.DateHistory, opt => opt.Ignore());

            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeCreationDTO, Employee>();

            CreateMap<PaymentType, PaymentTypeDTO>();
            CreateMap<PaymentTypeCreationDTO, PaymentType>();

            CreateMap<ServiceType, ServiceTypeDTO>()
                .ForMember(dto => dto.ServiceCategorieName, ent => ent.MapFrom(prop => prop.ServiceCategories.Name));
            CreateMap<ServiceTypeCreationDTO, ServiceType>();

            CreateMap<Servicio, ServicioDTO>()
                .ForMember(dto => dto.NombreCompletoEmpleado, ent => ent.MapFrom(prop => prop.Empleado.Name))
                .ForMember(dto => dto.NombreCompletoCliente, ent => ent.MapFrom(prop => prop.Cliente.Name))
                .ForMember(dto => dto.NombreTipoDePago, ent => ent.MapFrom(prop => prop.TipoDePago.Name))
                .ForMember(dto => dto.NombreServicio, ent => ent.MapFrom(prop => prop.TipoDeServicio.Name));
         
            CreateMap<ServicioCreacionDTO, Servicio>();


            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dto => dto.TipoRol, opt => opt.MapFrom(ent => ent.UsuarioRoles.FirstOrDefault().Rol.NombreRol))
                .ForMember(dto => dto.NombreUsuario, opt => opt.MapFrom(ent => ent.UsuarioRoles.FirstOrDefault().Usuario.NombreDeUsuario));
            CreateMap<UsuarioCreacionDTO, Usuario>();

            CreateMap<Rol, RolDTO>();
            CreateMap<RolCreacionDTO, Rol>();

            CreateMap<UsuarioRol, UsuarioRolCreacionDTO>();
            CreateMap<UsuarioRolCreacionDTO, UsuarioRol>();

            CreateMap<Expense, ExpenseDTO>()
                .ForMember(dto => dto.NameExpenseType, ent => ent.MapFrom(prop => prop.ExpenseType.Name));
            CreateMap<ExpenseCreationDTO, Expense>();

            CreateMap<ExpenseType, ExpenseTypeDTO>();
            CreateMap<ExpenseTypeCreationDTO, ExpenseType>();

            CreateMap<ServiceCategorie, ServiceCategoriesDTO>();
            CreateMap<ServiceCategoriesCreationDTO, ServiceCategorie>();

            CreateMap<Sale, SaleDTO>()
                .ForMember(dto => dto.NameClient, opt => opt.MapFrom(v => v.Client.Name))
                .ForMember(dto => dto.NamePaymentType, opt => opt.MapFrom(v => v.PaymentType.Name))
                //.ForMember(dto => dto.PriceTotal, opt => opt.MapFrom(v => v.))
                .ForMember(dto => dto.SaleDetail, opt => opt.MapFrom(v => v.SaleDetail));

            CreateMap<Sale, ClienteYTipoDePagoDTO>()
                .ForMember(dto => dto.ClienteId, opt => opt.MapFrom(v => v.ClientId))
                .ForMember(dto => dto.TipoDePagoId, opt => opt.MapFrom(v => v.PaymentTypeId));

            CreateMap<SaleCreationDTO, Sale>();

            CreateMap<SaleDetail, SaleDetailDTO>()
                .ForMember(dto => dto.NameServiceTypeSale, ent => ent.MapFrom(prop => prop.ServiceType.Name))
                //////.ForMember(dto => dto.PriceServiceType, ent => ent.MapFrom(prop => prop.Price.Price))
                .ForMember(dto => dto.NameEmployeeSale, ent => ent.MapFrom(prop => prop.Employee.Name));


            CreateMap<Sale, SaleDTO>()
                                        .ForMember(dto => dto.Id,
                                                   opt => opt.MapFrom(v => v.Id))
                                        .ForMember(dto => dto.NameClient,
                                                   opt => opt.MapFrom(v => v.Client.Name))
                                        .ForMember(dto => dto.NamePaymentType,
                                                   opt => opt.MapFrom(v => v.PaymentType.Name))
                                        .ForMember(dto => dto.DateSale,
                                                   opt => opt.MapFrom(v => v.DateSale))
                                        // Al no haber Cantidad, simplemente sumamos Precio de cada detalle
                                        //.ForMember(dto => dto.PriceTotal,
                                        //           opt => opt.MapFrom(v => v.SaleDetail.Sum(d => d.price)))
                                        .ForMember(dto => dto.SaleDetail,
                                                   opt => opt.MapFrom(v => v.SaleDetail))
                                         .ForMember(dest => dest.SaleDetail, opt => opt.MapFrom(src => src.SaleDetail));


            CreateMap<SaleDetail, SaleDetailDTO>()
                                                    .ForMember(dto => dto.Id,
                                                               opt => opt.MapFrom(d => d.Id))
                                                    // Nombre del cliente que compró: navegamos por la venta

                                                    .ForMember(dto => dto.NameServiceTypeSale,
                                                               opt => opt.MapFrom(d => d.ServiceType.Name))
                                                    //.ForMember(dto => dto.PriceServiceType,
                                                    //           opt => opt.MapFrom(d => d.price))  // tu propiedad calculada
                                                    .ForMember(dto => dto.NameEmployeeSale,
                                                               opt => opt.MapFrom(d => d.Employee.Name));
              


            CreateMap<SaleDetailCreationDTO, SaleDetail>();
            
        }
    }
}
