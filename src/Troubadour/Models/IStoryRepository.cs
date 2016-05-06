using System.Collections.Generic;

namespace Troubadour.Models
{
    public interface IStoryRepository
    {
        IEnumerable<Story> GetAll();
        Story GetById(long id);
        Story Add(Story story);
        Story Update(Story story);
        void Delete(long story);
    }
}