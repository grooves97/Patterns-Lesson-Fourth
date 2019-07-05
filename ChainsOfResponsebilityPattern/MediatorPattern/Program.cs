using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }

    // Конкретные Посредники реализуют совместное поведение, координируя
    // отдельные компоненты.
    class ConcreteMediator : IMediator
    {
        private Component1 _component1;

        private Component2 _component2;

        private Component3 _component3;

        public ConcreteMediator(Component1 component1, Component2 component2,Component3 component3)
        {
            this._component1 = component1;
            this._component1.SetMediator(this);
            this._component2 = component2;
            this._component2.SetMediator(this);
            this._component3 = component3;
            this._component3.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "A")
            {
                Console.WriteLine("Mediator reacts on A and triggers folowing operations: ");
                this._component2.DoC();
            }
            if (ev == "D")
            {
                Console.WriteLine("Mediator reacts on D and triggers following operations: ");
                this._component1.DoB();
                this._component2.DoC();
            }
            if (ev == "B")
            {
                Console.WriteLine("Mediator reacts on B and triggers following operations: ");
                this._component3.DoA();
            }
        }
    }

    // Базовый Компонент обеспечивает базовую функциональность хранения
    // экземпляра посредника внутри объектов компонентов.
    class BaseComponent
    {
        protected IMediator _mediator;

        public BaseComponent(IMediator mediator = null)
        {
            this._mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }

    // Конкретные Компоненты реализуют различную функциональность. Они не
    // зависят от других компонентов. Они также не зависят от каких-либо
    // конкретных классов посредников.
    class Component1 : BaseComponent
    {
        public void DoA()
        {
            Console.WriteLine("Component 1 does A.");

            this._mediator.Notify(this, "A");
        }

        public void DoB()
        {
            Console.WriteLine("Component 1 does B.");

            this._mediator.Notify(this, "B");
        }
    }

    class Component2 : BaseComponent
    {
        public void DoC()
        {
            Console.WriteLine("Component 2 does C.");

            this._mediator.Notify(this, "C");
        }

        public void DoD()
        {
            Console.WriteLine("Component 2 does D.");

            this._mediator.Notify(this, "D");
        }
    }

    class Component3 : BaseComponent
    {
        public void DoA()
        {
            Console.WriteLine("Component 3 does A.");

            this._mediator.Notify(this, "A");
        }

        public void DoB()
        {
            Console.WriteLine("Component 3 does B.");

            this._mediator.Notify(this, "B");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Component1 component1 = new Component1();
            Component2 component2 = new Component2();
            Component3 component3 = new Component3();
            new ConcreteMediator(component1, component2, component3);

            Console.WriteLine("Client triggets operation A.");
            component1.DoA();

            Console.WriteLine();

            Console.WriteLine("Client triggers operation D.");
            component2.DoD();

            Console.WriteLine();

            Console.WriteLine("Client triggers opeartion B");
            component3.DoB();

            Console.ReadLine();
        }
    }
}
