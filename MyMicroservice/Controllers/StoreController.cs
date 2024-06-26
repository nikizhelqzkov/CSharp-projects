﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.DataAccess.Requests;
using MyMicroservice.DTOModels;
using MyMicroservice.Models;
using MyMicroservice.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyMicroservice.Controllers
{
    [Route("api/store")]
    [ApiController]
    [Authorize]
    public class StoreController : ControllerBase
    {
        // GET: api/<ValuesController>
        private readonly IStoreService _service;
        public StoreController(IStoreService service)
        {
            _service = service;
        }
        [HttpGet, AllowAnonymous]
        public ActionResult<List<StoreDTO>> GetAllStores()
        {
            var result = _service.GetStores();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<StoreDTO> GetStore(int id)
        {
            var result = _service.GetStoreById(id);
            if (result == null)
            {
                return NotFound(id);
            }
            return Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] StoreRequest request)
        {

            _service.AddStore(request);
            return Created($"Added new store with id", request);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StoreDTO request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            _service.UpdateStoreById(id, request);
            return Ok();

        }

    }
}
