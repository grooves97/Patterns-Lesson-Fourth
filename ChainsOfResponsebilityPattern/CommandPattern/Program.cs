﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern
{
    public interface ICommand
    {
        void Execute();
    }

    // Некоторые команды способны выполнять простые операции самостоятельно.
    class WaiterOne : ICommand
    {
        private string _payload = string.Empty;

        public WaiterOne(string payload)
        {
            this._payload = payload;
        }

        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: See, I can do simple things like printing ({this._payload})");
        }
    }

    // Но есть и команды, которые делегируют более сложные операции другим
    // объектам, называемым «получателями».
    class WaiterTwo : ICommand
    {
        private Receiver _receiver;

        // Данные о контексте, необходимые для запуска методов получателя.
        private string _a;

        private string _b;

        // Сложные команды могут принимать один или несколько объектов-
        // получателей вместе с любыми данными о контексте через конструктор.
        public WaiterTwo(Receiver receiver, string a, string b)
        {
            this._receiver = receiver;
            this._a = a;
            this._b = b;
        }

        // Команды могут делегировать выполнение любым методам получателя.
        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver object.");
            this._receiver.DoSomething(this._a);
            this._receiver.DoSomethingElse(this._b);
        }
    }

    class WaiterThree : ICommand
    {
        private Receiver _receiver;

        // Данные о контексте, необходимые для запуска методов получателя.
        private string _a;

        private string _b;

        // Сложные команды могут принимать один или несколько объектов-
        // получателей вместе с любыми данными о контексте через конструктор.
        public WaiterThree(Receiver receiver, string a, string b)
        {
            this._receiver = receiver;
            this._a = a;
            this._b = b;
        }

        // Команды могут делегировать выполнение любым методам получателя.
        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver object.");
            this._receiver.DoSomething(this._a);
            this._receiver.DoSomethingElse(this._b);
        }
    }

    // Классы Получателей содержат некую важную бизнес-логику. Они умеют
    // выполнять все виды операций, связанных с выполнением запроса. Фактически,
    // любой класс может выступать Получателем.
    class Receiver
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Receiver: Working on ({a}.)");
        }

        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Receiver: Also working on ({b}.)");
        }
    }

    // Отправитель связан с одной или несколькими командами. Он отправляет
    // запрос команде.
    class InvokerOne
    {
        private ICommand _onStart;
        private ICommand _onMiddle;
        private ICommand _onFinish;

        // Инициализация команд
        public void SetOnStart(ICommand command)
        {
            this._onStart = command;
        }

        public void SetOnMiddle(ICommand command)
        {
            this._onMiddle = command;
        }

        public void SetOnFinish(ICommand command)
        {
            this._onFinish = command;
        }

        // Отправитель не зависит от классов конкретных команд и получателей.
        // Отправитель передаёт запрос получателю косвенно, выполняя команду.
        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: Does anybody want something done before I begin?");
            if (this._onStart is ICommand)
            {
                this._onStart.Execute();
            }

            Console.WriteLine("Invoker: Does anybody want somthing done before I start middle?");
            if (this._onMiddle is ICommand)
            {
                this._onMiddle.Execute();
            }

            Console.WriteLine("Invoker: ...doing something really important...");

            Console.WriteLine("Invoker: Does anybody want something done after I finish?");
            if (this._onFinish is ICommand)
            {
                this._onFinish.Execute();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {// Клиентский код может параметризовать отправителя любыми
         // командами.
            InvokerOne invoker = new InvokerOne();
            invoker.SetOnStart(new WaiterOne("Say Hi!"));
            Receiver driver = new Receiver();
            invoker.SetOnMiddle(new WaiterThree(driver, "Deliver food", "To wash the dishes"));
            Receiver waiter = new Receiver();
            invoker.SetOnFinish(new WaiterTwo(waiter, "Send email", "Save report"));
            

            invoker.DoSomethingImportant();

            Console.ReadLine();
        }
    }
}
