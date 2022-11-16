using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infraestructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Infraestructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly DatabaseContext _context;        
        public StudentRepository(DatabaseContext context) : base(context) 
        {
            _context= context;            
        }

        //TODO NOT COMMON METHODS 

    }
}
