<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using NLog;

namespace BusinessLayer.Services
{
    public class GreetingBL : IGreetingBL
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public string GetGreeting(string? firstName, string? lastName)
        {
            _logger.Info("Received request in Business Layer. First Name: {0}, Last Name: {1}", firstName, lastName);

            string greetingMessage;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                greetingMessage = $"Hello {firstName} {lastName}!";
            }
            else if (!string.IsNullOrEmpty(firstName))
            {
                greetingMessage = $"Hello {firstName}!";
            }
            else if (!string.IsNullOrEmpty(lastName))
            {
                greetingMessage = $"Hello {lastName}!";
            }
            else
            {
                greetingMessage = "Hello, World!";
            }

            _logger.Info("Generated Greeting Message in Business Layer: {0}", greetingMessage);
            return greetingMessage;
        }

        public string SayHello()
        {
            string message = "Hello World";
            _logger.Info("SayHello method called. Message: {0}", message);
            return message;
=======
﻿using BusinessLayer.Interface;
using ModelLayer.Model;

public class GreetingBL : IGreetingBL{
    private int count = 1;
    private List<RequestModel> ls = new List<RequestModel>();
    public GreetingBL(){
        helper();
    }
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
>>>>>>> UC2
        }
    }
}


