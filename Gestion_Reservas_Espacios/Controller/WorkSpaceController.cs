using DataAccess;
using DataAccess.Repos;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Reservas_Espacios.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSpaceController : ControllerBase
    {
        private readonly IGenericRepository<WorkSpace> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkSpaceRepository _workSpaceRepository;

        public WorkSpaceController(IGenericRepository<WorkSpace> genericRepository, IUnitOfWork unitOfWork, IWorkSpaceRepository workSpaceRepository)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _workSpaceRepository = workSpaceRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<WorkSpace>> Get()
        {
            return await _workSpaceRepository.GetAllGrouped();
            //return await _workSpaceRepository.GetAll();
        }
    }
}
