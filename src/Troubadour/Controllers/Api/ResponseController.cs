using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using Troubadour.Models;
using Troubadour.ViewModels;

namespace Troubadour.Controllers.Api
{
    public class ResponseController : Controller
    {
        private ILogger<ResponseController> _logger;
        private IStoryRepository _repository;
        private IResponseRepository _responseRepository;

        public ResponseController(IStoryRepository repository, IResponseRepository responseRepository, ILogger<ResponseController> logger)
        {
            _responseRepository = responseRepository;
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("api/responses")]
        public JsonResult Get(long id)
        {
            try
            {
                var results = _repository.GetById(id);

                if (results == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<ResponseViewModel>>(results.Responses));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get responses for story {id}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occured finding story id");
            }
        }

        [HttpPost("api/responses")]
        public JsonResult Post([FromBody]ResponseViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newResposne = Mapper.Map<Response>(vm);

                    newResposne.Author = "anon";
                    newResposne.PublishedAt = DateTime.Now;

                    // write to the database
                    _logger.LogInformation("Attempting to save a response to the database");

                    //var story = vm.StoryId;
                    //story.Responses.Add(newResposne);

                    //_repository.Update(story);

                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return Json(Mapper.Map<ResponseViewModel>(newResposne));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new response", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save repsonse, please try again later");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed for the response");

        }
    }
}
