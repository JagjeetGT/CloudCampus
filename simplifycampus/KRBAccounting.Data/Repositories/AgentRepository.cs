using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class AgentRepository : RepositoryBase<Agent>, IAgentRepository
    {
        public AgentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsAgentNameAvailable(string name)
        {
            var Name = name.ToLower();
            var groupName = this.GetMany(x => x.Name.ToLower() == Name).Any();
            return !groupName;
        }
        public bool IsAgentShortNameAvailable(string name)
        {
            var Name = name.ToLower();
            var groupName = this.GetMany(x => x.ShorName.ToLower() == Name).Any();
            return !groupName;
        }
    }

    public interface IAgentRepository : IRepository<Agent>
    {
        bool IsAgentNameAvailable(string name);
        bool IsAgentShortNameAvailable(string name);
    }
}
