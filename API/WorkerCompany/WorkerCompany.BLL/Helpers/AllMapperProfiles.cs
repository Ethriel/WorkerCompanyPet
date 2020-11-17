using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace WorkerCompany.BLL.Helpers
{
    public static class AllMapperProfiles
    {
        static readonly Type[] profiles;
        static AllMapperProfiles()
        {
            var parent = typeof(Profile);
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            profiles = types.Where(x => x.IsSubclassOf(parent))
                            .ToArray();
        }
        public static Type[] Profiles => profiles;
    }
}
