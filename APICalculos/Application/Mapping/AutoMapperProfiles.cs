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

            CreateMap<ClientModel, ClientDTO>();
            CreateMap<ClientCreationDTO, ClientModel>();

            CreateMap<CustomerHistory, CustomerHistoryDTO>()
                .ForMember(dto => dto.ClientName, ent => ent.MapFrom(prop => prop.Client.Name));
            CreateMap<CustomerHistoryCreationDTO, CustomerHistory>();

            CreateMap<EmployeeModel, EmployeeDTO>();
            CreateMap<EmployeeCreationDTO, EmployeeModel>();

            CreateMap<PaymentType, PaymentTypeDTO>();
            CreateMap<PaymentTypeCreationDTO, PaymentType>();

            CreateMap<ServiceType, ServiceTypeDTO>()
                .ForMember(dto => dto.ServiceCategorieName, ent => ent.MapFrom(prop => prop.ServiceCategorie.Name));
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
                .ForMember(dto => dto.NameExpenseType, ent => ent.MapFrom(prop => prop.ExpenseTypes.Name));
            CreateMap<ExpenseCreationDTO, Expense>();

            CreateMap<ExpenseType, ExpenseTypeDTO>();
            CreateMap<ExpenseTypeCreationDTO, ExpenseType>();

            CreateMap<ServiceCategorie, ServiceCategoriesDTO>();
            CreateMap<ServiceCategoriesCreationDTO, ServiceCategorie>();

            CreateMap<Venta, VentaDTO>()
                .ForMember(dto => dto.NombreCliente, opt => opt.MapFrom(v => v.NombreCliente))
                .ForMember(dto => dto.NombreTipoDePago, opt => opt.MapFrom(v => v.NombreTipoDePago))
                .ForMember(dto => dto.ValorTotal, opt => opt.MapFrom(v => v.ValorTotal))
                .ForMember(dto => dto.Detalle, opt => opt.MapFrom(v => v.Detalle));

            CreateMap<Venta, ClienteYTipoDePagoDTO>()
                .ForMember(dto => dto.ClienteId, opt => opt.MapFrom(v => v.ClienteId))
                .ForMember(dto => dto.TipoDePagoId, opt => opt.MapFrom(v => v.TipoDePagoId));

            CreateMap<VentaCreacionDTO, Venta>();

            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(dto => dto.NombreClienteVenta, ent => ent.MapFrom(prop => prop.Venta.Client.Name))
                .ForMember(dto => dto.NombreTipoDeServicioVenta, ent => ent.MapFrom(prop => prop.TipoDeServicio.Name))
                .ForMember(dto => dto.PrecioTipoDeServicio, ent => ent.MapFrom(prop => prop.TipoDeServicio.Price))
                .ForMember(dto => dto.NombreEmpleadoVenta, ent => ent.MapFrom(prop => prop.Empleado.Name));


            CreateMap<Venta, VentaDTO>()
                                        .ForMember(dto => dto.VentaId,
                                                   opt => opt.MapFrom(v => v.VentaId))
                                        .ForMember(dto => dto.NombreCliente,
                                                   opt => opt.MapFrom(v => v.Client.Name))
                                        .ForMember(dto => dto.NombreTipoDePago,
                                                   opt => opt.MapFrom(v => v.PaymentType.Name))
                                        .ForMember(dto => dto.FechaVenta,
                                                   opt => opt.MapFrom(v => v.FechaVenta))
                                        // Al no haber Cantidad, simplemente sumamos Precio de cada detalle
                                        .ForMember(dto => dto.ValorTotal,
                                                   opt => opt.MapFrom(v => v.Detalle.Sum(d => d.Precio)))
                                        .ForMember(dto => dto.Detalle,
                                                   opt => opt.MapFrom(v => v.Detalle));

            CreateMap<DetalleVenta, DetalleVentaDTO>()
                                                    .ForMember(dto => dto.DetalleVentaId,
                                                               opt => opt.MapFrom(d => d.DetalleVentaId))
                                                    // Nombre del cliente que compró: navegamos por la venta
                                                    .ForMember(dto => dto.NombreClienteVenta,
                                                               opt => opt.MapFrom(d => d.Venta.Client.Name))
                                                    .ForMember(dto => dto.NombreTipoDeServicioVenta,
                                                               opt => opt.MapFrom(d => d.TipoDeServicio.Name))
                                                    .ForMember(dto => dto.PrecioTipoDeServicio,
                                                               opt => opt.MapFrom(d => d.Precio))  // tu propiedad calculada
                                                    .ForMember(dto => dto.NombreEmpleadoVenta,
                                                               opt => opt.MapFrom(d => d.Empleado.Name));


            CreateMap<DetalleVentaCreacionDTO, DetalleVenta>();
            
        }
    }
}
