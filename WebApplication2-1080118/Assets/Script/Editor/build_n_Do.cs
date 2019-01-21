using UnityEditor;
using UnityEngine;

namespace Assets.Script.Editor
{
    class Buildit
    {
        private GunFactory GunFactory { get; set; }
        private MovetypeFactory MovetypeFactory { get; set; } 

        public IGuntype Guntype { get; set; }
        public IMoveType MoveType { get; set; }

        public void SetFactory()
        {
            GunFactory = new GunFactory();
            MovetypeFactory = new MovetypeFactory();
        }

        public void SetObject(string guntype,string movetype)
        {
            Guntype = GunFactory.GetGun(guntype);
            MoveType = MovetypeFactory.GetMoveType(movetype);
        }
    }
    [InitializeOnLoad]
    class Doit : MonoBehaviour
    {
        public static void Main()
        {
            Buildit buildit = new Buildit();
            XmlController xmlController = new XmlController(){Filename = "PlayerStatus",Role = "Player"};
            
            //初始化
            buildit.SetFactory();
            buildit.SetObject("Gun","Foot");
            
            //傳遞區
            var gun = buildit.Guntype;
            var move = buildit.MoveType;
            gun.Fire(xmlController.GetFloatXml("Fire","Atk"),xmlController.GetFloatXml("Fire","Rof"));
            Debug.Log("Ahhhh");            
        }
    }
}
