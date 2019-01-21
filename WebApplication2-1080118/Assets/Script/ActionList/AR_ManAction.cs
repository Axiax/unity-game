using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.ActionControl;

namespace Assets.Script.ActionList
{
    class AR_ManAction: ActionBasic
    {
        public bool idle(ActionStatus actionStatus)
        {
            return true;
        }

        public bool Before_move(ActionStatus actionStatus)
        {

            return true;
        }

        public bool move(ActionStatus actionStatus)
        {
            return true;
        }

        public bool running(ActionStatus actionStatus)
        {
            return true;
        }
    }
}
