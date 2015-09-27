using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FreeUniverse.Common
{
    // FieldCopyAttribute is used along with unity scripts to simplify conversion from and to by field/property names
    // type check applied
    public sealed class FieldCopyAttribute : Attribute
    {
        public string name { get; set; }
    }

    // Just a reminder class to show which class is used for field copy as target
    public sealed class FieldCopyTargetAttribute : Attribute
    {
        public FieldCopyTargetAttribute(Type type)
        {

        }
    }

    public static class FieldCopySerializer<S, D>
        where S : class
        where D : class
    {
        public static D CopyValuesByNames(S source, D destination)
        {
            int copied = 0;

            Type typeSource = source.GetType();
            Type typeDest = destination.GetType();

            FieldInfo[] sourceFields = typeSource.GetFields();
            FieldInfo[] destFields = typeDest.GetFields();

            PropertyInfo[] sourceProperties = typeSource.GetProperties();
            PropertyInfo[] destProperties = typeDest.GetProperties();

            foreach (FieldInfo srcField in sourceFields)
            {
                // Check if field has proper attribute
                string copyName = srcField.Name;

                // Try to find a field or property with same name at destination class
                FieldInfo destField = typeDest.GetField(copyName);

                // Field found
                if (destField != null)
                {
                    // Type match check
                    if (!destField.FieldType.Equals(srcField.FieldType))
                        throw new Exception("type mismatch");

                    // copy
                    destField.SetValue(destination, srcField.GetValue(source));
                    copied++;
                }
                else
                {
                    // no field found, try to find property
                    PropertyInfo destProperty = typeDest.GetProperty(copyName);

                    if (destProperty != null)
                    {
                        if (!destProperty.PropertyType.Equals(srcField.FieldType))
                            throw new Exception("type mismatch");

                        destProperty.SetValue(destination, srcField.GetValue(source), null);
                        copied++;
                    }
                }
            }

            // property to field and property to property
            foreach (PropertyInfo srcProperty in sourceProperties)
            {
                string copyName = srcProperty.Name;

                FieldInfo destField = typeDest.GetField(copyName);

                // Field found
                if (destField != null)
                {
                    // Type match check
                    if (!destField.FieldType.Equals(srcProperty.PropertyType))
                        throw new Exception("type mismatch");

                    destField.SetValue(destination, srcProperty.GetValue(source, null));
                    copied++;
                }
                else
                {
                    PropertyInfo destProperty = typeDest.GetProperty(copyName);

                    if (destProperty != null)
                    {
                        if (!destProperty.PropertyType.Equals(srcProperty.PropertyType))
                            throw new Exception("type mismatch");

                        destProperty.SetValue(destination, srcProperty.GetValue(source, null), null);
                        copied++;
                    }
                }
            }

            return destination;
        }
        
        public static int CopyValues(S source, D destination)
        {
            int copied = 0;

            Type typeSource = source.GetType();
            Type typeDest = destination.GetType();

            FieldInfo[] sourceFields = typeSource.GetFields();
            FieldInfo[] destFields = typeDest.GetFields();

            PropertyInfo[] sourceProperties = typeSource.GetProperties();
            PropertyInfo[] destProperties = typeDest.GetProperties();

            // field to field and field to property
            foreach (FieldInfo srcField in sourceFields)
            {
                // Check if field has proper attribute
                string copyName = GetCopyFieldName(srcField);

                // Try to find a field or property with same name at destination class
                FieldInfo destField = FindCopyFieldByName(copyName, destFields);

                // Field found
                if (destField != null)
                {
                    // Type match check
                    if (!destField.FieldType.Equals(srcField.FieldType))
                        throw new Exception("type mismatch");

                    // copy
                    destField.SetValue(destination, srcField.GetValue(source));
                    copied++;
                }
                else
                {
                    // no field found, try to find property
                    PropertyInfo destProperty = FindCopyFieldPropertyByName(copyName, destProperties);

                    if (destProperty != null)
                    {
                        if (!destProperty.PropertyType.Equals(srcField.FieldType))
                            throw new Exception("type mismatch");

                        destProperty.SetValue(destination, srcField.GetValue(source), null);
                        copied++;
                    }
                }
            }

            // property to field and property to property
            foreach (PropertyInfo srcProperty in sourceProperties)
            {
                string copyName = GetCopyFieldName(srcProperty);

                FieldInfo destField = FindCopyFieldByName(copyName, destFields);

                // Field found
                if (destField != null)
                {
                    // Type match check
                    if (!destField.FieldType.Equals(srcProperty.PropertyType))
                        throw new Exception("type mismatch");

                    destField.SetValue(destination, srcProperty.GetValue(source, null));
                    copied++;
                }
                else
                {
                    PropertyInfo destProperty = FindCopyFieldPropertyByName(copyName, destProperties);

                    if (destProperty != null)
                    {
                        if (!destProperty.PropertyType.Equals(srcProperty.PropertyType))
                            throw new Exception("type mismatch");

                        destProperty.SetValue(destination, srcProperty.GetValue(source, null), null);
                        copied++;
                    }
                }
            }

            return copied;
        }

        private static string GetCopyFieldName(FieldInfo info)
        {
            FieldCopyAttribute[] attribs = (FieldCopyAttribute[])info.GetCustomAttributes(typeof(FieldCopyAttribute), true);

            if (attribs.Length == 0)
                return null;

            // Get first
            FieldCopyAttribute attribute = attribs[0];

            return attribute.name;
        }

        private static string GetCopyFieldName(PropertyInfo info)
        {
            FieldCopyAttribute[] attribs = (FieldCopyAttribute[])info.GetCustomAttributes(typeof(FieldCopyAttribute), true);

            if (attribs.Length == 0)
                return null;

            // Get first
            FieldCopyAttribute attribute = attribs[0];

            return attribute.name;
        }

        private static PropertyInfo FindCopyFieldPropertyByName(string name, PropertyInfo[] properties)
        {
            foreach (PropertyInfo info in properties)
            {
                FieldCopyAttribute[] attribs = (FieldCopyAttribute[])info.GetCustomAttributes(typeof(FieldCopyAttribute), true);

                if (attribs.Length == 0)
                    continue;

                // Get first
                FieldCopyAttribute attribute = attribs[0];

                if (attribute.name.Equals(name))
                    return info;
            }

            return null;
        }

        private static FieldInfo FindCopyFieldByName(string name, FieldInfo[] fields)
        {
            foreach (FieldInfo info in fields)
            {
                FieldCopyAttribute[] attribs = (FieldCopyAttribute[])info.GetCustomAttributes(typeof(FieldCopyAttribute), true);

                if (attribs.Length == 0)
                    continue;

                // Get first
                FieldCopyAttribute attribute = attribs[0];

                if (attribute.name.Equals(name))
                    return info;
            }

            return null;
        }
    }
}
