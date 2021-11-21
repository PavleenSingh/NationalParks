using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParksAPI.Models;
using ParksAPI.Models.Dtos;
using ParksAPI.Models.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParksAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/nationalParks")]
    [ApiVersion("2.0")]
    [ApiController]
   // [ApiExplorerSettings(GroupName = "ParkyOpenApiSpecNationalParks")]
    public class NationalParksVersion2Controller : ControllerBase
    {
        private readonly INationalParkRepository _npr;
        private readonly IMapper _mapper;
        public NationalParksVersion2Controller(INationalParkRepository npr, IMapper mapper)
        {
            _npr = npr;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the List of all national parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(List<NationalParkDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            var objList = _npr.GetNationalParks();
            // using Mapper to return DTO and not the whole domain
            var objDTO = new List<NationalParkDTO>();
            foreach(var a in objList)
            {
                objDTO.Add(_mapper.Map<NationalParkDTO>(a));
            }
            return Ok(objDTO);
        }

    }
}
