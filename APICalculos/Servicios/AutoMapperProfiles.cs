﻿using APICalculos.DTOs;
using APICalculos.Entidades;
using AutoMapper;
using System.Data;

namespace APICalculos.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        { 
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoCreacionDTO, Producto>();

            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteCreacionDTO, Cliente>();

            CreateMap<HistorialClientes, HistorialClienteDTO>()
                .ForMember(dto => dto.NombreCompletoCliente, ent => ent.MapFrom(prop => prop.Cliente.NombreCompletoCliente));
            CreateMap<HistorialClienteCreacionDTO, HistorialClientes>();

            CreateMap<Empleado, EmpleadoDTO>();
            CreateMap<EmpleadoCreacionDTO, Empleado>();

            CreateMap<TipoDePago, TipoDePagoDTO>();
            CreateMap<TipoDePagoCreacionDTO, TipoDePago>();

            CreateMap<TipoDeServicio, TipoDeServicioDTO>()
                .ForMember(dto => dto.NombreCategoriaServicio, ent => ent.MapFrom(prop => prop.CategoriasServicios.NombreCategoriaServicio));
            CreateMap<TipoDeServicioCreacionDTO, TipoDeServicio>();

            CreateMap<Servicio, ServicioDTO>()
                .ForMember(dto => dto.NombreCompletoEmpleado, ent => ent.MapFrom(prop => prop.Empleado.NombreCompletoEmpleado))
                .ForMember(dto => dto.NombreCompletoCliente, ent => ent.MapFrom(prop => prop.Cliente.NombreCompletoCliente))
                .ForMember(dto => dto.NombreTipoDePago, ent => ent.MapFrom(prop => prop.TipoDePago.NombreTipoDePago))
                .ForMember(dto => dto.NombreServicio, ent => ent.MapFrom(prop => prop.TipoDeServicio.NombreServicio));
         
            CreateMap<ServicioCreacionDTO, Servicio>();

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dto => dto.TipoRol, opt => opt.MapFrom(ent => ent.UsuarioRoles.FirstOrDefault().Rol.NombreRol))
                .ForMember(dto => dto.NombreUsuario, opt => opt.MapFrom(ent => ent.UsuarioRoles.FirstOrDefault().Usuario.NombreDeUsuario));
            CreateMap<UsuarioCreacionDTO, Usuario>();

            CreateMap<Rol, RolDTO>();
            CreateMap<RolCreacionDTO, Rol>();

            CreateMap<UsuarioRol, UsuarioRolCreacionDTO>();
            CreateMap<UsuarioRolCreacionDTO, UsuarioRol>();

            CreateMap<Gastos, GastosDTO>()
                .ForMember(dto => dto.NombreTipoDeGastos, ent => ent.MapFrom(prop => prop.TiposDeGastos.NombreTipoDeGastos));
            CreateMap<GastosCreacionDTO, Gastos>();

            CreateMap<TiposDeGastos, TiposDeGastosDTO>();
            CreateMap<TipoDeGastosCreacionDTO, TiposDeGastos>();

            CreateMap<CategoriasServicios, CategoriasServiciosDTO>();
            CreateMap<CategoriasServiciosCreacionDTO, CategoriasServicios>();
            
                





        }
    }
}
