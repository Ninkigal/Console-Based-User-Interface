using ConsoleBasedMenuPoC.model;
using ConsoleBasedMenuPoC.ui;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.Linq;

namespace ConsoleBasedMenuPoC
{
    public static class Menu
    {
        private static TextItem[] textItems = Text.GetArr();

        public static void Initialize()
        {
            Menu.displayMenu();
            while(true)
            {
                Menu.processInput(System.Console.ReadKey());
            }
        }

        private static void processInput(ConsoleKeyInfo info)
        {
            System.Console.Clear();
            Menu.displayMenu();
            MenuItem[] menu = Menu.loadXMLMenu();
            string action = string.Empty;

            foreach(MenuItem menuItem in menu)
            {
                if(menuItem.ActivationKey.Equals(Char.ToUpper(info.KeyChar)))
                {
                    action = menuItem.Action;
                    break;
                }
            }

            if(!action.Equals(string.Empty))
            {
                try
                {
                    string dtype = string.Format("{0}.{1}", action.Split('.')[0], action.Split('.')[1]);
                    Type type = Type.GetType(dtype);
                    MethodInfo method = type.GetMethod(action.Split('.')[2], BindingFlags.Static | BindingFlags.Public);
                    method.Invoke(null, null);
                }
                catch
                {
                    throw new Exception("err pasrsing Menu.xml");
                }
            }
        }

        private static void displayMenu()
        {
            MenuItem[] menu = Menu.loadXMLMenu();
            var output = string.Empty;
            output += string.Format("{0}{1}", "=============", Environment.NewLine);
            output += string.Format("{0}{1}", "Key  |   Menu", Environment.NewLine);
            output += string.Format("{0}{1}", "=====|=======", Environment.NewLine);
            foreach(MenuItem menuItem in menu)
            {
                output += string.Format("{0}    |   {1}{2}", menuItem.ActivationKey, Text.GetText(menuItem.TextKey, Menu.textItems), Environment.NewLine);
            }
            output += string.Format("{0}{1}", "=============", Environment.NewLine);
            System.Console.WriteLine(output);
        }

        private static MenuItem[] loadXMLMenu()
        {
            List<MenuItem> answer = new List<MenuItem>();
            XmlDocument xml = new XmlDocument();
            xml.Load(@ConfigurationManager.AppSettings["APP_MENU_PATH"]);
            XmlNode root = xml.SelectSingleNode("items");
            foreach(XmlNode node in root.ChildNodes)
            {
                MenuItem menuItem = new MenuItem
                {
                    ActivationKey = node.Attributes["ActivationKey"].Value[0],
                    TextKey = node.Attributes["TextKey"].Value,
                    Action = node.Attributes["Action"].Value
                };
                answer.Add(menuItem);
            }
            return answer.ToArray();
        }
    }
}
