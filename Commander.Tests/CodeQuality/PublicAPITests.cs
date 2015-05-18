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
    public class PublicAPITests : CodeQualityTestBase
    {
        [Test]
        [TestCaseSource(nameof(AllPublicTypes))]
        [Conditional("JETBRAINS_ANNOTATIONS")]
        public void PublicType_IsTaggedWithPublicAPIAttribute(Type type)
        {
            bool attributeIsPresent = type.GetCustomAttributes(typeof(PublicAPIAttribute), true).Any();
            Assert.That(attributeIsPresent, "Type " + type.Name + " is not tagged with [PublicAPI]");
        }

        [Test]
        [TestCaseSource(nameof(AllPublicMethods))]
        [Conditional("JETBRAINS_ANNOTATIONS")]
        public void PublicMethod_IsTaggedWithPublicAPIAttribute(MethodInfo method)
        {
            bool attributeIsPresent = method.GetCustomAttributes(typeof(PublicAPIAttribute), true).Any();
            Assume.That(method.DeclaringType != null);
            Assert.That(attributeIsPresent, "Method " + method.Name + " of type " + method.DeclaringType.Name + " is not tagged with [PublicAPI]");
        }

        public IEnumerable<MethodInfo> AllPublicMethodsReturningReferenceTypes()
        {
            return from method in AllPublicMethods()
                   where method.ReturnType != typeof(void)
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
    }
}
