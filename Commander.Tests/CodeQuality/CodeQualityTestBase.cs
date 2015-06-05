using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Commander.Tests.CodeQuality
{
    public class CodeQualityTestBase
    {
        public IEnumerable<Assembly> AllAssemblies()
        {
            yield return typeof(ProcessEx).Assembly;
        }

        public IEnumerable<Type> AllPublicTypes()
        {
            return from assembly in AllAssemblies()
                   from type in assembly.GetTypes()
                   where type.IsPublic || type.IsNestedFamily || type.IsNestedFamORAssem
                   select type;
        }

        public IEnumerable<MethodInfo> AllPublicMethods()
        {
            return from type in AllPublicTypes()
                   from method in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                   where !method.IsSpecialName
                   select method;
        }

        public IEnumerable<ConstructorInfo> AllPublicConstructors()
        {
            return from type in AllPublicTypes()
                   from constructor in type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.Instance)
                   select constructor;
        }

        public IEnumerable<PropertyInfo> AllPublicProperties()
        {
            return from type in AllPublicTypes()
                   from property in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                   where !property.IsSpecialName
                   select property;
        }
    }
}