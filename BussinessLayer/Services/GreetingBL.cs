﻿using BusinessLayer.Interface;
using ModelLayer.Model;

public class GreetingBL : IGreetingBL{
    private int count = 1;
    private List<RequestModel> ls = new List<RequestModel>();
    public GreetingBL(){
        helper();
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
}


