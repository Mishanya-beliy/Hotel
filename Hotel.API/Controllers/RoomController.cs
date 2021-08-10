using AutoMapper;
using Hotel.API.Models;
using Hotel.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;
        private readonly IMapper _mapper;

        public RoomController(IRoomService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }
        // GET: api/<GuestController>
        [HttpGet]
        public IEnumerable<RoomModel> Get()
        {
            return _mapper.Map<List<RoomModel>>(_service.GetAllRooms());
        }

        // GET api/<GuestController>/5
        [HttpGet("{id}")]
        public RoomModel Get(int id)
        {
            return _mapper.Map<RoomModel>(_service.Get(id));
        }

        // GET api/<RoomController>/5
        [HttpGet("{start}/{end}/{countPeople}")]
        public IEnumerable<RoomModel> FreeRoom(string start, string end, string countPeople)
        {
            if (DateTime.TryParse(start, out DateTime startDate))
                if (DateTime.TryParse(end, out DateTime endDate))
                    if (int.TryParse(countPeople, out int people))
                        return _mapper.Map<List<RoomModel>>(_service.GetFreeRooms(startDate, endDate, people));
            
            return null;
        }  
    }
}
