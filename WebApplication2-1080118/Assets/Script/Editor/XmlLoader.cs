using System.Xml;

namespace Assets.Script.Editor
{
    class XmlLoader
    {
        /// <summary>
        /// 讀取Xml指定的節點，返回為list
        /// 檔名末須加上.xml,節點格式為AAAA/BBBB
        /// </summary>
        /// <param name="fileName">檔案名稱</param>
        /// <param name="nodePath">節點名稱</param>
        public object GetNode(string fileName, string nodePath)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(fileName);
            //格式請遵照 XXX.xml傳入
            XmlNode main = doc.SelectSingleNode(nodePath);
            //格式為AAAA/BBBB
            if (main == null) return null;
            XmlElement element = (XmlElement)main;
            XmlAttribute attribute = element.GetAttributeNode("value");
            return attribute.Value;
            
            /* 返回集合 
            XmlAttributeCollection attributes = element.Attributes;
            string content = "";
            foreach (XmlAttribute item in attributes)
            {
                content += item.Name + "," + item.Value + Environment.NewLine;
            }
            */
        }

        /// <summary>
        /// 載入XML
        /// </summary>
        /// <param name="fileName">檔案名稱,不須副檔名</param>
        /// <param name="role">腳色名稱,也可用來找子節點</param>
        /// <param name="act">動作名稱,和腳色名稱可合成路徑</param>
        /// <param name="attr">屬性值</param>
        /// <returns></returns>
        public object LoadAct(string fileName, string role, string act, string attr)
        {
            return GetNode(fileName + ".xml", role + "/" + act + "/" + attr);
        }
    }
}
