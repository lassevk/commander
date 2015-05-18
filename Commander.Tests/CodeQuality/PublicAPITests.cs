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

        [Test]
        [TestCaseSource(nameof(AllPublicConstructors))]
        [Conditional("JETBRAINS_ANNOTATIONS")]
        public void PublicConstructor_IsTaggedWithPublicAPIAttribute(ConstructorInfo constructor)
        {
            bool attributeIsPresent = constructor.GetCustomAttributes(typeof(PublicAPIAttribute), true).Any();
            Assume.That(constructor.DeclaringType != null);
            Assert.That(attributeIsPresent, "Constructor (" + string.Join(", ", constructor.GetParameters().Select(p => p.ParameterType.Name)) + " of type " + constructor.DeclaringType.Name + " is not tagged with [PublicAPI]");
        }

        [Test]
        [TestCaseSource(nameof(AllPublicProperties))]
        [Conditional("JETBRAINS_ANNOTATIONS")]
        public void PublicProperty_IsTaggedWithPublicAPIAttribute(PropertyInfo property)
        {
            bool attributeIsPresent = property.GetCustomAttributes(typeof(PublicAPIAttribute), true).Any();
            Assume.That(property.DeclaringType != null);
            Assert.That(attributeIsPresent, "Property " + property.Name + " of type " + property.DeclaringType.Name + " is not tagged with [PublicAPI]");
        }
    }
}
