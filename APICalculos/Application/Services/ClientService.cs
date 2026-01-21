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

        public async Task<List<ClientDTO>> GetAllClientsAsync()
        {
            var clientes = await _clientRepository.GetAllAsync();
            return _mapper.Map<List<ClientDTO>>(clientes);
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
            if (string.IsNullOrWhiteSpace(clienteCreacionDTO.Name))
                throw new ArgumentException("El nombre no puede estar vacío");

            var existeNombre = await _clientRepository.ExistsByNombreAsync(clienteCreacionDTO.Name);
            if (existeNombre)
                throw new InvalidOperationException("El nombre del cliente ya existe");

            var existeDocumento = !string.IsNullOrWhiteSpace(clienteCreacionDTO.IdentityDocument) &&
                await _clientRepository.ExistsByDocumentoAsync(clienteCreacionDTO.IdentityDocument);

            if (existeDocumento)
                throw new InvalidOperationException("El documento ya está registrado");

            var cliente = _mapper.Map<Client>(clienteCreacionDTO);

            await _unitOfWork.Clients.AddAsync(cliente);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ClientDTO>(cliente);
        }

        public async Task UpdateAsync(int id, ClientCreationDTO clienteCreacionDTO)
        {
            var clienteDB = await _clientRepository.GetByIdAsync(id);

            if (clienteDB == null)
                throw new KeyNotFoundException("Cliente no encontrado");

            if (!string.IsNullOrWhiteSpace(clienteCreacionDTO.Name))
                clienteDB.Name = clienteCreacionDTO.Name;

            if (!string.IsNullOrWhiteSpace(clienteCreacionDTO.IdentityDocument))
                clienteDB.IdentityDocument = clienteCreacionDTO.IdentityDocument;

            if (clienteCreacionDTO.DateBirth != default)
                clienteDB.DateBirth = clienteCreacionDTO.DateBirth;

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
