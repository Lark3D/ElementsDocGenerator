using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace DocGenerator
{
    [Serializable]
    public class XmlLoader
    {
        string _file;
        public List<Element> Parameters = new List<Element>();

        public XmlLoader(string file)
        {
            _file = file;
        }

        public List<Element> LoadElements()
        {
            List<Element> elements = new List<Element>();
            XDocument doc = XDocument.Load(_file);

            foreach (XElement xElement in doc.Root.Elements())
            {
                Element newElement = new Element();

                newElement.Code = (string)xElement.Attribute("Code");
                newElement.Description = (string)xElement.Element("Description");
                newElement.Text = (string)xElement.Element("Text");

                XElement xParameters = xElement.Element("Parameters");

                foreach (XElement xParameter in xParameters.Elements())
                {
                    Parameter newParameter = new Parameter();
                    newParameter.Name = (string)xParameter.Attribute("Name");
                    newParameter.Text = (string)xParameter.Element("Text");
                    newParameter.Description = (string)xParameter.Element("Description");
                    newElement.Parameters.Add(newParameter);
                }

                elements.Add(newElement);
            }

            return elements;
        }

        public string Utf8ToASCII(string utf8String)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding utf8 = Encoding.UTF8;
            byte[] unicodeBytes = utf8.GetBytes(utf8String);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(utf8, ascii, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);

            return asciiString;
        }

    }

}
