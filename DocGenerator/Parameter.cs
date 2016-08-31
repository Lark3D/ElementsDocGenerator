using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DocGenerator
{
    [Serializable]
    public class Parameter
    {
        [XmlAttribute]
        public string Name;
        public string Type;
        public string Text;
        public string Description;
        public string DefaultValue;
        public string MaxValue;
        public string MinValue;
        
    }
}
