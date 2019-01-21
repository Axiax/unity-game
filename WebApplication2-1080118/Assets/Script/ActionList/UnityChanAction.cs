using System;
using Assets.Script.ActionControl;
using Assets.Script.Avater;
using Assets.Script.Config;
using Assets.Script.weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.ActionList
{
    class UnityChanAction : ActionBasic
    {
        public bool ChangeWeapon(ActionStatus actionStatus)
        {
            //input 得到現在按鈕的參照
            //將玩家武器Dictionary內部的索引值對應input的參照
            return true;
        }

        public void Before_shoot(ActionStatus actionStatus)
        {
            gun.ChangeWeapon("MG");
        }

        public bool shoot(ActionStatus actionStatus)
        {
            RotateTowardSlerp(target.transform.position);
            gun.fire();
            return true;
        }

        public void Before_slash1(ActionStatus actionStatus)
        {
            gun.ChangeWeapon("katana");
        }

        public void slash1(ActionStatus actionStatus)
        {
            if (actionElapsedTime > actionStatus.Time1)
            {
                if (doOnlyOnce)
                {
                    gun.StartSlash(actionStatus.Time2);
                    doOnlyOnce = false;
                }
            }
        }

        public void Before_heavyslash(ActionStatus actionStatus)
        {
            gun.NowWeapon.charge = animator.GetFloat("charge");
        }

        public bool heavyslash(ActionStatus actionStatus)
        {
            if (doOnlyOnce)
            {
                myAgent.velocity = my.transform.TransformDirection
                    (Vector3.forward*5f+Vector3.up*100); 
                Debug.Log("your power is "+gun.NowWeapon.charge); 
                doOnlyOnce = false;   
            }
            return true;
        }

        public bool idle(ActionStatus actionStatus)
        {
            //var camPos = camera.transform.TransformDirection(Vector3.back *input.ws+Vector3.left*input.ad);
            //RotateTowardlerp(my.transform.position-camPos);
            myAgent.velocity = Vector3.Lerp(myAgent.velocity, Vector3.zero, 0.5f);
            return true;
        }

        public bool move(ActionStatus actionStatus)
        {
            if (Input.anyKey)
            {
                var camPos = camera.transform.TransformDirection(Vector3.back *input.ws+Vector3.left*input.ad);
                RotateTowardlerp(my.transform.position-camPos);
                myAgent.velocity = my.transform.TransformDirection(Vector3.forward).normalized * 5f;
            }

            //gun.fire();
            return true;
        }

        public void Before_strafe(ActionStatus actionStatus)
        {
            gun.ChangeWeapon("MG");
        }

        public bool strafe(ActionStatus actionStatus)
        {
            FPSLikeMovement(5f,10f);
            if(Input.GetButton("Fire1"))
            {
                return gun.fire();
            }
            return true;
        }

        public void Before_jump(ActionStatus actionStatus)
        {
            animator.SetBool("avater_can_jump",false);
            if(myAgent.agentTypeID != -334000983)
            {
                NowVecter = myAgent.velocity;
            }
            Debug.Log(NowVecter);
            myAgent.enabled = false;
            myRig.isKinematic = false;
            myRig.useGravity = true;
            //myRig.AddForce(NowVecter+Vector3.up * 5f,ForceMode.Impulse); //目前最佳數值.不確定之前別砍
            myRig.AddForce(NowVecter+Vector3.up * 10f,ForceMode.Impulse);
        }
        public bool jump(ActionStatus actionStatus)
        {
            FPSLikeMovement(3f,2f);
            return true;
        }
        public bool falling(ActionStatus actionStatus)
        {
            //myRig.isKinematic = true;
            myRig.velocity = my.transform.TransformVector(Vector3.forward);
            //FPSLikeMovement(10f,2f);            
            return true;
        }
        public void Before_dodge(ActionStatus actionStatus)
        {
            var col = my.GetComponent<Collider>();
            col.enabled = false;
        }

        public bool After_dodge(ActionStatus actionStatus)
        {
            var col = my.GetComponent<Collider>();
            col.enabled = true;
            return true;
        }
        public bool magnet_melee(ActionStatus actionStatus)
        {
            //參考:https://blog.csdn.net/u013700908/article/details/52888792
            //球體碰撞器來返回目標，夾角小於X時就會自動追尾
            float range = 10f;
            Collider[] colliders = Physics.OverlapSphere(my.transform.position,range,-1/*LayerMask*/);
            Debug.Log(colliders[0].name);//找到物件
            return true;
        }
        #region 跑牆
        /// <summary>
        /// 2019-01-18新增
        /// </summary>
        /// <param name="actionStatus"></param>
        /// <returns></returns>
        public void Before_wallrunR(ActionStatus actionStatus)
        {
            myRig.useGravity = false;
            myRig.isKinematic = true;
            myAgent.agentTypeID = -334000983;            
            myAgent.updateUpAxis = false;
            //myAgent.updateRotation = false;
            myAgent.enabled = true;

            animator.SetBool("avater_can_jump",true);
            NowVecter = myRig.velocity;         
        }
        public bool wallrunR(ActionStatus actionStatus)
        {
            var col = my.GetComponent<PlayerAvater>().col;
            myAgent.velocity = col.transform.TransformVector(Vector3.right*3);
            return true;
        }
        public bool After_wallrunR(ActionStatus actionStatus)
        {
            var col = my.GetComponent<PlayerAvater>().col;
            NowVecter = col.transform.TransformVector(Vector3.forward*10);
            Debug.Log(NowVecter);
            return true;
        }
        public bool wallrunL(ActionStatus actionStatus)
        {
            
            myAgent.velocity = my.transform.TransformVector(Vector3.left*3);
            return true;
        }
        public bool wallrunU(ActionStatus actionStatus)
        {
            
            myAgent.velocity = my.transform.TransformVector(Vector3.up*3);
            return true;
        }
        #endregion

    }
}
