using System;
using Assets.Script.ActionControl;
using Assets.Script.Avater;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.ActionList
{
    class ImpAction: ActionBasic
    {
        public bool idle(ActionStatus actionStatus)
        {
            RotateTowardSlerp(targetPos);
            return true;
        }

        public void Before_walk(ActionStatus actionStatus)
        {
            myAgent.updateRotation = false;
            myAgent.SetDestination(takecover());
        }

        public bool walk(ActionStatus actionStatus)
        {
            RotateTowardSlerp(targetPos);
            return true;
        }

        public void Before_fireball(ActionStatus actionStatus)
        {
            myAgent.ResetPath();
            gun.ChangeWeapon("MG");
        }

        public bool fireball(ActionStatus actionStatus)
        {
            /*
            if (doOnlyOnce)
            {
                //myAgent.SetDestination(target.transform.position);
                myAgent.ResetPath();
                gun.ChangeWeapon("MG");
                doOnlyOnce = false;
            }
            */
            if (gun.NowWeapon.BulletInMag > 0)
            {
                if (Vector3.Angle(my.transform.TransformDirection(Vector3.forward),
                        target.transform.position - my.transform.position) < 5)
                {
                    gun.fire();
                }
            }
            else
            {
                return false;
            }
            RotateTowardlerp(target.transform);
            return true;
        }

        public bool reload(ActionStatus actionStatus)
        {
            if (actionElapsedTime > actionStatus.Time1)
            {
                if (doOnlyOnce)
                {
                    gun.reload();
                    doOnlyOnce = false;
                }
            }
            return true;
        }

        public bool jumpslash(ActionStatus actionStatus)
        {
            if (doOnlyOnce)
            {
                myAgent.SetDestination(target.transform.position);
                gun.ChangeWeapon("katana");
                gun.NowWeapon.BulletInMag = 1;
                doOnlyOnce = false;
            }
            if (actionElapsedTime > actionStatus.Time1)
            {
                gun.StartSlash(actionStatus.Time2);
            }
            //RotateTowardlerp(target.transform);
            return true;
        }
    }
}
