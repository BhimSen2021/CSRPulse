using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                var result = await Task.FromResult(_rightRepository.GetMenuList());
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

                var result = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.Menu>(x => x.IsActive==true).Include(r=>r.UserRights).Where(c=>c.UserRights.Any(r=>r.RoleId==roleId)));

                lstMenu = _mapper.Map<List<RoleAccessRights>>(result);
                return lstMenu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertUpdateRoleAccessRights(int roleId, List<RoleAccessRights> listUpdateRoleRights, BaseModel baseModel)
        {
            try
            {
                foreach (var item in listUpdateRoleRights)
                {
                    DTOModel.UserRights obj = new DTOModel.UserRights();
                    if (_genericRepository.Get<DTOModel.UserRights>()
                        .Any(x => x.RoleId == roleId && x.MenuId == item.MenuId))
                    {
                        obj = _genericRepository.Get<DTOModel.UserRights>(x => x.RoleId == roleId && x.MenuId == item.MenuId).FirstOrDefault();
                        obj.ViewRight = item.ViewRight;
                        obj.CreateRight = item.CreateRight;
                        obj.EditRight = item.EditRight;
                        obj.DeleteRight = item.DeleteRight;
                        obj.UpdatedBy = baseModel.UpdatedBy;
                        obj.UpdatedOn = baseModel.UpdatedOn;
                        obj.UpdatedRid = baseModel.UpdatedRid;
                        obj.UpdatedRname = baseModel.UpdatedRname;

                        _genericRepository.Update<DTOModel.UserRights>(obj);
                    }
                    else
                    {
                        obj.MenuId = item.MenuId;
                        obj.RoleId = roleId;                        
                        obj.ShowMenu = true;
                        obj.ViewRight = item.ViewRight;
                        obj.CreateRight = item.CreateRight;
                        obj.EditRight = item.EditRight;
                        obj.DeleteRight = item.DeleteRight;
                        obj.CreatedBy = baseModel.CreatedBy;
                        obj.CreatedOn = baseModel.CreatedOn;
                        obj.CreatedRid = baseModel.CreatedRid;
                        obj.CreatedRname = baseModel.CreatedRname;
                        _genericRepository.Insert<DTOModel.UserRights>(obj);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
