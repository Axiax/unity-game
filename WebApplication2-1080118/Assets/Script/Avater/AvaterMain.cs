using Assets.Script.ActionControl;
using Assets.Script.weapon;
using Boo.Lang;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Avater
{
    class AvaterMain:MonoBehaviour
    {
        protected ActionBasicBuilder actionBasicBuilder = new ActionBasicBuilder();
        protected ActionStatusDictionary actionStatusDictionary = new ActionStatusDictionary();
        protected ActionBasic actionBasic = new ActionBasic();
        protected Animator animator;

        public ActionStatus OldActionStatus;
        public ActionStatus NowActionStatus;
        private List<string> candolist = new List<string>();

        public float ActionElapsedTime;
        public bool IsEndNormal = true;

        public void Init_Avater()
        {
            actionStatusDictionary.Init(gameObject.name);
            actionBasic = actionBasicBuilder.GetActionBaseByName(gameObject.name);
            actionBasic.Init(gameObject);

            animator = gameObject.GetComponent<Animator>();
        }

        public void GetAnimaterParameter()
        {
            foreach (var animaterparameter in animator.parameters)
            {
                if (animaterparameter.name.StartsWith("avater_can"))
                {
                    candolist.Add(animaterparameter.name);
                }
            }
        }

        public void RefreshAnimaterParameter()
        {
            foreach (var ignore in candolist)
            {
                animator.SetBool(ignore,true);
            }
        }

        public void OnHit()
        {
            animator.SetTrigger("avatermain_stun");
        }

        public void OnSpecial(string Name,float value)
        {
            animator.CrossFade(Name, 0);
            actionBasic.doOnlyOnce = true;
            NowActionStatus.ActionName = Name;
        }

        public void recover()
        {
            gameObject.GetComponent<Gun>().NowWeapon.weapon.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
