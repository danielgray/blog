using System;

namespace Troubadour.Models
{
    public class Response
    {
        public long Id { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public virtual Story Story { get; set; }
    }
}