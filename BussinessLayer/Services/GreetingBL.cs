using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Services;
using RepositoryLayer.Interface;

namespace BusinessLayer.Services;
public class GreetingBL : IGreetingBL{

    private readonly IGreetingRL _greetingRL;

    public GreetingBL(IGreetingRL greetingRL)
    {
        helper();
        _greetingRL = greetingRL;
    }



    private int count = ls.Count+1;
    private static List<RequestModel> ls = new List<RequestModel>();


    //UC2
    public string SayHello()
    {
        return "Hello World !";
    }
    public void helper()
    {
        if (ls.Count == 0)
        {
            ls.Add(new RequestModel { name = "Shivam Kumar", lname = "Rajput", id = count++ });
            ls.Add(new RequestModel { name = "Shudhanshu", lname = "Tripathi", id = count++ });
            ls.Add(new RequestModel { name = "Vaibhav", lname = "Singh", id = count++ });
            ls.Add(new RequestModel { name = "Aditya Kumar", lname = "Sharma", id = count++ });
            ls.Add(new RequestModel { name = "Ashish", lname = "Verma", id = count++ });
            ls.Add(new RequestModel { name = "Harshit", lname = "Thakur", id = count++ });
        }
    }

    public List<RequestModel> GetAll()
    {
        
        return ls;
    }

    public void Add(RequestModel model)
    {
        model.id = count++;
        ls.Add(model);
    }

    public RequestModel GetById(int id)
    {
        return ls.FirstOrDefault(x => x.id == id);
    }

    public void Update(RequestModel model){
        var item = ls.FirstOrDefault(x => x.id == model.id);
        if (item != null){
            item.name = model.name;
            item.lname = model.lname;
        }
    }

    public void Delete(int id){
        var item = ls.FirstOrDefault(x => x.id == id);
        if (item != null){
            ls.Remove(item);
        }

    }

    //UC3
    public string GetGreeting(RequestModel model){
       
        string greetingMessage;

        if (!string.IsNullOrEmpty(model.name) && !string.IsNullOrEmpty(model.lname))
        {
            greetingMessage = $"Hello {model.name} {model.lname}!";
        }
        else if (!string.IsNullOrEmpty(model.name))
        {
            greetingMessage = $"Hello {model.name}!";
        }
        else if (!string.IsNullOrEmpty(model.lname))
        {
            greetingMessage = $"Hello {model.lname}!";
        }
        else
        {
            greetingMessage = "Hello, World!";
        }

         return greetingMessage;
    }

    //UC4

    public GreetEntity SaveGreetingBL(GreetingModel greetingModel)
    {
        var result = _greetingRL.SaveGreetingRL(greetingModel);
        return result;
    }


    //UC5
    public GreetingModel GetGreetingByIdBL(int Id)
    {
        return _greetingRL.GetGreetingByIdRL(Id);
    }

    //UC6

    public List<GreetingModel> GetAllGreetingsBL()
    {
        var entityList = _greetingRL.GetAllGreetingsRL();  // Calling Repository Layer
        if (entityList != null)
        {
            return entityList.Select(g => new GreetingModel
            {
                Id = g.Id,
                Message = g.Message
            }).ToList();  // Converting List of Entity to List of Model
        }
        return null;
    }

    //UC7
    public GreetingModel EditGreetingBL(int id, GreetingModel greetingModel)
    {
        var result = _greetingRL.EditGreetingRL(id, greetingModel); // Calling Repository Layer
        if (result != null)
        {
            return new GreetingModel()
            {
                Id = result.Id,
                Message = result.Message
            };
        }
        return null;
    }

    //UC8
    public bool DeleteGreetingBL(int id)
    {
        var result = _greetingRL.DeleteGreetingRL(id);
        if (result)
        {
            return true; // Successfully Deleted
        }
        return false; // Not Found
    }


}


