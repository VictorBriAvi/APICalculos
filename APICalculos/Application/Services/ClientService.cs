using APICalculos.Application.DTOs;
using APICalculos.Application.DTOs.Client;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Repositories;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace APICalculos.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IClientRepository clientRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClientDTO>> GetAllClientsAsync(string? search)
        {
            var clients =
                await _clientRepository.GetAllAsync(search);

            return _mapper.Map<List<ClientDTO>>(clients);
        }

        public async Task<ClientDTO?> GetClientForIdAsync(int id)
        {
            var cliente = await _clientRepository.GetByIdAsync(id);
            if (cliente == null)
                return null;

            return _mapper.Map<ClientDTO>(cliente);
        }


        public async Task<ClientDTO> AddAsync(ClientCreationDTO clienteCreacionDTO)
        {
            // Regla principal
            if (
                string.IsNullOrWhiteSpace(clienteCreacionDTO.Name) &&
                string.IsNullOrWhiteSpace(clienteCreacionDTO.Email) &&
                string.IsNullOrWhiteSpace(clienteCreacionDTO.Phone)
            )
            {
                throw new ArgumentException(
                    "Debe ingresar al menos Nombre, Email o Teléfono"
                );
            }

            if (!string.IsNullOrWhiteSpace(clienteCreacionDTO.Name))
            {
                var existeNombre = await _clientRepository.ExistsByNombreAsync(clienteCreacionDTO.Name);
                if (existeNombre)
                    throw new InvalidOperationException("El nombre del cliente ya existe");
            }

            if (!string.IsNullOrWhiteSpace(clienteCreacionDTO.Email))
            {
                var existeEmail = await _clientRepository.ExistsByEmailAsync(clienteCreacionDTO.Email);
                if (existeEmail)
                    throw new InvalidOperationException("El email ya está registrado");
            }

            if (!string.IsNullOrWhiteSpace(clienteCreacionDTO.Phone))
            {
                var existePhone = await _clientRepository.ExistsByPhoneAsync(clienteCreacionDTO.Phone);
                if (existePhone)
                    throw new InvalidOperationException("El teléfono ya está registrado");
            }

            var cliente = _mapper.Map<Client>(clienteCreacionDTO);

            await _unitOfWork.Clients.AddAsync(cliente);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ClientDTO>(cliente);
        }


        public async Task UpdateAsync(int id, ClientUpdateDTO clienteUpdateDTO)
        {
            var clienteDB = await _clientRepository.GetByIdAsync(id);

            if (clienteDB == null)
                throw new KeyNotFoundException("Cliente no encontrado");

            if (!string.IsNullOrWhiteSpace(clienteUpdateDTO.Name))
                clienteDB.Name = clienteUpdateDTO.Name;

            if (clienteUpdateDTO.IdentityDocument != null)
                clienteDB.IdentityDocument = clienteUpdateDTO.IdentityDocument;

            if (clienteUpdateDTO.Email != null)
                clienteDB.Email = clienteUpdateDTO.Email;

            if (clienteUpdateDTO.Phone != null)
                clienteDB.Phone = clienteUpdateDTO.Phone;

            // ✅ FECHA: correcto
            if (clienteUpdateDTO.DateBirth.HasValue)
                clienteDB.DateBirth = clienteUpdateDTO.DateBirth.Value;

            _clientRepository.Update(clienteDB);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var clienteDB = await _clientRepository.GetByIdAsync(id);
            if (clienteDB == null)
                throw new KeyNotFoundException("Cliente no encontrado");

            try
            {
                _clientRepository.Remove(clienteDB);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                throw new InvalidOperationException("No se puede eliminar este cliente porque está asociado a una venta.");
            }
        }

        public async Task<List<ClientSearchDTO>> SearchClientsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<ClientSearchDTO>();

            var clients = await _clientRepository.SearchAsync(query.Trim(), 15);

            return _mapper.Map<List<ClientSearchDTO>>(clients);
        }


    }
}
