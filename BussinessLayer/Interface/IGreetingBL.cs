using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        public string SayHello();
        public void helper();
        public List<RequestModel> GetAll();
        public void Add(RequestModel model);
        public RequestModel GetById(int id);
        public void Update(RequestModel model);
        public void Delete(int id);
        public string GetGreeting(RequestModel model);

        //UC4
        public GreetEntity SaveGreetingBL(GreetingModel greetingModel);
    }
}
