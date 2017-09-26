using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ExpectBetter;
using NUnit.Framework;

namespace Stiletto.Test
{
  [TestFixture]
  public class ValidatorTests
  {
    public interface IClass
    {
      int GetInt { get; set; }
    }

    [Test]
    public void NonProvidedInjectableProviderParamIsValid()
    {
      var container = Container.Create(typeof(NonProvidedImplementationModule));
      container.Validate();
    }

    [Test]
    public void Validate_WhenCircularDependenciesExist_Throws()
    {
      Assert.Throws<InvalidOperationException>(() =>
            Container.Create(typeof(CaptainPlanet)).Validate());
    }

    [Test]
    public void Validate_WhenCircularDependenciesExistAcrossModules_Throws()
    {
      var container = Container.Create(typeof(BadModuleOne), typeof(BadModuleTwo));
      Expect.The(container.Validate).ToThrow<InvalidOperationException>();
    }

    [Test]
    public void Validate_WhenDependencyIsUnsatisfied_Throws()
    {
      Assert.Throws<InvalidOperationException>(() =>
            Container.Create(typeof(NeedsSomethingMore)).Validate());
    }

    [Test]
    public void Validate_WhenNoCyclesExist_DoesNotThrow()
    {
      var container = Container.Create(typeof(GoodModuleOne), typeof(GoodModuleTwo));
      container.Validate();
    }

    [Test]
    public void Validate_WhenProviderMethodsAreUnused_Throws()
    {
      Assert.Throws<InvalidOperationException>(() =>
            Container.Create(typeof(UnusedProvidesModule)).Validate());
    }

    [Test]
    public void Validate_WhenProviderParamMissing_Throws()
    {
      Assert.Throws<InvalidOperationException>(() =>
            Container.Create(typeof(MissingProviderParamModule)).Validate());
    }

    [Test]
    public void Validate_WithCircularDependencies_AcrossProvidersAndInjectables_Throws()
    {
      Assert.Throws<InvalidOperationException>(() =>
            Container.Create(typeof(CircularModule)).Validate());
    }

    [Module(IsComplete = false)]
    public class BadModuleOne
    {
      [Provides]
      public string Foo(int i)
      {
        return "";
      }
    }

    [Module(IsComplete = false)]
    public class BadModuleTwo
    {
      [Provides]
      public int Bar(string s)
      {
        return 0;
      }
    }

    [Module(Injects = new[] { typeof(CaptainPlanet) })]
    public class CaptainPlanet
    {
      public CaptainPlanet()
      {
      }

      [Inject]
      public CaptainPlanet(Earth earth, Wind wind, Fire fire, Water water, Heart heart)
      {
      }
    }

    [Module]
    public class CircularModule
    {
      [Provides]
      public IClass One(SomeClass impl)
      {
        return impl;
      }

      [Provides]
      public int Two(IClass provider)
      {
        return provider.GetInt;
      }
    }

    public class Earth
    {
      [Inject]
      public Wind Wind { get; set; }
    }

    public class Fire
    {
      [Inject]
      public Water Water { get; set; }
    }

    [Module(IsComplete = false, IsLibrary = true)]
    public class GoodModuleOne
    {
      [Provides]
      public string Foo(int i)
      {
        return "";
      }
    }

    [Module(IsLibrary = true)]
    public class GoodModuleTwo
    {
      [Provides]
      public int Bar()
      {
        return 0;
      }
    }

    public class Heart
    {
      [Inject]
      public Earth Earth { get; set; }
    }

    [Module(IsLibrary = true)]
    public class MissingProviderParamModule
    {
      [Provides]
      public int Bar(string foo)
      {
        return foo.GetHashCode();
      }

      [Provides]
      public string GetString(object bar)
      {
        return bar.ToString();
      }
    }

    [Module(Injects = new[] { typeof(Dep) })]
    public class NeedsSomethingMore
    {
      [Provides]
      public string GetString()
      {
        return "foo";
      }

      public class Dep
      {
        [Inject]
        public object Bar { get; set; }

        [Inject]
        public string Str { get; set; }
      }
    }

    [Module(IsLibrary = true)]
    public class NonProvidedImplementationModule
    {
      [Provides]
      public IClass ProvideClass(SomeOtherClass impl)
      {
        return impl;
      }

      [Provides]
      public string ProvideString()
      {
        return "foo";
      }
    }

    public class SomeClass : IClass
    {
      [Inject]
      public SomeClass(int num)
      {
        GetInt = num;
      }

      public int GetInt { get; set; }
    }

    public class SomeOtherClass : IClass
    {
      [Inject]
      public SomeOtherClass(string str)
      {
      }

      public int GetInt { get; set; }
    }

    public class UnusedProvidesInjectable
    {
      [Inject]
      public string Foo { get; set; }
    }

    [Module(Injects = new[] { typeof(UnusedProvidesInjectable) })]
    public class UnusedProvidesModule
    {
      [Provides]
      public int ProvideInt()
      {
        return -1;
      }

      [Provides]
      public string ProvideString()
      {
        return "foo";
      }
    }

    public class Water
    {
      [Inject]
      public Heart Heart { get; set; }
    }

    public class Wind
    {
      [Inject]
      public Fire Fire { get; set; }
    }
  }
}