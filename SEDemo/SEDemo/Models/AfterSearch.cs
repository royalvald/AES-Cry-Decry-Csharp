using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SEDemo.Models
{
    class AfterSearch
    {
        public int UpdateSearchHistory(string tokenString, string fileList)
        {
            string rootPath = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("\\bin"));
            string xmlPath = rootPath + "\\" + "history.xml";

            XmlDocument document = new XmlDocument();
            if (!File.Exists(xmlPath))
            {
                File.Create(xmlPath);
                document.Load(rootPath);
                XmlElement rootElement = document.CreateElement("root");
                document.AppendChild(rootElement);
                return -1;
            }
            else  document.Load(rootPath); 



            XmlNode parentNode = document.SelectSingleNode("/root//token[@Id='" + tokenString + "']");
            if (parentNode == null)
            {
                XmlElement element = document.CreateElement("token");
                element.SetAttribute("Id", tokenString);
                XmlNode rootNode = document.SelectSingleNode("/root");
                rootNode.AppendChild(element);
                parentNode = element;
            }

            XmlElement fileNode = document.CreateElement("file");
            fileNode.SetAttribute("Id", fileList);
            parentNode.AppendChild(fileNode);

            document.Save(xmlPath);
            return 0;
        }


        public List<string> GetTokenHistory(string token)
        {
            string rootPath = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("\\bin"));
            string xmlPath = rootPath + "\\" + "history.xml";

            XmlDocument document = new XmlDocument();
            if (File.Exists(xmlPath))
            {
                document.Load(xmlPath);
            }
            //else return -1;//没有搜索记录立刻返回

            //find the token
            string nodePath = "/root//token[@Id='" + token + "']";
            XmlNode node = document.SelectSingleNode(nodePath);

            //add the file ID
            List<string> list = null;
            if (node != null)
            {               
                foreach (XmlElement item in node.ChildNodes)
                {
                    list.Add(item.GetAttribute("Id"));
                }
            }

            return list;
        }
    }
}
