﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ClassTeacherMappingRepository: RepositoryBase<ClassTeacherMapping>, IClassTeacherMappingRepository
    {
        public ClassTeacherMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       
    }

    public interface IClassTeacherMappingRepository : IRepository<ClassTeacherMapping>
    {
      
    }
}
