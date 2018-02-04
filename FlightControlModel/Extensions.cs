using System;
using System.Data;
using System.Reflection;

namespace FlightControlModel
{
    public static class Extensions
    {
        public static object GetStringOrDBNull(this string obj)
        {
            return string.IsNullOrEmpty(obj) ? DBNull.Value : (object)obj;
        }

        public static object GetCharOrDBNull(this char? obj)
        {
            return obj == null ? DBNull.Value : (object)obj;
        }

        public static object GetDateTimeOrDBNull(this DateTime? obj)
        {
            return obj == null ? DBNull.Value : (object)obj;
        }

        public static object GetIntOrDBNull(this int? obj)
        {
            return obj == null ? DBNull.Value : (object)obj;
        }

        public static bool ReflectiveEquals(this object first, object second)
        {
            if (first == null && second == null)
            {
                return true;
            }
            if (first == null || second == null)
            {
                return false;
            }

            Type firstType = first.GetType();
            if (second.GetType() != firstType)
            {
                return false;
            }

            foreach (PropertyInfo propertyInfo in firstType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    object firstValue = propertyInfo.GetValue(first, null);
                    object secondValue = propertyInfo.GetValue(second, null);
                    if (!object.Equals(firstValue, secondValue))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
