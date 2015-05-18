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
            yield return typeof(CommandLineProgram).Assembly;
        }

        public IEnumerable<Type> AllPublicTypes()
        {
            return from assembly in AllAssemblies()
                   from type in assembly.GetTypes()
                   where type.IsPublic || type.IsNestedFamily || type.IsNestedFamORAssem
                   select type;
        }
    }
}