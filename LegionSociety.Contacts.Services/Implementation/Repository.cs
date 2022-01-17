﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services.Implementation
{

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext Context;
        public Repository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public async Task Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> GetById(params object[] id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }
    }
}
