using UnityEngine;

namespace Assets.Script.ActionControl
{
    /// <summary>
    /// 動作列表，未來加上一個virturltype在上面作為工廠模式原始
    /// </summary>
    public class ActionStatus
    {
        public string ActionName { get; set; }
        public float Time1 { get; set; }
        public float Time2 { get; set; }
        public float Chance { get; set; }
        public float Speed { get; set; }
        public Vector3 Vector3 { get; set; }
        public string[] ignorelist { get; set; }

        //初始化
        public void Init(string actionname, float time1,
            float time2, float chance,float speed,Vector3 vector3)
        {
            this.ActionName = actionname;
            this.Time1 = time1;
            this.Time2 = time2;
            this.Chance = chance;
            this.Speed = speed;
            this.Vector3 = vector3;
        }
    }
}
