using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using ModelLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        //UC4
        public GreetEntity SaveGreetingRL(GreetingModel greetingModel);

        //UC5
        public GreetingModel GetGreetingByIdRL(int Id);
        //UC6
        public List<GreetEntity> GetAllGreetingsRL();

        //UC7
        public GreetEntity EditGreetingRL(int id, GreetingModel greetingModel);

        //UC8
        public bool DeleteGreetingRL(int id);
    }
}
