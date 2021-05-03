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

        public List<Menu> GetMenuByUserAsync(int UserID)
        {
            List<Menu> userMenu = new List<Menu>();
            Menu uMenu = new Menu();

            var uDetail = _genericRepository.Get<DTOModel.UserRights>(u => u.UserId == UserID).FirstOrDefault();


            //userMenu = _mapper.Map<List<Menu>>(uDetail.Menu);

            return userMenu;
        }
    }
}
