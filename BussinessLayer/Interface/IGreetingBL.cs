using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        public string SayHello();
<<<<<<< HEAD
        string GetGreeting(string? firstName, string? lastName);
=======
        public void helper();
        public List<RequestModel> GetAll();
        public void Add(RequestModel model);
        public RequestModel GetById(int id);
        public void Update(RequestModel model);
        public void Delete(int id);
>>>>>>> UC2
    }
}
