using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Script.ActionControl;

namespace Assets.Script.ActionList
{
    class ShotgunSergeantAction:ActionBasic
    {
        public bool idle(ActionStatus actionStatus)
        {
            return true;
        }

        public bool walk(ActionStatus actionStatus)
        {
            if (doOnlyOnce)
            {
                myAgent.SetDestination(target.transform.position);
                doOnlyOnce = false;
            }
            return true;
        }

        public bool Shoot()
        {
            if (doOnlyOnce)
            {
                myAgent.SetDestination(target.transform.position);
                doOnlyOnce = false;
            }
            return true;
        }
    }
}
