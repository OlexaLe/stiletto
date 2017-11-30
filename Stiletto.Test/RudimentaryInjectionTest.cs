using System;
using System.Collections.Generic;
using ExpectBetter;
using NUnit.Framework;

namespace Stiletto.Test
{
  [TestFixture]
  public class RudimentaryInjectionTest
  {
    [Test]
    public void BaseClassGetsInjectedToo()
    {
      var derived = GetWithModules<DerivedInjectable>(new NameModule());
      Expect.The(derived.TheDude).Not.ToBeNull();
      Expect.The(derived.Name).ToEqual("Joe");
    }

    [Test]
    public void BaseClassInstance_InjectingDerivedProperties_FailsWhenGenericIsUsed()
    {
      //MARK: now this functionality throws ArgumentException BEFORE the InvalidOperationException. 
      //It's difficult to say can it cause anyissues in future
      Assert.Throws<ArgumentException>(() => 
      {
        var container = Container.Create(typeof(NameModule));
        var baseInjectable = new DerivedInjectable("foo") as BaseInjectable;
        container.Inject(baseInjectable);
      });
    }

    [Test]
    public void BaseClassInstance_InjectingDerivedProperties_WorksWhenNonGenericIsUsed()
    {
      var container = Container.Create(typeof(NameModule));
      var baseInjectable = new DerivedInjectable("foo") as BaseInjectable;
      container.Inject(baseInjectable, baseInjectable.GetType());
    }

    [Test]
    public void CanGetTheDude()
    {
      var container = Container.Create(typeof(TestNamedModule));
      var dude = container.Get<Dude>();
      Expect.The(dude).Not.ToBeNull();

      var birthday = dude.Birthday;
      Expect.The(birthday).ToEqual(new DateTime(1982, 12, 3));

      var listOfHobbies = dude.Hobbies;
      Expect.The(listOfHobbies).ToContain("dependency injection");
    }

    [Test]
    public void CanGetTheDudeFromAnIncludedModule()
    {
      var container = Container.Create(typeof(TestIncludedModules));
      var dude = container.Get<Dude>();
      Expect.The(dude).Not.ToBeNull();
    }

    [Test]
    public void ConstructorExceptionsPropagate()
    {
      Assert.Throws<PlatformNotSupportedException>(() =>
            GetWithModules<ThrowsOnNew>(new ThrowableModule()));
    }

    [Test]
    public void DuplicateModuleTypesFail()
    {
      Assert.Throws<ArgumentException>(() =>
            Container.Create(new NonOverridingModule(), new NonOverridingModule()));
    }

    [Test]
    public void DuplicateProvidedTypesFail()
    {
      Assert.Throws<ArgumentException>(() =>
            Container.Create(new BoolProvidingModule(), new NonOverridingModule()));
    }

    [Test]
    public void Injectable_Injected_WhenDepenencyNotProvided_GetsJitBinding()
    {
      var injectable = GetWithModules<NeedsA>(new EmptyModule());
      Expect.The(injectable).Not.ToBeNull();
    }

    [Test]
    public void InjectableDerivedFromNonInjectable_WithSuper_IsInjected()
    {
      GetWithModules<DerivedWithSuper>(typeof(TypeProvidingModule));
    }

    [Test]
    public void InjectableDerivedFromNonInjectableIsInjected()
    {
      GetWithModules<DerivedFromNonInjectable>(typeof(BaseNonInjectableModule));
    }

    [Test]
    public void ModuleOrderDoesNotMatterForOverriding()
    {
      var c1 = Container.Create(new NonOverridingModule(), new OverridingModule());
      var c2 = Container.Create(new OverridingModule(), new NonOverridingModule());

      var b1 = c1.Get<bool>();
      var b2 = c2.Get<bool>();

      Expect.The(b1).ToEqual(b2);
    }

    [Test]
    public void ModulesCanBeInstances()
    {
      var guy = GetWithModules<Dude>(new TestNamedModule("going outside", "dancing"));
      var listOfHobbies = guy.Hobbies;

      Expect.The(listOfHobbies).ToContain("dancing");
      Expect.The(listOfHobbies).Not.ToContain("dependency injection");
    }

    [Test]
    public void ModulesCanOverride()
    {
      var container = Container.Create(new OverridingModule(), new NonOverridingModule());
      Expect.The(container.Get<bool>()).ToBeTrue();
    }

    [Test]
    public void NonSingletonProviderMethodReturnsDifferentInstances()
    {
      var injectable = GetWithModules<NonSingletonTestInjectable>(typeof(TestSingletonProviderModule));

      Expect.The(injectable.One).Not.ToBeTheSameAs(injectable.Another);
    }

    [Test]
    public void PropertySetterExceptionsPropagate()
    {
      Assert.Throws<PlatformNotSupportedException>(() =>
            GetWithModules<ThrowsOnSet>(new ThrowableModule()));
    }

    [Test]
    public void SingletonProviderMethodReturnsSameInstance()
    {
      var container = Container.Create(typeof(TestSingletonProviderModule));
      var injectable = container.Get<SingletonTestInjectable>();

      Expect.The(injectable.One).ToBeTheSameAs(injectable.Another);
    }

    [Test]
    public void ThereCanBeOnlyOne()
    {
      var container = Container.Create(typeof(TestNamedModule));
      var dude = container.Get<Dude>();
      var otherDude = container.Get<Dude>();
      Expect.The(otherDude).ToBeTheSameAs(dude);
    }

