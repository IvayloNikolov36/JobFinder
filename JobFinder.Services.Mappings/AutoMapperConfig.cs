﻿using AutoMapper;
using System.Reflection;

namespace JobFinder.Services.Mappings
{
    public static class AutoMapperConfig
    {
        private static bool initialized;

        public static IMapper MapperInstance { get; set; }

        public static void RegisterMappings(params Assembly[] assemblies)
        {
            if (initialized)
            {
                return;
            }

            initialized = true;

            List<Type> types = assemblies
                .SelectMany(a => a.GetExportedTypes())
                .ToList();

            MapperConfigurationExpression config = new();

            config.CreateProfile(
                "ReflectionProfile",
                configuration =>
                {
                    // IMapFrom<>
                    foreach (TypesMap map in GetFromMaps(types))
                    {
                        configuration.CreateMap(map.Source, map.Destination);
                    }

                    // IMapTo<>
                    foreach (TypesMap map in GetToMaps(types))
                    {
                        configuration.CreateMap(map.Source, map.Destination);
                    }

                    // IHaveCustomMappings
                    foreach (IHaveCustomMappings map in GetCustomMappings(types))
                    {
                        map.CreateMappings(configuration);
                    }
                });

            MapperInstance = new Mapper(new MapperConfiguration(config));
        }

        private static IEnumerable<TypesMap> GetFromMaps(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> fromMaps = from t in types
                                             from i in t.GetTypeInfo().GetInterfaces()
                                             where i.GetTypeInfo().IsGenericType &&
                                                   i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                                                   !t.GetTypeInfo().IsAbstract &&
                                                   !t.GetTypeInfo().IsInterface
                                             select new TypesMap
                                             {
                                                 Source = i.GetTypeInfo().GetGenericArguments()[0],
                                                 Destination = t,
                                             };

            return fromMaps;
        }

        private static IEnumerable<TypesMap> GetToMaps(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> toMaps = from t in types
                                           from i in t.GetTypeInfo().GetInterfaces()
                                           where i.GetTypeInfo().IsGenericType &&
                                                 i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                                                 !t.GetTypeInfo().IsAbstract &&
                                                 !t.GetTypeInfo().IsInterface
                                           select new TypesMap
                                           {
                                               Source = t,
                                               Destination = i.GetTypeInfo().GetGenericArguments()[0],
                                           };

            return toMaps;
        }

        private static IEnumerable<IHaveCustomMappings> GetCustomMappings(IEnumerable<Type> types)
        {
            IEnumerable<IHaveCustomMappings> customMaps = from t in types
                                                          from i in t.GetTypeInfo().GetInterfaces()
                                                          where typeof(IHaveCustomMappings).GetTypeInfo().IsAssignableFrom(t) &&
                                                                !t.GetTypeInfo().IsAbstract &&
                                                                !t.GetTypeInfo().IsInterface
                                                          select (IHaveCustomMappings)Activator.CreateInstance(t);

            return customMaps;
        }
    }
}
