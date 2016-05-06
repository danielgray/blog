using System;

namespace Troubadour.ViewModels
{
    public class ResponseViewModel
    {
        public long Id { get; set; }
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
        public string Content { get; set; }
        public string Author { get; set; }
        public long StoryId { get; set; }
    }
}