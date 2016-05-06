using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System.Linq;
using Troubadour.Models;
using Troubadour.Services;

namespace Troubadour.Controllers.Web
{
    public class BlogController : Controller
    {
        private IMailService _mailService;
        private IStoryRepository _storyRepository;

        public BlogController(IMailService mailService, IStoryRepository storyRepository)
        {
            _mailService = mailService;
            _storyRepository = storyRepository;
        }

        public IActionResult Index()
        {
            var stories = _storyRepository.GetAll().OrderByDescending(x => x.PublishedAt).ToList();
            
            return View(stories);
        }

        public IActionResult Story(long id)
        {
            var story = _storyRepository.GetById(id);

            return View(story);
        }

        [Authorize]
        public IActionResult NewStory()
        {
            return View();
        }

        [Authorize]
        public IActionResult EditStory(long id)
        {
            var story = _storyRepository.GetById(id);

            return View(story);
        }

        [Authorize]
        public IActionResult Admin()
        {
            var stories = _storyRepository.GetAll().ToList();

            return View(stories);
        }

    }
}