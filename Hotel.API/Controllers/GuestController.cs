using AutoMapper;
using Hotel.API.Models;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _service;
        private readonly IMapper _mapper;

        public GuestController(IGuestService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        // GET: api/<GuestController>
        [HttpGet]
        public IEnumerable<GuestModel> Get()
        {            
            return _mapper.Map<List<GuestModel>>(_service.GetAllGuests());
        }

        // GET api/<GuestController>/5
        [HttpGet("{id}")]
        public GuestModel Get(int id)
        {
            return _mapper.Map<GuestModel>(_service.Get(id));
        }

        // POST api/<GuestController>
        [HttpPost]
        public void Post([FromBody] GuestModel guest)
        {
            _service.Registration(_mapper.Map<GuestDTO>(guest));
        }
    }
}
