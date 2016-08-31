using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DocGenerator
{
    [Serializable]
    public class Element
    {
        [XmlAttribute]
        public string Code;
        public string Text;
        public string Description;
        public List<Parameter> Parameters = new List<Parameter>();
       
    }
}
