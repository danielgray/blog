using System;
using System.Collections.Generic;

namespace Troubadour.Models
{
    public class Story
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public int PublishStatus { get; set; }
        public DateTime PublishedAt { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
        public string Author { get; set; }
    }
}