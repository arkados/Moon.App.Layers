namespace Moon.DAL
{
    public  interface IRepository<T> : IRepositoryReadOnly<T>
   {
       void Insert(T entity);
       void Update(T entity);
       void Delete(T entity);
       void Attach(T entity);
   }
}


