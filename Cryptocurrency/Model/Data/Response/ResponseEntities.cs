using System.Collections.Generic;

namespace Cryptocurrency.Model.Data.Response
{
    public class ResponseEntities<TEntity>
        where TEntity : class
    {
        public List<TEntity> Data { get; set; } = new List<TEntity>();
    }
}
