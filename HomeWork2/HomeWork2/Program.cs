using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp2
{
    internal class Program
    {
        
        // записываем имя файла к имеющемуся пути
        public static string ConvertPath(string path)
        {
            int posishionEnd = path.Length - 1;

            while (posishionEnd >= 0 && path[posishionEnd] != '/' && path[posishionEnd] != '\\')
                --posishionEnd;
            return path.Substring(0, posishionEnd + 1) + "result.txt";
        }

        static void Main(string[] args)
        {
            String line;
            //запрашиваем путь к файлу
            Console.Write("Введите путь к файлу с текстом: ");
            String file = Console.ReadLine();
            Dictionary<string, int> word;
            var obj = typeof(word_count.word_count).GetMethod("process_count", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var result = obj?.Invoke(new word_count.word_count(), new object[]{file});
            if(result!=null && result.GetType() == typeof(Dictionary<string, int>))
            {
                try
                {
                    //Открываем файл для записи

                    StreamWriter sw = new StreamWriter(ConvertPath(file));
                    // сортируем словарь по убыванию повторений
                    var sortdic = from pair in result as Dictionary<string,int>
                                  orderby pair.Value descending
                                  select pair;
                    // записываем в файл
                    foreach (KeyValuePair<string, int> valuePair in sortdic)
                    {
                        sw.WriteLine(valuePair.Key + "  " + valuePair.Value);
                    }

                    //закрываем файл
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("Problem with type");
            }
           
           
        }
    }
}

