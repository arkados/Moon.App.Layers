using Moon.DAL;
namespace Moon.BLL
{
    /// <summary>
    /// Vrátí objekt implementující rozhraní ISimpleQuery
    /// </summary>
    /// <typeparam name="T">Type Entity</typeparam>
   public interface IReadOnlyService<T>
   {
       ISimpleQuery<T> Query { get; }
   }
}
