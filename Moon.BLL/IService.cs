namespace Moon.BLL
{
   public interface IService<T> : IReadOnlyService<T>
   {
       int Delete(T entity);
       int Insert(T entity);
       int Update(T entity);
   }
}
