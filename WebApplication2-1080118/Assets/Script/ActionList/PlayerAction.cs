using System;
using Assets.Script.ActionControl;
using Assets.Script.Avater;
using Assets.Script.Config;
using Assets.Script.weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.ActionList
{
    class PlayerAction : ActionBasic
    {
        public bool ChangeWeapon(ActionStatus actionStatus)
        {
            //input 得到現在按鈕的參照
            //將玩家武器Dictionary內部的索引值對應input的參照
            return true;
        }

        public bool Before_shoot(ActionStatus actionStatus)
        {
            gun.ChangeWeapon("MG");
            return true;
        }

        public bool shoot(ActionStatus actionStatus)
        {
            RotateTowardSlerp(target.transform.position);
            gun.fire();
            return true;
        }

        public bool Before_slash1(ActionStatus actionStatus)
        {
            gun.ChangeWeapon("katana");
            return true;
        }

        public bool slash1(ActionStatus actionStatus)
        {
            if (actionElapsedTime > actionStatus.Time1)
            {
                if (doOnlyOnce)
                {
                    gun.StartSlash(actionStatus.Time2);
                    doOnlyOnce = false;
                }
            }
            return true;
        }

        public bool Before_heavyslash(ActionStatus actionStatus)
        {
            gun.NowWeapon.charge = animator.GetFloat("charge");
            return true;
        }

        public bool heavyslash(ActionStatus actionStatus)
        {
            if (doOnlyOnce)
            {
                Debug.Log("your power is "+gun.NowWeapon.charge);    
            }
            return true;
        }

        public bool idle(ActionStatus actionStatus)
        {
            my.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //my.GetComponent<Rigidbody>().velocity = Vector3.Lerp(my.GetComponent<Rigidbody>().velocity,Vector3.zero,0);
            //myAgent.velocity = Vector3.Lerp(myAgent.velocity, Vector3.zero, 1);
            return true;
        }
        public bool Before_move(ActionStatus actionStatus)
        {
            return true;
        }
        public bool move(ActionStatus actionStatus)
        {
            if (Input.anyKey)
            {
                RotateTowardlerp(my.transform.position + Vector3.forward * input.ws + Vector3.right * input.ad);
                myAgent.velocity = my.transform.TransformDirection(Vector3.forward).normalized * 5f;
            }
            //my.GetComponent<Rigidbody>().velocity = ;
            //my.GetComponent<NavMeshAgent>().Move(my.transform.TransformDirection(Vector3.forward).normalized* 0.1f);

            gun.fire();
            return true;
        }

        public bool jump(ActionStatus actionStatus)
        {
            if (doOnlyOnce)
            {
                NowVecter = myAgent.velocity;
                myAgent.enabled = false;
                my.GetComponent<Rigidbody>().isKinematic = false;
                //my.GetComponent<Rigidbody>().AddForce(Vector3.up * 20f);
                my.GetComponent<Rigidbody>().velocity = NowVecter+Vector3.up * 20f;                
                doOnlyOnce = false;
            }
            if (Input.anyKey)
            {
                RotateTowardlerp(my.transform.position + Vector3.forward * input.ws + Vector3.right * input.ad);
                my.GetComponent<Rigidbody>().AddForce(Vector3.right * input.ad);
            }

            return true;
        }

        public bool falling(ActionStatus actionStatus)
        {
            if (doOnlyOnce)
            {
                myAgent.enabled = true;
                doOnlyOnce = false;
            }

            if (Input.anyKey)
            {
                RotateTowardlerp(my.transform.position + Vector3.forward * input.ws + Vector3.right * input.ad);
                my.GetComponent<Rigidbody>().AddForce(Vector3.right * input.ad);
            }
            return true;
        }

        public bool katana(ActionStatus actionStatus)
        {
            var katana = my.GetComponent<Gun>();
            katana.Bullet.GetComponent<Collider>().enabled = true;
            return true;
        }
        public bool stun()
        {
            return true;
        }
    }
}
