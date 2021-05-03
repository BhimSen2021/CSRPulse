using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public class MenuService : BaseService, IMenuService
    {
        public MenuService(IMapper mapper, IGenericRepository genericRepository) : base(mapper, genericRepository)
        {

        }

        public async Task<List<Menu>> GetMenuByUserAsync(int UserID)
        {
            List<Menu> userMenu = new List<Menu>();
            var uDetail = await _genericRepository.GetAsync<DTOModel.UserRights>(u => u.UserID == UserID && u.ShowMenu == true && u.Menu.IsDeleted == false && u.Menu.IsActive == true);
            userMenu = _mapper.Map<List<Menu>>(uDetail);

            return userMenu;
        }
    }
}
