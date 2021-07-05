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
    public class DepartmentServices : BaseService, IDepartmentServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public DepartmentServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<bool> CreateDepartment(Department department)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Department>(department);

                var res = await _genericRepository.InsertAsync(model);
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

        public async Task<List<Department>> GetDepartmentList()
        {
            try
            {
                var departments = await _genericRepository.GetAsync<DTOModel.Department>();
                var departmentsList = _mapper.Map<List<Department>>(departments);
                return departmentsList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Department> GetDepartmentById(int DepartmentId)
        {
            try
            {
                var department = await _genericRepository.GetByIDAsync<DTOModel.Department>(DepartmentId);
                if (department != null)
                    return _mapper.Map<Department>(department);
                else
                    return new Department();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateDepartment(Department department)
        {
            try
            {
                var dData = await _genericRepository.GetByIDAsync<DTOModel.Department>(department.DepartmentId);
                if (dData != null)
                {
                    if (dData.DepartmentName == department.DepartmentName)
                        return true;

                    dData.DepartmentName = department.DepartmentName;
                    dData.UpdatedBy = department.UpdatedBy;
                    dData.UpdatedOn = department.UpdatedOn;
                    _genericRepository.Update(dData);
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

        public async Task<bool> RecordExist(Department department)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.Department>(x => x.DepartmentName == department.DepartmentName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Department>> GetDepartmentsAsync(Department department)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Department>(x => (department.IsActiveFilter.HasValue ? x.IsActive == department.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<Department>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ActiveDeActive(int id, bool IsActive)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.Department>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
