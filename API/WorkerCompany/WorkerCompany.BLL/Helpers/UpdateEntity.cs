using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections;
using System.Linq;

namespace WorkerCompany.BLL.Helpers
{
    public class UpdateEntity
    {
        public static T Update<T>(IModel model, T old, T @new) where T : class
        {
            // get the original type
            var type = UnProxy(model, @new.GetType());

            // get only needed properties
            var filteredProperties = type.GetProperties()
                                         .Where(x => x.DeclaringType.Namespace.Equals("WorkerCompany.DAL.Models"));
            var oldValue = default(object);
            var newValue = default(object);
            var propName = default(string);

            foreach (var property in filteredProperties)
            {
                propName = property.Name;
                oldValue = type.GetProperty(propName)
                               .GetValue(old);

                newValue = type.GetProperty(propName)
                               .GetValue(@new);

                if (newValue != null)
                {
                    if (!(oldValue is ICollection))
                    {
                        property.SetValue(old, newValue);
                    }
                }

            }
            return old;
        }

        private static Type UnProxy(IModel model, Type type)
        {
            // get all entity types from db context
            var entityTypes = model.GetEntityTypes();
            Type originalType = null;
            foreach (var entityType in entityTypes)
            {
                originalType = entityType.ClrType;
                if (originalType.Name.Equals(type.Name))
                {
                    break;
                }
            }
            return originalType;
        }
    }

}
