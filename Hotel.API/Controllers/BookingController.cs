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
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;
        private readonly IMapper _mapper;

        public BookingController(IBookingService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        // GET: api/<BookingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public BookingModel Get(int id)
        {
            return _mapper.Map<BookingModel>(_service.Get(id));
        }

        // POST api/<BookingController>
        [HttpPost]
        public void Post([FromBody] BookingModel booking)
        {
            _service.Booking(_mapper.Map<BookingDTO>(booking));
        }

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
