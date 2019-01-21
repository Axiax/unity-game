using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using UnityEngine;

namespace Assets.Script.ActionControl
{
    public class ActionStatusDictionary
    {
        //初始化
        private string Charactername;
        //1.工廠模式返回 2.字典集
        private ActionBasic ActionBasic;

        private List<ActionStatus> _moveList;
        private List<ActionStatus> _attackList;
        private List<ActionStatus> _specialList;

        public Dictionary<string, ActionStatus> AllActionStatusDictionary;
        public Dictionary<string, ActionStatus> MoveDictionary;
        public Dictionary<string, ActionStatus> CloseRangeDictionary;
        public Dictionary<string, ActionStatus> LongRangeDictionary;
        //actionScript.ActionStatus = actionStatuses.Find(x => x.ActionName == name);

        /// <summary>
        /// 創造腳色動作資料庫，完成後透過ApplyAction來
        /// </summary>
        /// <param name="charactername"></param>
        /// 
        public void Init(string charactername)
        {
            this.Charactername = charactername;
            this.MoveDictionary = LoadAttributesDictionary("move");
            this.CloseRangeDictionary = LoadAttributesDictionary("close");
            this.LongRangeDictionary = LoadAttributesDictionary("long");
            this.AllActionStatusDictionary = MoveDictionary.Concat(CloseRangeDictionary).Concat(LongRangeDictionary)
                .GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value);
        }

        //將XML的值載入人物的各動作表(數值)
        private Dictionary<string,ActionStatus> LoadAttributesDictionary(string type)
        {
            Dictionary<string, ActionStatus> actDictionary = new Dictionary<string, ActionStatus>();
            XmlDocument doc = new XmlDocument();
            doc.Load("data\\actionstatus\\" + Charactername + "Status.xml");
            XmlNodeList nodeList = doc.SelectNodes(Charactername + "/" + type);

            foreach (XmlNode node in nodeList)
            {
                string actname = node.Attributes["name"].Value;
                float time1 = (float)Convert.ToDouble(node.SelectSingleNode("time").Attributes.GetNamedItem("value").Value);
                float time2 = (float)Convert.ToDouble(node.SelectSingleNode("time2").Attributes.GetNamedItem("value").Value);
                float chance = (float)Convert.ToDouble(node.SelectSingleNode("pos").Attributes.GetNamedItem("value").Value);
                float speed = (float)Convert.ToDouble(node.SelectSingleNode("speed").Attributes.GetNamedItem("value").Value);
                float x = (float)Convert.ToDouble(node.SelectSingleNode("vector3").Attributes.GetNamedItem("x").Value);
                float y = (float)Convert.ToDouble(node.SelectSingleNode("vector3").Attributes.GetNamedItem("y").Value);
                float z = (float)Convert.ToDouble(node.SelectSingleNode("vector3").Attributes.GetNamedItem("z").Value);
                string ignore = node.SelectSingleNode("ignore").Attributes.GetNamedItem("list").Value;
                string[] ignorelist = ignore.Split(',');

                ActionStatus ac = new ActionStatus();
                ac.Init(actname, time1, time2, chance, speed, new Vector3(x, y, z));
                if (ignore != "0")
                {
                    ac.ignorelist = ignorelist;
                }
                actDictionary.Add(actname,ac);
            }
            return actDictionary;
        }

        public ActionStatus FindByNameDic(string name, string type)
        {
            switch (type)
            {
                case "move":
                    return MoveDictionary[name];
                case "close":
                    return CloseRangeDictionary[name];
                case "long":
                    return LongRangeDictionary[name];
            }

            return null;
        }
    }
}
