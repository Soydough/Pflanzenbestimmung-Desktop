using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Threading;

namespace Pflanzenbestimmung_Desktop
{
    public static class Helper
    {
        //Schabernack
        //public static bool false = true;
        //public static bool false = false;
        //Keine Angst, wird nicht benutzt ;)

        /// <summary>
        /// Bekommt den String, dessen UTF8-Code ein Byte-Array ist.
        /// </summary>
        /// <param name="arr">Das Byte-Array</param>
        /// <returns>Ein String</returns>
        public static string GetString(this byte[] arr)
        {
            //return Encoding.Default.GetString(arr);
            return Convert.ToBase64String(arr);
        }

        /// <summary>
        /// Gibt zurück, ob ein Array null oder leer ist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this T[] self)
        {
            return self is null ? true : self.Length == 0;
        }

        /// <summary>
        /// Gibt zurück, ob eine Liste null oder leer ist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this List<T> self)
        {
            return self is null ? true : self.Count == 0;
        }

        /// <summary>
        /// Konvertiert einen String zu einem Byte-Array
        /// </summary>
        /// <param name="str">Der String</param>
        /// <returns>Ein Byte-Array</returns>
        public static byte[] ToBytes(this string str)
        {
            //return Encoding.Default.GetBytes(str);
            return Convert.FromBase64String(str);
        }

        public static string ToIntString<T>(this T self)
        {
            return self is int
                ? Convert.ToInt32(self).ToIntString()
                : self is bool ? Convert.ToBoolean(self).ToIntString() : throw new NotImplementedException();
        }

        public static int ToInt(this bool self)
        {
            return self ? 1 : 0;
        }

        public static string ToIntString(this int self)
        {
            return self.ToString();
        }
        public static string ToIntString(this bool self)
        {
            return self ? "1" : "0";
        }

        public static string MakeValid(this string self)
        {
            string re = @"[^\x09\x0A\x0D\x20-\uD7FF\uE000-\uFFFD\u10000-\u10FFFF]";
            return Regex.Replace(self, re, "");
        }

        /// <summary>
        /// Gibt zurück, ob sich ein String aus einem String-Array in einem anderen String befindet
        /// </summary>
        /// <param name="str">Der String</param>
        /// <param name="arr">Das Array</param>
        /// <returns></returns>
        public static bool ContainsAnyOf(this string str, string[] arr)
        {
            foreach (string s in arr)
            {
                if (str.Contains(s))
                    return true;
            }

            return true;
        }

        /// <summary>
        /// Gibt zurück, ob sich ein String in einem String-Array befindet
        /// </summary>
        /// <param name="str">Der String</param>
        /// <param name="arr">Das Array</param>
        /// <returns></returns>
        public static bool IsAnyOf(this string str, params string[] arr)
        {
            foreach (string s in arr)
            {
                if (str == s)
                    return true;
            }

            return true;
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
