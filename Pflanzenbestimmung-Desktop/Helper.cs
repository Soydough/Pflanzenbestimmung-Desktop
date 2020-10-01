using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Pflanzenbestimmung_Desktop
{
    public static class Helper
    {
        /// <summary>
        /// Bekommt den String, dessen UTF8-Code ein Byte-Array ist.
        /// </summary>
        /// <param name="arr">Das Byte-Array</param>
        /// <returns>Ein String</returns>
        public static string GetString(this byte[] arr)
        {
            return Encoding.UTF8.GetString(arr);
        }

        /// <summary>
        /// Gibt zurück, ob ein Array null oder leer ist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this T[] self)
        {
            if (self is null)
                return true;
            if (self.Length == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Konvertiert einen String zu einem Byte-Array
        /// </summary>
        /// <param name="str">Der String</param>
        /// <returns>Ein Byte-Array</returns>
        public static byte[] ToBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
    }


    public static class Stall
    {
        static Random random = new Random();
        static int[] arr;

        /// <summary>
        /// Benutzt BubbleSort, um Zeit zu verzögern
        /// </summary>
        /// <param name="amount">Anzahl der Elemente, die sortiert werden sollen</param>
        public static void Bubblesort(int amount)
        {
            Execute(BubblesortTask, amount);
        }

        static void Initialize(int amount)
        {
            arr = new int[amount];
            for (int i = 0; i < amount; i++)
            {
                arr[i] = random.Next(amount + 1);
            }
        }

        /// <summary>
        /// Benutzt einen optimierten BubbleSort, um Zeit zu verzögern (schneller als normaler Bubble Sort)
        /// </summary>
        /// <param name="amount">Anzahl der Elemente, die sortiert werden sollen</param>
        public static void OptimizedBubbleSort(int amount)
        {
            Execute(OptimizedBubbleSortTask, amount);
        }

        static void BubblesortTask(int amount)
        {
            Initialize(amount);

            bool hasSwitched;
            do
            {
                hasSwitched = false;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;

                        hasSwitched = true;
                    }
                }
            }
            while (hasSwitched);
        }

        public static void OptimizedBubbleSortTask(int amount)
        {
            Initialize(amount);

            bool hasSwitched;
            int len = arr.Length;
            do
            {
                hasSwitched = false;
                for (int i = 0; i < len - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;

                        hasSwitched = true;
                    }
                }
                len--;
            }
            while (hasSwitched);
        }

        static object Execute(Action<int> func, int amount)
        {
            var frame = new DispatcherFrame();
            new Thread(() =>
            {
                func.Invoke(amount);
                frame.Continue = false;
            }).Start();
            Dispatcher.PushFrame(frame);

            return Dispatcher.Yield();
        }
    }
}
