using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Commander.Tests.CodeQuality
{
    [TestFixture]
    public class NullableTypeTests : CodeQualityTestBase
    {
        private bool IsNullable(Type type)
        {
            if (type == typeof(void))
                return false;

            if (type.IsClass)
                return true;

            if (!type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return true;

            return false;
        }

        public IEnumerable<MethodInfo> AllPublicMethodsReturningReferenceTypes()
        {
            return from method in AllPublicMethods()
                   where IsNullable(method.ReturnType)
                   select method;
        }

        [Test]
        [TestCaseSource(nameof(AllPublicMethodsReturningReferenceTypes))]
        [Conditional("JETBRAINS_ANNOTATIONS")]
        public void PublicMethod_ThatReturnsReferenceType_MustBeTaggedWithNotNullOrCanBeNullAttributes(MethodInfo method)
        {
            bool hasNotNullAttribute = method.GetCustomAttributes(typeof(NotNullAttribute), true).Any();
            bool hasCanBeNullAttribute = method.GetCustomAttributes(typeof(CanBeNullAttribute), true).Any();

            Assume.That(method.DeclaringType != null);
            Assert.That(hasNotNullAttribute || hasCanBeNullAttribute, "Method " + method.Name + " of type " + method.DeclaringType.Name + " must be tagged with [NotNull] or [CanBeNull]");
        }

        public IEnumerable<PropertyInfo> AllPublicPropertiesHoldingNullableTypes()
        {
            return from property in AllPublicProperties()
                   where IsNullable(property.PropertyType)
                   select property;
        }

        [Test]
        [TestCaseSource(nameof(AllPublicPropertiesHoldingNullableTypes))]
        [Conditional("JETBRAINS_ANNOTATIONS")]
        public void PublicProperty_ThatHoldsReferenceType_MustBeTaggedWithNotNullOrCanBeNullAttributes(PropertyInfo property)
        {
            bool hasNotNullAttribute = property.GetCustomAttributes(typeof(NotNullAttribute), true).Any();
            bool hasCanBeNullAttribute = property.GetCustomAttributes(typeof(CanBeNullAttribute), true).Any();

            Assume.That(property.DeclaringType != null);
            Assert.That(hasNotNullAttribute || hasCanBeNullAttribute, "Property " + property.Name + " of type " + property.DeclaringType.Name + " must be tagged with [NotNull] or [CanBeNull]");
        }
    }
}
