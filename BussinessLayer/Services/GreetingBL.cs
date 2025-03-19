using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Services;
using RepositoryLayer.Interface;
using ModelLayer.Entity;
using NLog;

namespace BusinessLayer.Services;
public class GreetingBL : IGreetingBL
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    private readonly IGreetingRL _greetingRL;

    public GreetingBL(IGreetingRL greetingRL)
    {
        Logger.Info("GreetingBL constructor called.");
        helper();
        _greetingRL = greetingRL;
    }

    private int count = ls.Count + 1;
    private static List<RequestModel> ls = new List<RequestModel>();

    //UC2
    public string SayHello()
    {
        Logger.Info("SayHello method called.");
        return "Hello World !";
    }

    public void helper()
    {
        Logger.Info("Initializing helper method.");
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
        Logger.Info("GetAll method called.");
        return ls;
    }

    public void Add(RequestModel model)
    {
        Logger.Info("Adding new request: {0} {1}", model.name, model.lname);
        model.id = count++;
        ls.Add(model);
    }

    public RequestModel GetById(int id)
    {
        Logger.Info("Fetching request by ID: {0}", id);
        return ls.FirstOrDefault(x => x.id == id);
    }

    public void Update(RequestModel model)
    {
        Logger.Info("Updating request with ID: {0}", model.id);
        var item = ls.FirstOrDefault(x => x.id == model.id);
        if (item != null)
        {
            item.name = model.name;
            item.lname = model.lname;
        }
    }

    public void Delete(int id)
    {
        Logger.Info("Deleting request with ID: {0}", id);
        var item = ls.FirstOrDefault(x => x.id == id);
        if (item != null)
        {
            ls.Remove(item);
        }
    }

    //UC3
    public string GetGreeting(RequestModel model)
    {
        Logger.Info("Generating greeting for {0} {1}", model.name, model.lname);
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
        Logger.Info("Saving greeting: {0}", greetingModel.Message);
        var result = _greetingRL.SaveGreetingRL(greetingModel);
        return result;
    }

    //UC5
    public GreetingModel GetGreetingByIdBL(int Id)
    {
        Logger.Info("Fetching greeting by ID: {0}", Id);
        return _greetingRL.GetGreetingByIdRL(Id);
    }





    //UC6
    public List<GreetingModel> GetAllGreetingsBL()
    {
        Logger.Info("Fetching all greetings.");
        var entityList = _greetingRL.GetAllGreetingsRL();
        if (entityList != null)
        {
            return entityList.Select(g => new GreetingModel
            {
                Id = g.Id,
                Message = g.Message,
                Uid = g.UserId // Ensure Uid is included
            }).ToList();
        }
        return null;
    }



    //UC7
    public GreetingModel EditGreetingBL(int id, GreetingModel greetingModel)
    {
        Logger.Info("Editing greeting ID: {0}", id);
        var result = _greetingRL.EditGreetingRL(id, greetingModel);
        if (result != null)
        {
            return new GreetingModel()
            {
                Id = result.Id,
                Message = result.Message,
                Uid = result.UserId // Ensure Uid is returned
            };
        }
        return null;
    }


    //UC8
    public bool DeleteGreetingBL(int id)
    {
        Logger.Info("Deleting greeting ID: {0}", id);
        var result = _greetingRL.DeleteGreetingRL(id);
        return result;
    }
}
