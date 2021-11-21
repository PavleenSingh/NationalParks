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
    [ApiController]
   // [ApiExplorerSettings(GroupName = "ParkyOpenApiSpecNationalParks")]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository _npr;
        private readonly IMapper _mapper;
        public NationalParksController(INationalParkRepository npr, IMapper mapper)
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
        


        /// <summary>
        /// Get a Single National Park
        /// </summary>
        /// <param name="nationalParkID">The id of the National Park</param>
        /// <returns></returns>
        [HttpGet("{nationalParkID:int}",Name ="GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int nationalParkID)
        {
            var obj = _npr.GetNationalPark(nationalParkID);
            if(obj==null)
            {
                return NotFound();
            }
            var objDTO = _mapper.Map<NationalParkDTO>(obj);
            /* // IF Auto Mapper was not there
            // So for that case we do mapping like this
            var objdto = new NationalPark()
            {
                Created = obj.Created,
                Id=obj.Id,
                Name=obj.Name,
                State=obj.State
            }
            */

            
            return Ok(objDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NationalParkDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateNationalPark([FromBody] NationalParkDTO ndto)
        {
            if(ndto==null)
            {
                return BadRequest(ModelState);
            }
            if(_npr.NationalParkExists(ndto.Name))
            {
                ModelState.AddModelError("", "National Park Already Exists");
                return StatusCode(404, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var NationalParkObj = _mapper.Map<NationalPark>(ndto);
            if(!_npr.CreateNationalPark(NationalParkObj))
            {
                ModelState.AddModelError("",$"Something Went Wrong When Saving the Record {NationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            //return Ok();
            return CreatedAtRoute("GetNationalPark", new { version=HttpContext.GetRequestedApiVersion().ToString(),
                nationalParkID = NationalParkObj.Id }, NationalParkObj);
        }

        
        [HttpPatch("{nationalParkId:int}",Name ="UpdateAPI")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(500)]
        public IActionResult UpdateNationalPark(int nationalparkId,[FromBody] NationalParkDTO ndto)
        {
            if (ndto == null || nationalparkId!=ndto.Id)
            {
                return BadRequest(ModelState);
            }

            var NationalParkObj = _mapper.Map<NationalPark>(ndto);


            if (!_npr.UpdateNationalPark(NationalParkObj))
            {
                ModelState.AddModelError("", $"Something Went Wrong When Updateing the Record {NationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{nationalParkId}",Name="DeleteAPI")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(500)]
        public IActionResult DeleteNationalPark(int nationalparkId)
        {
            if(!_npr.NationalParkExists(nationalparkId))
            {
                ModelState.AddModelError("", "National Park Does Not Exist");
                return StatusCode(404, ModelState);
            }
            var obj = _npr.GetNationalPark(nationalparkId);

            if (!_npr.DeleteNationalPark(obj))
            {
                ModelState.AddModelError("", $"Something Went Wrong When Deleting the Record {obj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
