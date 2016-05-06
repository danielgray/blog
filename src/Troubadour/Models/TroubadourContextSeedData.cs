using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Troubadour.Models
{
    public class TroubadourContextSeedData
    {
        private TroubadourContext _context;
        private UserManager<TroubadourUser> _userManager;

        public TroubadourContextSeedData(UserManager<TroubadourUser> userManager, TroubadourContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task PerformSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("dangrayuk@gmail.com") == null)
            {
                var newUser = new TroubadourUser()
                {
                    UserName = "sayaka",
                    Email = "dangrayuk@gmail.com"
                };

                await _userManager.CreateAsync(newUser, "Passw0rd!");
            }

            if (!_context.Stories.Any())
            {
                var simpleStory = new Story
                {
                    Title = "This is a simple story",
                    Content = "This is the content of the story",
                    PublishStatus = (int)PublishedStatus.Published,
                    PublishedAt = DateTime.UtcNow,
                    Tags = new List<Tag>()
                    {
                        new Tag { Name = "First-Post" },
                        new Tag { Name = "Testing" }
                    },
                    Responses = new List<Response>()
                    {
                        new Response { Author = "Anon", Content = "This is a nice story", PublishedAt = DateTime.UtcNow }
                    }
                };

                _context.Stories.Add(simpleStory);
                _context.Responses.AddRange(simpleStory.Responses);
                _context.Tags.AddRange(simpleStory.Tags);

                _context.SaveChanges();
            }
        }
    }
}
