using AutoMapper;
using CSRPulse.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
namespace CSRPulse.Services
{
    public class RoleServices : BaseService, IRoleServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public RoleServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<bool> CreateRole(Model.Role role)
        {
            try
            {
                var dtoRole = _mapper.Map<DTOModel.Role>(role);

                var res = await _genericRepository.InsertAsync(dtoRole);
                if (res > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Model.Role>> GetRoles()
        {
            try
            {
                var getRoles = await _genericRepository.GetAsync<DTOModel.Role>();
                var dd= _mapper.Map<List<Model.Role>>(getRoles);
                return dd;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Model.Role> GetRolesById(int roleId)
        {
            try
            {
                var getRoles = await _genericRepository.GetByIDAsync<DTOModel.Role>(roleId);
                if (getRoles != null)
                    return _mapper.Map<Model.Role>(getRoles);
                else
                    return new Model.Role();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdateRole(Model.Role role)
        {
            try
            {

                var getRoles = await _genericRepository.GetByIDAsync<DTOModel.Role>(role.RoleId);
                if (getRoles != null)
                {
                    getRoles.RoleName = role.RoleName;
                    getRoles.RoleShortName = role.RoleShortName;
                    _genericRepository.Update(getRoles);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
