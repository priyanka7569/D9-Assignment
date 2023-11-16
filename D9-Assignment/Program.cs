using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace D9_Assignment
{
    public class LargeDataCollection : IDisposable
    {
        private List<Object> list;
        private bool disposedvalue = false;
        public LargeDataCollection(params Object[] a)
        {
            list = new List<Object>(a);

            Console.WriteLine("List after adding elements " + list.Count);
            Console.WriteLine("Elements in the list");
            foreach (Object o in list)
            {
                Console.WriteLine(o);
            }
        }
        public void AddElement(Object e)
        {
            if (!disposedvalue)
            {
                list.Add(e);
            }
            else
            {
                Console.WriteLine("Cannot add element after Disposal");
            }
        }

        public void RemoveElement(Object e)
        {
            if (!disposedvalue)
            {
                list.Remove(e);
            }
            else
            {
                Console.WriteLine("Cannot add element after Disposal");
            }
        }

        public object AccessElement(int i)
        {
            if (!disposedvalue && i >= 0 && i < list.Count)
            {
                return list[i];
            }
            else
            {
                Console.WriteLine("Invalid Index or element does not exist");
                return null;
            }
        }

        public void DisplayElement()
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedvalue)
            {
                if (disposing)
                {
                    list.Clear();
                    list = null;
                }

                Console.WriteLine("Large data collection has been disposed");
                disposedvalue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LargeDataCollection()
        {
            Dispose(false);
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter element 1:");
            int a1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter element 2:");
            float b = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter element 3:");
            char c = char.Parse(Console.ReadLine());
            Console.WriteLine("Enter element 4:");
            string d = Console.ReadLine();
            var a = new object[] { a1, b, c, d, new object() };
            using (var LargeCollection = new LargeDataCollection(a))
            {
                LargeCollection.AddElement(44);
                Console.WriteLine("List after adding an element");
                LargeCollection.DisplayElement();
                LargeCollection.RemoveElement(a1);
                Console.WriteLine("List after removing an element");
                LargeCollection.DisplayElement();
                Console.WriteLine("Element at Index 0: " + LargeCollection.AccessElement(0));
                Console.WriteLine("Element at Index 2: " + LargeCollection.AccessElement(2));
            }
            Console.ReadKey();
        }
    }
}