    private T GetWithModules<T>(params object[] modules)
    {
      return Container.Create(modules).Get<T>();
    }

    public class BaseInjectable
    {
      [Inject]
      public Dude TheDude { get; set; }
    }

    public class BaseNonInjectable
    {
    }

    [Module(Injects = new[] { typeof(DerivedFromNonInjectable) })]
    public class BaseNonInjectableModule
    {
      [Provides]
      public string ProvideFoo()
      {
        return "foo";
      }
    }

    public class BaseNonInjectibleWithSuperRequired
    {
      public BaseNonInjectibleWithSuperRequired(Type t)
      {
        Type = t;
      }

      public Type Type { get; private set; }
    }

    [Module(IsLibrary = true)]
    public class BoolProvidingModule
    {
      [Provides]
      public bool HereIsABool()
      {
        return false;
      }
    }

    public class DerivedFromNonInjectable : BaseNonInjectable
    {
      private readonly string foo;

      [Inject]
      public DerivedFromNonInjectable(string foo)
      {
        this.foo = foo;
      }

      public string Foo
      {
        get { return foo; }
      }
    }

    public class DerivedInjectable : BaseInjectable
    {
      private readonly string name;

      [Inject]
      public DerivedInjectable(string name)
      {
        this.name = name;
      }

      public string Name
      {
        get { return name; }
      }
    }

    public class DerivedWithSuper : BaseNonInjectibleWithSuperRequired
    {
      [Inject]
      public DerivedWithSuper(Type t)
          : base(t)
      { }
    }

    [Singleton]
    public class Dude
    {
      private readonly IList<string> hobbies;

      [Inject]
      public Dude(IList<string> hobbies)
      {
        this.hobbies = hobbies;
      }

      [Inject, Named("bar")]
      public DateTime Birthday { get; set; }

      [Inject]
      public DateTime FavoriteTimeOfDay { get; set; }

      public IList<string> Hobbies
      {
        get { return hobbies; }
      }
    }

    [Module(Injects = new[] { typeof(DerivedInjectable) },
            IncludedModules = new[] { typeof(TestNamedModule) })]
    public class NameModule
    {
      [Provides]
      public string GetName()
      {
        return "Joe";
      }
    }

    [Module(Injects = new[] { typeof(bool) }, IsLibrary = true)]
    public class NonOverridingModule
    {
      [Provides]
      public bool ProvideBool()
      {
        return false;
      }
    }

    public class NonSingletonTestInjectable
    {
      [Inject, Named("n")]
      public object Another { get; set; }

      [Inject, Named("n")]
      public object One { get; set; }
    }

    [Module(IsOverride = true, IsLibrary = true)]
    public class OverridingModule
    {
      [Provides]
      public bool ProvideAnotherBool()
      {
        return true;
      }
    }

    public class SingletonTestInjectable
    {
      [Inject, Named("s")]
      public object Another { get; set; }

      [Inject, Named("s")]
      public object One { get; set; }
    }

    [Module(IncludedModules = new[] { typeof(TestNamedModule) })]
    public class TestIncludedModules
    {
      // This space intentionally left blank
    }

    [Module(
            Injects = new[] { typeof(Dude) })]
    public class TestNamedModule
    {
      private readonly IList<string> hobbies;

      public TestNamedModule()
          : this("dependency injection")
      {
      }

      public TestNamedModule(params string[] hobbies)
      {
        this.hobbies = hobbies;
      }

      [Provides]
      public IList<string> Activities()
      {
        return new List<string>(hobbies);
      }

      [Provides, Named("bar")]
      public DateTime GetBar()
      {
        return new DateTime(1982, 12, 3);
      }

      [Provides]
      public DateTime GetSomeOtherDate()
      {
        return DateTime.Now;
      }
    }

    [Module(
            Injects = new[] { typeof(SingletonTestInjectable), typeof(NonSingletonTestInjectable) })]
    public class TestSingletonProviderModule
    {
      [Provides, Named("n")]
      public object NewEveryTime()
      {
        return new object();
      }

      [Provides, Named("s"), Singleton]
      public object Singleton()
      {
        return new object();
      }
    }

    [Module(Injects = new[] { typeof(ThrowsOnNew), typeof(ThrowsOnSet) })]
    public class ThrowableModule
    {
      [Provides]
      public int GetInt()
      {
        return 0;
      }
    }

    public class ThrowsOnNew
    {
      [Inject]
      public ThrowsOnNew(int arg)
      {
        throw new PlatformNotSupportedException();
      }
    }

    public class ThrowsOnSet
    {
      private int n;

      [Inject]
      public int Dependency
      {
        get { return n; }
        set { n = value; throw new PlatformNotSupportedException(); }
      }
    }

    [Module(Injects = new[] { typeof(DerivedWithSuper) })]
    public class TypeProvidingModule
    {
      [Provides]
      public Type HaveAType()
      {
        return typeof(TypeProvidingModule);
      }
    }

    private class A
    {
      [Inject]
      public A() { }
    }

    [Module(Injects = new[] { typeof(NeedsA) })]
    private class EmptyModule
    {
    }

    private class NeedsA
    {
      [Inject]
      public A A { get; set; }
    }
  }
}