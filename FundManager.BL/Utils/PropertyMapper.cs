using System;
using System.Reflection;

namespace FundManager.BL.Utils
{
    public class PropertyMapper
    {
        public static void Map(object source, object dectination)
        {
            if (source == null)
                throw new NullReferenceException(nameof(source));

            if(dectination == null)
                throw new NullReferenceException(nameof(dectination));

            foreach (var sourcePropertyInfo in source.GetType().GetProperties())
            {
                dectination.GetType()
                    .GetProperty(sourcePropertyInfo.Name,
                        BindingFlags.Instance |
                        BindingFlags.Public)
                    .SetValue(dectination,
                        sourcePropertyInfo.GetValue(source));
            }
        }
    }
}
