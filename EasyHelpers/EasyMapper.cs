using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class EasyMapper
    {
        public static TResult MapTo<TResult>(this object obj)
        {
            var r = Activator.CreateInstance<TResult>();
            if (obj == null) return r;
            var type = obj.GetType();
            var typeR = typeof(TResult);
            var props = type.GetProperties();
            var propsR = typeR.GetProperties();
            foreach (var prop in props)
            {
                if (propsR.Any(x => x.Name == prop.Name))
                {
                    var propR = typeR.GetProperty(prop.Name);
                    propR.SetValue(r, prop.GetValue(obj));
                }
            }
            return r;
        }
    }
}
