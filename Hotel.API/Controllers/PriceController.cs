using AutoMapper;
using Hotel.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService _service;
        private readonly IMapper _mapper;

        public PriceController(IPriceService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        // GET: api/<PriceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/<PriceController>/5
        [HttpGet("{data}")]
        public int Get(string data)
        {
            if (DateTime.TryParse(data, out DateTime date))
                return  _service.ProfitPerMonth(date);
            else return -1;
        }

        // POST api/<PriceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PriceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PriceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
