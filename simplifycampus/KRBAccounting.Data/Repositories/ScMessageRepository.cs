﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScMessageRepository : RepositoryBase<ScMessage>, IScMessageRepository
    {
        public ScMessageRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       
    }

    public interface IScMessageRepository : IRepository<ScMessage>
    {
      
    }
}