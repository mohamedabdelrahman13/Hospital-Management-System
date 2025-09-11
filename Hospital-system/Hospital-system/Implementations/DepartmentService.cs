using AutoMapper;
using Hospital_system.DTOs;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_system.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IBaseRepository<Department> deptRepo;
        private readonly IMapper mapper;

        public DepartmentService(IBaseRepository<Department> deptRepo
            ,IMapper mapper)
        {
            this.deptRepo = deptRepo;
            this.mapper = mapper;
        }

        public async Task<List<DepartmentDTO>> GetAllDepartments()
        {
            var deptsFromDB =   await deptRepo.GetAll().ToListAsync();
            //Mapping.. 
            var deptsDTO = mapper.Map<List<DepartmentDTO>>(deptsFromDB);

            return deptsDTO;
        }

        public async Task<DepartmentDTO> SearchDepartmentByName(string query)
        {
            var deptFromDB = await deptRepo.GetAll().FirstOrDefaultAsync(x => x.Name.Contains(query) || x.ar_Name.Contains(query));
            if (deptFromDB == null) 
            {
                return null;
            }
            //mapping..
            var deptDTO = mapper.Map<DepartmentDTO>(deptFromDB);
            return deptDTO;

        }
    }
}
