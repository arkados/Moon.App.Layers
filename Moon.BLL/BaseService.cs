using Moon.DAL;

namespace Moon.BLL
{
   public class BaseService<T>: BaseReadOnlySimpleService<T>, IService<T>
   {
       protected new IRepository<T> Repository;
       private readonly IUnitOfWork _unitOfWork;
     
       protected IUnitOfWork UnitOfWork
       {
           get { return _unitOfWork; }
       }

       public BaseService(IUnitOfWork unitOfWork, IRepository<T> repository ):base(repository)
       {
           _unitOfWork = unitOfWork;
           Repository = repository;

       }

       /// <summary>
       /// Smaže předanou Entitu.
       /// </summary>
       /// <param name="entity">Entita pro smazaní</param>
       /// <returns>
       /// Počet odstraněných entit.
       /// Například smažu-li Categorii smažou se i Názvy kategorie v daných jazicích
       /// V tabulce CategoryNames je CategorieID, LanguageID, Name
       /// Při smazání kategorie nemám žádné  CategorieID tudíž se odmaže také;
       ///  </returns>
       public virtual int Delete(T entity)
       {
           Repository.Delete(entity);
           return UnitOfWork.Commit();
       }

       /// <summary>
       /// Vkládá do databáze Novou Entitu, včetně všech Related Entit
       /// </summary>
       /// <param name="entity">Entita která se má vložit</param>
       /// <returns>Počet vložených entit (Započítávají se i related entity) </returns>
       public virtual int Insert(T entity)
       {
          Repository.Insert(entity);
          return UnitOfWork.Commit();
       }

       /// <summary>
       /// Pokud dělám update entity, která není  připojená.
       /// Tz. Vrátil mi jí GridView, ListView, podíval jsem se do databáze na ID a sám sem si jí vytvořil atd ..
       /// Uloží se pouze primitivní vlastnosti dané entity.
       /// Pokud dělám update entity pro kterou jsem už dělal Update, nebo Insert 
       /// ukládají se nejen primitivní vlastnosti, ale i změny které jsem provedl na připojených entitách.
       /// </summary>
       /// <param name="entity">Entita pro Update</param>
       /// <returns>Počet uložených entit včetně related entit</returns>
       public virtual int Update(T entity)
       {
           if (!UnitOfWork.IsAttach(entity))
           {
               Repository.Update(entity);
           }
           
           return UnitOfWork.Commit();
       }
       
      
   }
}
