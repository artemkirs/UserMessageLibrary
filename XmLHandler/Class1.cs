using System.Text;
using System.Xml.Linq;

namespace XmLHandler
{
    public static class CodeAnalysisTask
    {
        /// <summary>
        /// Изначальная функция
        /// </summary>
        static string Func1(string input, string elementName, string attrName)
        {
            string[] lines = System.IO.File.ReadAllLines(input);
            string result = null;

            foreach (var line in lines)
            {
                var startElEndex = line.IndexOf(elementName);
                if (startElEndex != -1)
                {
                    if (line[startElEndex - 1] == '<')
                    {
                        var endElIndex = line.IndexOf('>', startElEndex - 1);

                        var attrStartIndex = line.IndexOf(attrName, startElEndex, endElIndex - startElEndex + 1);
                        if (attrStartIndex != -1)
                        {
                            int valueStartIndex = attrStartIndex + attrName.Length + 2;
                            while (line[valueStartIndex] != '"')
                            {
                                result += line[valueStartIndex];
                                valueStartIndex++;
                            }
                            break;
                        }
                    }
                }
            }

            return result;
        }
        

        /// <summary>
        /// Решение
        /// Исходный код после рефакторинга
        /// </summary>
        public static string RefactoringFunc1(string input, string elementName, string attrName)
        {
            Checking(input, elementName, attrName);
            
            string result = String.Empty;
            string[] lines = System.IO.File.ReadAllLines(input, Encoding.UTF8);
            var elementStart = "<" + elementName;
            foreach (var line in lines)
            {
                var startElementIndex = line.IndexOf(elementStart);
                var endEllementIndex = line.IndexOf('>', startElementIndex + elementStart.Length);
                if (endEllementIndex == -1)
                {
                    throw new Exception("Не верный формат XML");
                }

                var attrStartIndex = line.IndexOf(attrName, startElementIndex + elementStart.Length, endEllementIndex - 1);
                if (attrStartIndex != -1)
                {
                    int valueStartIndex = attrStartIndex + attrName.Length + 2;
                    while (line[valueStartIndex] != '"')
                    {
                        result += line[valueStartIndex];
                        valueStartIndex++;
                    }
                    break;
                }
            }
            
            return result;
            
        }

       
        /// <summary>
        /// Проверка параметров поиска
        /// </summary>
        /// <param name="input"> Путь</param>
        /// <param name="elementName">Название элемента</param>
        /// <param name="attrName">Название атрибута</param>
        private static void Checking(string input, string elementName, string attrName)
        {
            if (String.IsNullOrWhiteSpace(input) && !File.Exists(input))
            {
                throw new Exception("Файл не найден или не верно указан путь до файла");
            }

            if (string.IsNullOrWhiteSpace(elementName) && string.IsNullOrWhiteSpace(attrName))
            {
                throw new Exception("Не верно заданы параметры поика");
            }
        }
    }
}