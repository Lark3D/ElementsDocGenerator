using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Load elements from the .xml file
                var xmlLoader = new XmlLoader("ElementParameters.xml");
                List<Element> elements = xmlLoader.LoadElements();

                // Save docs describing the elements
                DocOperator docOperator = new DocOperator();
                foreach (Element element in elements)
                {
                    docOperator.GenerateElementDoc(element, element.Text);
                }

                Console.WriteLine("Документация была успешно сгенерирована в папку result");
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка. Документация не была сгенерирована");
            }

            Console.ReadKey();
        }
    }
}
