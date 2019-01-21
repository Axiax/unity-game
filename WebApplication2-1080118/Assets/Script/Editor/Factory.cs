using System;
using System.Diagnostics;

namespace Assets.Script.Editor
{
    public class GunFactory
    {
        public IGuntype GetGun(string gun)
        {
            if (gun == null){return null;}

            if (gun == "Gun"){ return new Gun();}
            else if (gun == "Rocket") {return  new Rocket();}

            else return null;
        }
    }

    public class MovetypeFactory
    {
        public IMoveType GetMoveType(string move)
        {
            if (move == null){return null;}

            if (move == "Foot") { return new Foot();}
            else if (move == "Car"){return new Car();}
            else if (move == "Plane"){return new Plane();}

            else return null;
        }
    }

    public interface IGuntype
    {
        void Fire(float speed, float rof);
        void Reload(float time);
    }

    class Gun : IGuntype
    {
        public void Fire(float speed, float rof)
        {
            Debug.WriteLine("Fire:"+speed+"rof:"+rof);
        }

        public void Reload(float time)
        {
            Debug.WriteLine("reload"+time);
        }
    }

    class Rocket : IGuntype
    {
        public void Fire(float speed, float rof)
        {
            Debug.WriteLine("Boom:" + speed + "rof:" + rof);
        }

        public void Reload(float time)
        {
            Debug.WriteLine("reload" + time);
        }
    }

    public interface IMoveType
    {
        void Move(float speed, float dir);
    }

    class Foot : IMoveType
    {
        public void Move(float speed,float dir)
        {
            //Console.WriteLine("以"+speed+"的速度跑向"+dir);
            Console.Write("以" + speed + "的速度跑向" + dir);

        }
    }
    class Car : IMoveType
    {
        public void Move(float speed, float dir)
        {
            Console.Write("以" + speed + "的BT-7衝向" + dir);
        }
    }
    class Plane : IMoveType
    {
        public void Move(float speed, float dir)
        {
            Console.Write("以" + speed + "的BF-109飛向" + dir);
        }
    }
}
