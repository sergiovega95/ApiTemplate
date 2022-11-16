using ApiTemplate.Infraestructure.DataAccess;
using ApiTemplate.Infraestructure.Repositories;
using Iris.Domain.Interfaces;

namespace Iris.Infraestructure.Repositories
{
    public  class TaskRepository : GenericRepository<Iris.Domain.Entities.Task>, ITaskRepository
    {
        private readonly DatabaseContext _context;

        public TaskRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
