using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Troubadour.Models
{
    public class ResponseRepository : IResponseRepository
    {
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public ResponseRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IEnumerable<Response> GetAll()
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var repository = unitOfWork.GetRepository<Response>();
                return repository.GetAll().ToList();
            }
        }

        public Response GetById(long id)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var repository = unitOfWork.GetRepository<Response>();
                return repository.Get(x => x.Id == id).FirstOrDefault();
            }
        }

        //public IEnumerable<Response> Get(Expression<Func<Response, bool>> predicate)
        //{
        //    return GetQueryable(predicate).AsEnumerable();
        //}

        //public IQueryable<Response> GetQueryable(Expression<Func<Response, bool>> predicate)
        //{
        //    using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
        //    {
        //        var repository = unitOfWork.GetRepository<Response>();
        //        return repository.Get(predicate);
        //    }
        //}

        public Response Add(Response response)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var repository = unitOfWork.GetRepository<Response>();
                repository.Add(response);
                unitOfWork.SaveChanges();

                return response;
            }
        }

        //public Story Update(Story reminder)
        //{
        //    using (IUnitOfWork unitOfWork = UnitOfWorkFactory.Create())
        //    {
        //        var updateableModel = reminder as IUpdateable;
        //        if (updateableModel != null) { updateableModel.DateUpdated = TimeManager.Now; }

        //        var repository = unitOfWork.GetRepository<DateReminder>();
        //        DateReminder dbModel = repository.Get(s => s.Id == reminder.Id).FirstOrDefault();
        //        if (dbModel != null)
        //        {
        //            PropertyMapperFactory.Create<DateReminder, DateReminder>().Map(reminder, dbModel);
        //            unitOfWork.SaveChanges();
        //        }

        //        return reminder;
        //    }
        //}

        public void Delete(long response)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var repository = unitOfWork.GetRepository<Response>();

                var resposne = repository.Get(x => x.Id == response).FirstOrDefault();

                if (resposne != null)
                {
                    repository.Delete(resposne);
                    //dbModel.DateDeleted = TimeManager.Now;
                    unitOfWork.SaveChanges();
                }
            }
        }
    }
}
