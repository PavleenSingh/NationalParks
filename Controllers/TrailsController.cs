using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParksAPI.Models;
using ParksAPI.Models.DTOs;
using ParksAPI.Models.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParksAPI.Controllers
{
    //[Route("api/Trails")]
    [Route("api/v{version:apiVersion}/trails")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenApiSpecTrails")]

    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepository _npr;
        private readonly IMapper _mapper;
        public TrailsController(ITrailRepository npr, IMapper mapper)
        {
            _npr = npr;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the List of all national parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrailDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetTrails()
        {
            var objList = _npr.GetTrails();
            // using Mapper to return DTO and not the whole domain
            var objDTO = new List<TrailDTO>();
            foreach (var a in objList)
            {
                objDTO.Add(_mapper.Map<TrailDTO>(a));
            }
            return Ok(objDTO);
        }



        /// <summary>
        /// Get a Single National Park
        /// </summary>
        /// <param name="trailID">The id of the Trail</param>
        /// <returns></returns>
        [HttpGet("{trailID:int}", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int trailID)
        {
            var obj = _npr.GetTrail(trailID);
            if (obj == null)
            {
                return NotFound();
            }
            var objDTO = _mapper.Map<TrailDTO>(obj);

            return Ok(objDTO);
        }

        [HttpGet("GetTrailInNationalPark/{nationalParkId:int}", Name = "GetTrailinNationalPArk")]
        [ProducesResponseType(200, Type = typeof(TrailDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrailInNationalPark(int nationalParkId)
        {
            var objList = _npr.GetTrailsInNationalPark(nationalParkId);
            if (objList == null)
            {
                return NotFound();
            }
            var objDtoList = new List<TrailDTO>();
            foreach(var obj in objList)
            {
                objDtoList.Add(_mapper.Map<TrailDTO>(obj));
            }

            return Ok(objDtoList);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(500)]
        public IActionResult CreateTrail([FromBody] TrailCreateDTO ndto)
        {
            if (ndto == null)
            {
                return BadRequest(ModelState);
            }
            if (_npr.TrailExists(ndto.Name))
            {
                ModelState.AddModelError("", "Trail Already Exists");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var trailObj = _mapper.Map<Trail>(ndto);
            if (!_npr.CreateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something Went Wrong When Saving the Record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            //return Ok();
            return CreatedAtRoute("GetTrail", new { trailID = trailObj.Id }, trailObj);
        }


        [HttpPatch("{trailId:int}", Name = "UpdateTrailAPI")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(500)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDTO ndto)
        {
            if (ndto == null || trailId != ndto.Id)
            {
                return BadRequest(ModelState);
            }

            var trailObj = _mapper.Map<Trail>(ndto);


            if (!_npr.UpdateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something Went Wrong When Updating the Record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{trailId}", Name = "DeleteTrailAPI")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(500)]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_npr.TrailExists(trailId))
            {
                ModelState.AddModelError("", "Trail Does Not Exist");
                return StatusCode(404, ModelState);
            }
            var obj = _npr.GetTrail(trailId);

            if (!_npr.DeleteTrail(obj))
            {
                ModelState.AddModelError("", $"Something Went Wrong When Deleting the Record {obj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
