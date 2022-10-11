using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
namespace word_count
{
    public  class word_count
    {
        // функция для удаления дефиса из конца слова
        // а так же не считывания --
        private static string parsDef(string test)
        {
            int posishionEnd = test.Length - 1;
            while (posishionEnd >= 0 && test[posishionEnd] == '-')
                --posishionEnd;
            if (posishionEnd != test.Length - 1)
                test = test.Substring(0, posishionEnd + 1);
            return test;
        }
        
        private Dictionary<string, int> process_count( String file)
        {
            String line;
            // объявляем словарь, в котором ключ - это слово, значение - это кол-во повторений
            Dictionary<string, int> word = new Dictionary<string, int>();
            try
            {
                StreamReader sr = new StreamReader(file);
                //читаем до конца строчки
                line = sr.ReadLine();
                //Читаем весь файл построчно
                while (line != null)
                {
                    // строка для записи слов
                    string test = "";
                    // символ для проверки
                    char a;
                    // флаг для проверки на ссылку
                    int c = 0;
                    //идем по всё строке
                    for (int i = 0; i < line.Length; i++)
                    {
                        //берем символ
                        a = line.ToCharArray()[i];
                        //проверка на скобку
                        if (a == 91)
                        {
                            int j = i + 1;
                            char b = line.ToCharArray()[j];
                            // записываем только символы, цифры отметаем
                            while (b != 93)
                            {
                                if ((b < 48 || b > 57) && b != 32)
                                {
                                    test = test + b;
                                }

                                j++;
                                b = line.ToCharArray()[j];
                            }
                            c = 1;
                        }
                        // закрывающаяся скобка, флаг возвращаем
                        if (a == 93)
                        {
                            c = 0;
                        }
                        if (c != 1)
                        {
                            // знаки припенания, скобки и прочие, что не является буквой/отдельной цифрой. Их не учитываем
                            if (a != 32 && a != 33 && a != 34 && a != 63 && a != 46 && a != 44 && a != 58 && a != 59 && a != 133 && a != 40 && a != 41
                                && a != 91 && a != 93 && a != 151)
                            {
                                // формирование слова
                                test = test + a;
                            }
                            // если столкнулись со знаком
                            else if (test != "")
                            {
                                // проверка на -
                                test = parsDef(test);
                                if (test != "")
                                {
                                    string myValue2 = "";
                                    // проходим по всему словарю и ищём нужный ключ, который совпадает со словом
                                    foreach (KeyValuePair<string, int> valuePair in word)
                                    {
                                        if (valuePair.Key.ToLower() == test.ToLower())
                                        {
                                            myValue2 = valuePair.Key;
                                        }
                                    }
                                    // если слово появилось впервые, то создаем такое ключ-значение и записываем в число повторений 1
                                    if (myValue2 == "")
                                    {
                                        word.Add(test.ToLower(), 1);
                                    }
                                    // иначе увеличиваем значение-счетчик на 1
                                    else
                                    {
                                        word[myValue2] = word[myValue2] + 1;
                                    }
                                }
                                // "обнуляем" строку для записи слов
                                test = "";
                            }
                        }
                    }
                    // запись в словарь последнего слова в документе
                    if (test != "")
                    {
                        test = parsDef(test);
                        if (test != "")
                        {
                            string myValue2 = "";
                            foreach (KeyValuePair<string, int> valuePair in word)
                            {
                                if (valuePair.Key.ToLower() == test.ToLower())
                                {
                                    myValue2 = valuePair.Key;
                                }
                            }
                            if (myValue2 == "")
                            {
                                word.Add(test, 1);
                            }
                            else
                            {
                                word[myValue2] = word[myValue2] + 1;
                            }
                        }
                        test = "";
                    }
                    //Читаем новую строку
                    line = sr.ReadLine();
                }
                //закрываем файл
                sr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return word;
        }
    }
}