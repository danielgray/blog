namespace Troubadour.Models
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual Story Story { get; set; }
    }
}
