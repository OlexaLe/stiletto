using System;
using System.Collections.Generic;
using System.Diagnostics;
using Stiletto;

namespace Example
{
    [Module(
        Injects = new[] { typeof(Testing) })]
    public class BotModule
    {
        [Provides, Singleton]
        public IBot GetBot()
        {
            return new BotImpl();
        }
        
        [Provides, Singleton]
        public ITob<string> GetTob()
        {
            return new TobImpl<string>();
        }
    }

    public interface ITob<T>
    {
        T Get<T>();
    }

    public class TobImpl<T>: ITob<T>
    {
        public T Get<T>()
        {
            Console.WriteLine("Run Tob");
            return default(T);
        }
    }
    
    public interface IBot
    {
        void Run();
    }

    public class BotImpl: IBot
    {
        public void Run()
        {
            Console.WriteLine("Bot run");
        }
    }

    class BaseTesting
    {
        [Inject]
        public IBot Bot { get; set; }
        [Inject]
        public ITob<string> Tob { get; set; }
    }
    
    class Testing: BaseTesting
    {
        
        public void Run()
        {
            Bot.Run();
        }

        public void Get()
        {
            Tob.Get<string>();
        }
    }

    public class CoffeeApp
    {
        public static void Main()
        {
            var cc = Container.Create(typeof(BotModule));
            var baseInjectable = new Testing() as BaseTesting;
            cc.Inject(baseInjectable);
            
            var c = Container.Create(typeof(BotModule));
            
            var a = c.Get<Testing>();
            a.Run();
            a.Get();
            
            Test();
            var container = Container.Create(new DripCoffeeModule());
            for (var i = 0; i < 100000; ++i) {
                container.Get<CoffeeApp>();
                container.Get<CoffeeApp>();
                container.Get<CoffeeApp>();
            }
        }

        static void Test()
        {
            var container = Container.Create(new DripCoffeeModule());
            container.Get<CoffeeApp>();

            var sw = new Stopwatch();

            var hash = 0;
            sw.Start();
            for (var i = 0; i < 10; ++i) {
                container = Container.Create(new DripCoffeeModule());
                hash += container.Get<CoffeeApp>().GetHashCode();
            }
            sw.Stop();

            Console.WriteLine("Hash {0}, {1} iters, {2} ms", hash, 10000, sw.ElapsedMilliseconds);
        }
    }
}
