using ConsoleBasedMenuPoC.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleBasedMenuPoC.ui
{
    public static class Text
    {
        private static TextItem[] loadXMLText()
        {
            List<TextItem> answer = new List<TextItem>();
            XmlDocument xml = new XmlDocument();
            xml.Load(@ConfigurationManager.AppSettings["APP_TEXT_PATH"]);
            XmlNode root = xml.SelectSingleNode("items");
            foreach (XmlNode node in root.ChildNodes)
            {
                TextItem textItem = new TextItem
                {
                    TextKey = node.Attributes["TextKey"].Value,
                    TextValue = node.Attributes["TextValue"].Value
                };
                answer.Add(textItem);
            }
            return answer.ToArray();
        }

        public static TextItem[] GetArr()
        {
            return Text.loadXMLText();
        }

        public static string GetText(string key, TextItem[] list)
        {
            foreach(TextItem item in list)
            {
                if(item.TextKey.Equals(key))
                {
                    return item.TextValue;
                }
            }
            return null;
        }
    }
}
