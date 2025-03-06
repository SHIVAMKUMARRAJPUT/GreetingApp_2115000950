﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Contexts;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Services
{
    public class GreetingRL:IGreetingRL{

        private readonly GreetingAppContext _dbContext;

        public GreetingRL(GreetingAppContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        }

        //UC4
        public GreetEntity SaveGreetingRL(GreetingModel greetingModel)
        {
            var existingMessage = _dbContext.Greet.FirstOrDefault<GreetEntity>(e => e.Id == greetingModel.Id);
            if (existingMessage == null)
            {
                var newMessage = new GreetEntity
                {

                    Message = greetingModel.Message,
                };
                _dbContext.Greet.Add(newMessage);
                _dbContext.SaveChanges();

                return newMessage;
            }


            return existingMessage;
        }


        //UC5
        public GreetingModel GetGreetingByIdRL(int Id)
        {
            var entity = _dbContext.Greet.FirstOrDefault(g => g.Id == Id);

            if (entity != null)
            {
                return new GreetingModel()
                {
                    Id = entity.Id,
                    Message = entity.Message
                };
            }
            return null;
        }
    }
}
