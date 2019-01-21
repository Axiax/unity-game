using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Assets.Script.weapon
{
    class WeaponFactory
    {
        public Dictionary<string, WeaponBasic> AllWeaponDictionary;
        public Dictionary<string, WeaponBasic> RangeDictionary;
        public Dictionary<string, WeaponBasic> RocketDictionary;
        public Dictionary<string, WeaponBasic> CloseDictionary;

        public void Init()
        {
            RangeDictionary = Loadweapon("range", "rifle");
            RocketDictionary = Loadweapon("range", "rocket");
            CloseDictionary = Loadweapon("close", "sword");
            AllWeaponDictionary = RangeDictionary.Concat(CloseDictionary).Concat(RocketDictionary).GroupBy(d => d.Key)
                .ToDictionary(d => d.Key, d => d.First().Value);
        }

        //開始時就載入全列表，被呼叫時返回一個新的武器給該單位
        //近接和遠程分開
        public Dictionary<string, WeaponBasic> Loadweapon(string close_or_range, string weapontype)
        {
            Dictionary<string, WeaponBasic> weapondictionary = new Dictionary<string, WeaponBasic>();
            XmlDocument doc = new XmlDocument();
            doc.Load("data\\weapon\\weapon.xml");
            XmlNodeList weapontypeNode = doc.SelectNodes("weapon/" + close_or_range + "/" + weapontype);
            XmlNodeList nodeList = weapontypeNode[0].ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                string name = node.Attributes["name"].Value;
                string type = node.Attributes["type"].Value;

                int damage = Convert.ToInt32(node.Attributes["damage"].Value);
                int magsize = Convert.ToInt32(node.Attributes["magsize"].Value);
                int bulletusedpershot = Convert.ToInt32(node.Attributes["bulletusedpershot"].Value);
                int maxammo = Convert.ToInt32(node.Attributes["maxammo"].Value);
                int nowammo = Convert.ToInt32(node.Attributes["nowammo"].Value);

                float charge = (float)Convert.ToDouble(node.Attributes["charge"].Value);
                float acc = (float)Convert.ToDouble(node.Attributes["acc"].Value);
                float stun = (float)Convert.ToDouble(node.Attributes["stun"].Value);
                float rof = (float)Convert.ToDouble(node.Attributes["rof"].Value);
                float dropoff = (float)Convert.ToDouble(node.Attributes["dropoff"].Value);
                float speed = (float)Convert.ToDouble(node.Attributes["speed"].Value);
                float recoil = (float)Convert.ToDouble(node.Attributes["recoil"].Value);
                float blast = (float)Convert.ToDouble(node.Attributes["blast"].Value);

                GameObject weaponGameObject = Resources.Load("Prefabs/" + name) as GameObject;

                WeaponBasic weapon = new WeaponBasic()
                {
                    name = name,type = type,Damage = damage,MagSize = magsize,BulletUsedPerShot = bulletusedpershot
                    ,acc=acc,dropoff = dropoff,speed = speed,recoil = recoil,rof=rof,maxammo = maxammo,nowammo = nowammo
                    ,stun = stun,charge = charge,blast = blast,weapon = weaponGameObject
                };
                weapondictionary.Add(name, weapon);
            }
            return weapondictionary;
        }
    }


    public class WeaponBasic
    {
        public string name;
        public string type;

        public int Damage;
        public int MagSize;
        public int BulletInMag;
        public int BulletUsedPerShot;
        public int maxammo;
        public int nowammo;

        public float charge;
        public float acc;
        public float stun;
        public float rof;
        public float dropoff;
        public float speed;
        public float recoil;
        public float blast;

        public GameObject weapon;
        public GameObject bullet;
    }
}
