using System.Collections.Generic;

namespace Troubadour.Models
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();
        Tag GetById(long id);
        Tag Add(Tag tag);

        void Delete(long tag);
    }
}
