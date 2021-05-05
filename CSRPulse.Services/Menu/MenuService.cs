using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Data.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CSRPulse.Services
{
    public class MenuService : BaseService, IMenuService
    {

        public MenuService(IMapper mapper, IGenericRepository genericRepository) : base(mapper, genericRepository)
        {

        }

        public async Task<List<Menu>> GetMenuByUserAsync(int userId)
        {
            List<Menu> uMenu = new List<Menu>();
            var uRights = await _genericRepository.GetIQueryable<DTOModel.UserRights>(x => x.UserId == userId && x.ShowMenu == true).Include(d => d.Menu).ToListAsync();

            uRights.ForEach(r =>
            {
                uMenu.Add(new Menu
                {
                    MenuId = r.MenuId,
                    SequenceNo = r.Menu.SequenceNo,
                    MenuName = r.Menu.MenuName,
                    Area = r.Menu.Area,
                    Url = r.Menu.Url,
                    ParentMenuId = r.Menu.ParentMenuId,
                    IconClass = r.Menu.IconClass,
                    Help = r.Menu.Help
                });
            });

            return uMenu;
        }

        public async Task<List<UserRight>> GetUserRightsByUserAsync(int userId)
        {
            List<UserRight> userRigth = new List<UserRight>();
            var uRigth = await _genericRepository.GetAsync<DTOModel.UserRights>(r => r.UserId == userId && r.ShowMenu == true);

            _mapper.Map<List<Model.UserRight>>(uRigth);
            return userRigth;
        }
    }
}
