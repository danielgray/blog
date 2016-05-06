using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Troubadour.Extensions;

namespace Troubadour.Models
{
    public class StoryRepository : IStoryRepository
    {
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public StoryRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IEnumerable<Story> GetAll()
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var repository = unitOfWork.GetRepository<Story>();
                return repository.GetAll().IncludeMultiple(x => x.Responses, x => x.Tags).ToList();
            }
        }

        public Story GetById(long id)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var repository = unitOfWork.GetRepository<Story>();
                return repository.Get(x => x.Id == id).IncludeMultiple(x => x.Responses, x => x.Tags).FirstOrDefault();
            }
        }

        //public IEnumerable<Story> Get(Expression<Func<Story, bool>> predicate)
        //{
        //    return GetQueryable(predicate).AsEnumerable();
        //}

        //public IQueryable<Story> GetQueryable(Expression<Func<Story, bool>> predicate)
        //{
        //    using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
        //    {
        //        var repository = unitOfWork.GetRepository<Story>();
        //        return repository.Get(predicate);
        //    }
        //}

        public Story Add(Story story)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var repository = unitOfWork.GetRepository<Story>();
                repository.Add(story);
                unitOfWork.SaveChanges();
                return story;
            }
        }

        public Story Update(Story story)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var repository = unitOfWork.GetRepository<Story>();

                Story dbModel = repository.Get(s => s.Id == story.Id).FirstOrDefault();

                if (dbModel != null)
                {
                    Mapper.Map(story, dbModel);
                    dbModel.Responses = null;
                    dbModel.Tags = null;
                    repository.Update(dbModel);

                    unitOfWork.SaveChanges();
                }

                return story;
            }
        }

        public void Delete(long id)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                // create all repositories
                var repository = unitOfWork.GetRepository<Story>();
                var responseRepository = unitOfWork.GetRepository<Response>();
                var tagRepository = unitOfWork.GetRepository<Tag>();

                // populate the story object
                Story story = repository.Get(s => s.Id == id).FirstOrDefault();
                story.Responses = responseRepository.GetAll().Where(r => r.Story.Id == id).ToArray();
                story.Tags = tagRepository.GetAll().Where(t => t.Story.Id == id).ToArray();

                foreach (var response in story.Responses)
                {
                    if (response != null)
                    {
                        responseRepository.Delete(response);
                    }
                }

                foreach (var tag in story.Tags)
                {
                    if (tag != null)
                    {
                        tagRepository.Delete(tag);
                    }
                }

                repository.Delete(story);
                unitOfWork.SaveChanges();
            }
        }
    }
}
