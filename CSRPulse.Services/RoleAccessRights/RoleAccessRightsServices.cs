using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CSRPulse.Services
{
    public class RoleAccessRightsServices : BaseService, IRoleAccessRightsServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IRoleAccessRightRepository _rightRepository;
        private readonly IMapper _mapper;
        public RoleAccessRightsServices(IGenericRepository generic, IRoleAccessRightRepository rightRepository, IMapper mapper)
        {
            _genericRepository = generic;
            _rightRepository = rightRepository;
            _mapper = mapper;
        }


        public async Task<List<RoleAccessRights>> GetMenuListAsync()
        {
            try
            {
                var result2 = await _genericRepository.GetAsync<DTOModel.Menu>(x => x.IsActive == true);
                var result = _rightRepository.GetMenuList();
                var lstMenu = _mapper.Map<List<RoleAccessRights>>(result);
                return lstMenu;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<RoleAccessRights>> GetRoleAccessMenuListAsync(int roleId)
        {
            try
            {
                List<RoleAccessRights> lstMenu = new List<RoleAccessRights>();
                var result = await _genericRepository.GetAsync<DTOModel.Menu>(x => x.UserRights.Any(r => r.RoleId == roleId));

                lstMenu = _mapper.Map<List<RoleAccessRights>>(result);
                return lstMenu.OrderBy(x => x.MenuId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
