using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        //UC4
        public GreetEntity SaveGreetingRL(GreetingModel greetingModel);

        //UC5
        public GreetingModel GetGreetingByIdRL(int Id);
    }
}
