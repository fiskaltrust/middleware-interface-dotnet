using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using fiskaltrust.ifPOS;
using System.CodeDom;

namespace fiskaltrust.Middleware.Interface.Client.Helpers
{
    public static class ModelConverter<T, U> where U : new()
    {
        public static U Convert(T from)
        {
            var to = new U();

            var fromType = from.GetType();

            fromType.GetProperties().All((System.Reflection.PropertyInfo fromProperty) => {
                try
                {
                    var toProperty = to.GetType().GetProperty(fromProperty.Name);

                    var fromPropertyType = fromProperty.PropertyType;
                    var toPropertyType = toProperty.PropertyType;

                    if (fromPropertyType == toPropertyType)
                    {
                        var fromPropertyValue = fromProperty.GetValue(from, null);
                        toProperty.SetValue(to, fromPropertyValue);
                    }
                    else
                    {
                        if(fromPropertyType.IsArray && toPropertyType.IsArray)
                        {
                            var fromPropertyArrayType = fromPropertyType.GetElementType();
                            var toPropertyArrayType = toPropertyType.GetElementType();

                            var fromPropertyValue = fromProperty.GetValue(from, null);
                            if(fromPropertyValue != null)
                            {

                                var length = (int)fromPropertyType.GetProperty("Length").GetValue(fromPropertyValue, null);
                                var toArray = Array.CreateInstance(toPropertyArrayType, length);

                                for (int i = 0; i < length;i++)
                                {
                                    var fromItem = fromPropertyType.GetMethod("GetValue", new [] { typeof(int) }).Invoke(fromPropertyValue, new object[] { i });

                                    var toItem = typeof(ModelConverter<,>).MakeGenericType(fromPropertyArrayType, toPropertyArrayType).GetMethod("Convert").Invoke(null, new object[] { fromItem });
                                    toArray.SetValue(toItem, i);
                                }
                                toProperty.SetValue(to, toArray);
                            }
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            });

            return to;
        }
    }
}
