using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Entity;
using ModelLayer.Model;
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

        //UC5
        public GreetingModel GetGreetingByIdBL(int Id);
        //UC6
        public List<GreetingModel> GetAllGreetingsBL();
        //UC7
        public GreetingModel EditGreetingBL(int id, GreetingModel greetingModel);

        //UC8
        public bool DeleteGreetingBL(int id);
    }
}
