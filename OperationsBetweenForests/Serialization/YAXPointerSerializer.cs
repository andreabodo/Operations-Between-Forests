using GraphX.Measure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YAXLib;

namespace OperationsBetweenForests.Serialization
{
    class YAXPointerSerializer : ICustomSerializer<Point>
    {
        public Point DeserializeFromAttribute(XAttribute attrib)
        {
            return Deserialize(attrib.Value);
        }

        public Point DeserializeFromElement(XElement element)
        {
            return Deserialize(element.Value);
        }

        public Point DeserializeFromValue(string value)
        {
            return Deserialize(value);
        }

        public void SerializeToAttribute(Point objectToSerialize, XAttribute attrToFill)
        {
            attrToFill.Value = String.Format("{0}|{1}", objectToSerialize.X.ToString(), objectToSerialize.Y.ToString());
        }

        public void SerializeToElement(Point objectToSerialize, XElement elemToFill)
        {
            elemToFill.Value = String.Format("{0}|{1}", objectToSerialize.X.ToString(), objectToSerialize.Y.ToString());
        }

        public string SerializeToValue(Point objectToSerialize)
        {
            return String.Format("{0}|{1}", objectToSerialize.X.ToString(), objectToSerialize.Y.ToString());
        }

        private Point Deserialize(String str)
        {
            var res = str.Split(new char[] { '|' });
            if (res.Length == 2) return new Point(Convert.ToDouble(res[0]), Convert.ToDouble(res[1]));
            else return new Point();
        }
    }
}
