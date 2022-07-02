using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Model.Data.Response
{
    public class ResponseEntities<TEntity>
        where TEntity : class
    {
        public List<TEntity> Data { get; set; }
    }
}
