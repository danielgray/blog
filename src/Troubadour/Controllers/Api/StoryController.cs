using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using Troubadour.Models;
using Troubadour.ViewModels;

namespace Troubadour.Controllers.Api
{
    [Authorize]
    public class StoryController : Controller
    {
        private ILogger<StoryController> _logger;
        private IStoryRepository _repository;

        public StoryController(IStoryRepository repository, ILogger<StoryController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("api/stories")]
        public JsonResult Get()
        {
            var results = Mapper.Map<IEnumerable<StoryViewModel>>(_repository.GetAll());
            return Json(results);
        }

        [HttpPost("api/stories")]
        public JsonResult Post([FromBody]StoryViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var newStory = Mapper.Map<Story>(viewModel);
                        newStory.Author = User.Identity.Name;

                        // write to the database
                        _logger.LogInformation("Attempting to save story to the database");
                        _repository.Add(newStory);

                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(new { Data = Mapper.Map<StoryViewModel>(newStory), Message = "Created" });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to save story", ex);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Data = "", Message = ex.Message });
                }
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Data = "", Message = "Validation failed for the story" });
        }

        [HttpPut("api/stories")]
        public JsonResult Put([FromBody]StoryViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var newStory = Mapper.Map<Story>(viewModel);
                        newStory.Author = User.Identity.Name;

                        // write to the database
                        _logger.LogInformation(String.Format("Attempting to update story id {0} in the database", viewModel.Id));
                        _repository.Update(newStory);

                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StoryViewModel>(newStory));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to update story", ex);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Message = ex.Message });
                }
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Validation failed for the story" });
        }

        [HttpDelete("api/stories/{id}")]
        public JsonResult Delete(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // remove form the database
                        _logger.LogInformation(String.Format("Attempting to delete story id {0} from the database", id));

                        _repository.Delete(id);

                        Response.StatusCode = (int)HttpStatusCode.NoContent;
                        return Json(null);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to delete story", ex);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Message = ex.Message });
                }
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Validation failed for the story" });
        }
    }
}
