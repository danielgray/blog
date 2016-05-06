using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Troubadour.ViewModels
{
    public class StoryViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public ICollection<TagViewModel> Tags { get; set; }
        public int PublishStatus { get; set; } = 0;
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
        public ICollection<ResponseViewModel> Responses { get; set; }

        public string Author { get; set; } = "Anonymous";
    }
}
