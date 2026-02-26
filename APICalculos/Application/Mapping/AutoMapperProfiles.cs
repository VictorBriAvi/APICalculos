using APICalculos.Application.DTOs;
using APICalculos.Application.DTOs.Client;
using APICalculos.Application.DTOs.CustomerHistory;
using APICalculos.Application.DTOs.Employee;
using APICalculos.Application.DTOs.Expense;
using APICalculos.Application.DTOs.ExpenseType;
using APICalculos.Application.DTOs.PaymentType;
using APICalculos.Application.DTOs.Rol;
using APICalculos.Application.DTOs.Sale;
using APICalculos.Application.DTOs.SaleDetail;
using APICalculos.Application.DTOs.ServiceCategories;
using APICalculos.Application.DTOs.Services;
using APICalculos.Application.DTOs.Store;
using APICalculos.Application.DTOs.User;
using APICalculos.Domain.Entidades;
using APICalculos.Domain.Entities;
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
            CreateMap<ClientCreationDTO, Client>()
                .ForMember(dest => dest.DateBirth,
                    opt => opt.MapFrom(src => src.DateBirth));


            CreateMap<CustomerHistory, CustomerHistoryDTO>()
                .ForMember(dto => dto.ClientName, ent => ent.MapFrom(prop => prop.Client.Name));
            CreateMap<CustomerHistoryCreationDTO, CustomerHistory>();
                
                
            CreateMap<CustomerHistoryUpdateDTO, CustomerHistory>()
                .ForMember(dest => dest.DateHistory, opt => opt.Ignore());

            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeCreationDTO, Employee>();

            CreateMap<PaymentTypes, PaymentTypeDTO>();
            CreateMap<PaymentTypeCreationDTO, PaymentTypes>();

            CreateMap<ServiceType, ServiceTypeDTO>()
                .ForMember(dto => dto.ServiceCategorieName, ent => ent.MapFrom(prop => prop.ServiceCategories.Name));
            CreateMap<ServiceTypeCreationDTO, ServiceType>();

            CreateMap<Servicio, ServicioDTO>()
                .ForMember(dto => dto.NombreCompletoEmpleado, ent => ent.MapFrom(prop => prop.Empleado.Name))
                .ForMember(dto => dto.NombreCompletoCliente, ent => ent.MapFrom(prop => prop.Cliente.Name))
                .ForMember(dto => dto.NombreTipoDePago, ent => ent.MapFrom(prop => prop.TipoDePago.Name))
                .ForMember(dto => dto.NombreServicio, ent => ent.MapFrom(prop => prop.TipoDeServicio.Name));
         
            CreateMap<ServicioCreacionDTO, Servicio>();


            CreateMap<User, UsuarioDTO>()
                .ForMember(dto => dto.TipoRol, opt => opt.MapFrom(ent => ent.UserRoles.FirstOrDefault().Rol.Name))
                .ForMember(dto => dto.NombreUsuario, opt => opt.MapFrom(ent => ent.UserRoles.FirstOrDefault().User.FullName));
            CreateMap<UsuarioCreacionDTO, User>();

            CreateMap<Rol, RolDTO>();
            CreateMap<RolCreacionDTO, Rol>();

            CreateMap<UserRol, UsuarioRolCreacionDTO>();
            CreateMap<UsuarioRolCreacionDTO, UserRol>();

            CreateMap<Expenses, ExpenseDTO>()
                .ForMember(dto => dto.NameExpenseType, ent => ent.MapFrom(prop => prop.ExpenseType.Name))
                .ForMember(d => d.NameExpenseType,o => o.MapFrom(s => s.ExpenseType.Name))
                .ForMember(d => d.PaymentTypeName, o => o.MapFrom(s => s.PaymentType.Name));

            CreateMap<ExpenseCreationDTO, Expenses>();

            CreateMap<ExpenseType, ExpenseTypeDTO>();
            CreateMap<ExpenseTypeCreationDTO, ExpenseType>();

            CreateMap<ServiceCategorie, ServiceCategoriesDTO>();
            CreateMap<ServiceCategoriesCreationDTO, ServiceCategorie>();

            CreateMap<Sale, SaleDTO>()
                .ForMember(dto => dto.NameClient, opt => opt.MapFrom(v => v.Client.Name))
                .ForMember(dto => dto.Payments, opt => opt.MapFrom(v => v.Payments))
                //.ForMember(dto => dto.PriceTotal, opt => opt.MapFrom(v => v.))
                .ForMember(dto => dto.SaleDetail, opt => opt.MapFrom(v => v.SaleDetail));

            CreateMap<Sale, ClienteYTipoDePagoDTO>()
                .ForMember(dto => dto.ClienteId, opt => opt.MapFrom(v => v.ClientId));


            CreateMap<SaleCreationDTO, Sale>();

            CreateMap<SaleDetail, SaleDetailDTO>()
                .ForMember(dto => dto.NameServiceTypeSale, ent => ent.MapFrom(prop => prop.ServiceType.Name))
                //////.ForMember(dto => dto.PriceServiceType, ent => ent.MapFrom(prop => prop.Price.Price))
                .ForMember(dto => dto.NameEmployeeSale, ent => ent.MapFrom(prop => prop.Employee.Name));



            CreateMap<Sale, SaleDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(v => v.Id))
                .ForMember(dto => dto.ClientId, opt => opt.MapFrom(v => v.ClientId))
                .ForMember(dto => dto.NameClient, opt => opt.MapFrom(v => v.Client.Name))
                //.ForMember(dto => dto.TotalAmount, opt => opt.MapFrom(v => v.TotalAmount))
                .ForMember(dto => dto.DateSale, opt => opt.MapFrom(v => v.DateSale))
                .ForMember(dto => dto.IsDeleted, opt => opt.MapFrom(v => v.IsDeleted))
                .ForMember(dto => dto.SaleDetail, opt => opt.MapFrom(v => v.SaleDetail))
                .ForMember(dto => dto.Payments, opt => opt.MapFrom(v => v.Payments));
                // resumen concatenado: "Efectivo, Tarjeta"
                //.ForMember(dto => dto.NamePaymentType, opt => opt.MapFrom(v => v.Payments != null && v.Payments.Any() ? string.Join(", ", v.Payments.Select(p => p.PaymentType.Name))  : string.Empty));





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
              


            CreateMap<SaleDetailCreationDTO, SaleDetail>()
                .ForMember(dest => dest.SaleId, opt => opt.Ignore());


            CreateMap<SaleCreationDTO, Sale>()
                .ForMember(dest => dest.SaleDetail, opt => opt.MapFrom(src => src.SaleDetails))
                .ForMember(dest => dest.DateSale, opt => opt.Ignore());


            CreateMap<SaleDetailCreationDTO, SaleDetail>();

            CreateMap<SalePayment, SalePaymentDTO>()
                .ForMember(dto => dto.PaymentTypeName, opt => opt.MapFrom(p => p.PaymentType.Name));

            CreateMap<SalePaymentDTO, SalePayment>();


            CreateMap<Client, ClientSearchDTO>();
            CreateMap<Employee, EmployeeSearchDTO>();
            CreateMap<ServiceType, ServicesSearchDTO>();


            CreateMap<Store, StoreResponseDTO>();

            CreateMap<StoreCreateDto, Store>()
                .ForMember(dest => dest.CreateOn,
                           opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<StoreUpdateDTO, Store>();

            CreateMap<User, UserResponseDTO>();

            CreateMap<UserCreateDTO, User>()
                .ForMember(dest => dest.PasswordHash,
                           opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn,
                           opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive,
                           opt => opt.MapFrom(_ => true));

            CreateMap<UserUpdateDTO, User>();


        }
    }
}
