using APICalculos.Application.DTOs.User;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;

namespace APICalculos.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllByStoreAsync(int storeId)
        {
            var users = await _unitOfWork.Users.GetAllByStoreAsync(storeId);
            return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        }

        public async Task<UserResponseDTO?> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
                return null;

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> CreateAsync(UserCreateDTO dto)
        {
            if (await _unitOfWork.Users.UsernameExistsAsync(dto.Username))
                throw new Exception("El username ya existe");

            var user = _mapper.Map<User>(dto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDTO dto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
                return false;

            _mapper.Map(dto, user);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
                return false;

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }

}
