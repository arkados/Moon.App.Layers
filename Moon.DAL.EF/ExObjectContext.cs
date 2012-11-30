using System;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;

namespace Moon.DAL.EF
{
   public static class ExObjectContext
   {
      public static string GetEntitySetName(this ObjectContext objectContext, Type entityType )
      {
          var entityTypeName = entityType.Name;

          var container = objectContext.MetadataWorkspace.GetEntityContainer(
              objectContext.DefaultContainerName, DataSpace.CSpace);
          return (from meta in container.BaseEntitySets
                                  where meta.ElementType.Name == entityTypeName
                                  select meta.Name).First();
      }


   }
}
