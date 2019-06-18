using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dynamic.Shared.DynamicContext.Entities;

namespace Dynamic.Shared.DynamicContext.Commands
{
    public static class ResultBuilder
    {
        public static E Build<T, E>(this E result, T entity, string returnFields = "") where E : ICommandResult where T : Entity
        {
            List<string> fields = new List<string>();

            if (returnFields != null && returnFields.Trim() != string.Empty)
                fields = returnFields.Trim().Split(',').ToList();

            else
                foreach (PropertyInfo property in entity.GetType().GetProperties())
                    fields.Add(property.Name);

            foreach (string field in fields)
            {
                PropertyInfo property = getProperty(result.GetType(), field);

                if (property != null && property.SetMethod != null)
                {
                    PropertyInfo entityProperty = entity.GetType().GetProperty(field);
                    
                    if (entityProperty.PropertyType.IsPrimitive)
                        property.SetValue(result, entityProperty.GetValue(entity));
                    
                    else
                        property.SetValue(result, entityProperty.GetValue(entity).ToString());
                }
                    
            }

            return result;
        }

        private static PropertyInfo getProperty(Type type, string field)
        {
            PropertyInfo property = null;

            string[] fields = field.Split('.');

            property = type.GetProperty(fields[0]);

            if (fields.Length > 1 && property != null)
                property = getProperty(property.GetType(), fields[1]);

            return property;
        }
    }
}