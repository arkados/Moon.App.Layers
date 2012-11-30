namespace Moon.DAL
{
    public interface IUnitOfWork 
   {
       int Commit();
       bool IsAttach(object entity);
       void Detach(object entity);
   }
}
