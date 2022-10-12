using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeWork2Extra
{
    abstract class People
    {
        public string firstname;
        public string secondtname;
        public int age;
        public string gender;

        public People()
        {
            firstname = "Иванов";
            secondtname = "Иван";
            age = 33;
            gender = "мужчина";
        }
        public abstract void Abstract();
    }
    class Program
    {
        static void Main(string[] args)
        {
            //получаем приватный метод Allocate
            var allocate_method = typeof(RuntimeTypeHandle).GetMethod("Allocate", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            //получение объект абстрактного класса
            var obj = (People)allocate_method.Invoke(null, new object[] { typeof(People) });
            //применение конструктора к абстрактному классу
            typeof(People).GetConstructor(Type.EmptyTypes).Invoke(obj, new object[0]);
            //вывод
            Console.WriteLine(obj.firstname + " " + obj.secondtname + ", " + obj.age + " года, " + obj.gender);
        }
    }
}
