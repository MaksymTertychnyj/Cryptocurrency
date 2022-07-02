namespace Cryptocurrency.Model.Data.Response
{
    public class ResponseEntity<TEntity>
        where TEntity : class
    {
        public TEntity Data { get; set; }

        public ResponseEntity(TEntity entity)
        {
            Data = entity;
        }
    }
}
