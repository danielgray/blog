using System.Collections.Generic;

namespace Troubadour.Models
{
    public interface IResponseRepository
    {
        IEnumerable<Response> GetAll();
        Response GetById(long id);        
        Response Add(Response resposne);
        
        void Delete(long response);
    }
}