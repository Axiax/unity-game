using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Script.AIGroup;
using Assets.Script.Avater;
using Assets.Script.Config;
using Assets.Script.weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.ActionControl
{
    class ActionBasicBuilder
    {
        public ActionBasic GetActionBaseByName(string ActionName)
        {
            Type t = Type.GetType("Assets.Script.ActionList." + ActionName+"Action");
            ActionBasic actionBase = new ActionBasic();
            actionBase = (ActionBasic)Activator.CreateInstance(t);
            return actionBase;
        }
    }
    /// <summary>
    /// 這裡負責關於人物的面向，移動，動作
    /// </summary>
    public class ActionBasic
    {
        public Animator animator;

        public GameObject my;
        public GameObject target;

        protected Rigidbody myRig;
        protected NavMeshAgent myAgent;
        protected Vector3 targetPos;
        protected Vector3 NowVecter;
        protected Vector3 CamFront;
        protected InputManager input;

        protected Camera camera;

        protected Gun gun;

        protected float actionStartTime;
        protected float actionElapsedTime;

        public bool doOnlyOnce;

        public void Init(GameObject my)
        {
            this.my = my;
            //this.myAgent = this.my.GetComponent<NavMeshAgent>();
            this.myAgent = this.my.GetComponentInChildren<NavMeshAgent>();
            this.gun = my.GetComponent<Gun>();
            this.animator = my.GetComponent<Animator>();
            this.myRig = my.GetComponent<Rigidbody>();

            if (my.GetComponent<InputManager>())
            {
                input = my.GetComponent<InputManager>();
            }

            if(my.transform.Find("Camera"))
            {
                camera = my.transform.Find("Camera").GetComponent<Camera>();
                CamFront = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.nearClipPlane));
            }

            actionElapsedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        }
        #region CustomAction
        public void BeforeCustomAction(ActionStatus actionStatus)
        {
            Type actionbaseType = GetType();
            MethodInfo methodInfo = actionbaseType.GetMethod("Before_"+actionStatus.ActionName);
            if (methodInfo != null)
                methodInfo.Invoke(this, new object[] { actionStatus }/*放入actionStatus*/);
        }

        public bool CustomAction(ActionStatus actionStatus)
        {
            Type actionbaseType = GetType();
            MethodInfo methodInfo = actionbaseType.GetMethod(actionStatus.ActionName);
            return (bool)methodInfo.Invoke(this, new object[] { actionStatus }/*放入actionStatus*/);
        }

        public void AfterCustomAction(ActionStatus actionStatus)
        {
            Type actionbaseType = GetType();
            MethodInfo methodInfo = actionbaseType.GetMethod("After_"+actionStatus.ActionName);
            if (methodInfo != null)
                methodInfo.Invoke(this, new object[] { actionStatus }/*放入actionStatus*/);
        }
        public bool CustomAction(string actionName)
        {
            //var actionStatus = FindByNameDic(action, type);
            Type actionbaseType = GetType();
            MethodInfo methodInfo = actionbaseType.GetMethod(actionName);
            return (bool)methodInfo.Invoke(this, null/*放入actionStatus*/);
        }

        #endregion
        public void SetupBeforeAction()
        {
            //targetAgent.FindClosestEdge(out coverPos);
            doOnlyOnce = true;
            actionStartTime = Time.time;
        }
        #region RotateTowardSlerp
        protected void RotateTowardSlerp(Transform target)
        {
            Vector3 direction = (target.position - my.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
            my.transform.rotation = Quaternion.Slerp(my.transform.rotation, lookRotation, Time.deltaTime * 1.7f/*rotationSpeed*/);
        }

        protected void RotateTowardSlerp(Vector3 target)
        {
            Vector3 direction = (target - my.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
            my.transform.rotation = Quaternion.Slerp(my.transform.rotation, lookRotation, Time.deltaTime * 1.7f/*rotationSpeed*/);
        }

        protected void RotateTowardSlerp(Vector3 target,float speed)
        {
            Vector3 direction = (target - my.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
            my.transform.rotation = Quaternion.Slerp(my.transform.rotation, lookRotation, Time.deltaTime * speed/*rotationSpeed*/);
        }

        protected void RotateTowardlerp(Transform target)
        {
            Vector3 direction = (target.position - my.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
            my.transform.rotation = Quaternion.Lerp(my.transform.rotation, lookRotation, Time.deltaTime * 1.7f/*rotationSpeed*/);
        }

        protected void RotateTowardlerp(Vector3 target)
        {
            Vector3 direction = (target - my.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
            my.transform.rotation = Quaternion.Lerp(my.transform.rotation, lookRotation, Time.deltaTime * 3f/*rotationSpeed*/);
        }

        protected void RotateTowardlerp(Vector3 target, float speed)
        {
            Vector3 direction = (target - my.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
            my.transform.rotation = Quaternion.Lerp(my.transform.rotation, lookRotation, Time.deltaTime * speed/*rotationSpeed*/);
        }
        #endregion
        public void ChangeTarget(GameObject targetGameObject)
        {
            this.target = targetGameObject;
        }

        public void TakeCoverPath()
        {
            NavMeshPath path = new NavMeshPath();
            myAgent.CalculatePath(target.transform.position, path);
            //得到倒數2個轉角的位置
            Vector3 point1 = path.corners[path.corners.Length - 2];
            Vector3 point2 = path.corners[path.corners.Length - 3];
            //得到最後一條往轉角的路徑
            Vector3 ToLastCorner = point1 - point2;
            //在獲取目標到轉角的向量
            Vector3 TargetToCorner = target.transform.position - point1;
            //如果兩向量夾角太小，距離太近，就不會過第二個轉角
            if (Vector2.Angle(
                    new Vector2(ToLastCorner.x, ToLastCorner.z),
                    new Vector2(TargetToCorner.x, TargetToCorner.z)) < 30
                && Vector3.Distance(point1, point2) < 1)
            {
                myAgent.SetDestination(point1);
                Debug.Log("nextcorner");
            }
            else
            {
                myAgent.SetDestination(point2);
                Debug.Log("i wont go there");
            }
        }

        protected Vector3 takecover()
        {
            NavMeshPath path = new NavMeshPath();
            myAgent.CalculatePath(target.transform.position, path);
            int i = path.corners.Length;
            while ( i-3 > 0)
            {
                Vector3 point1 = path.corners[i - 2];
                Vector3 point2 = path.corners[i - 3];
                Vector3 ToLastCorner = point1 - point2;
                Vector3 TargetToCorner = target.transform.position - point1;
                if (Vector2.Angle(
                        new Vector2(ToLastCorner.x, ToLastCorner.z),
                        new Vector2(TargetToCorner.x, TargetToCorner.z)) > 45
                    && Vector3.Distance(point1, point2) > .5)
                {
                    //myAgent.SetDestination(point1);
                    return point1;
                }
                else
                {
                    i--;
                }
            }
            return path.corners[path.corners.Length - 3];
        }

        public virtual bool stun(ActionStatus actionStatus)
        {
            if (doOnlyOnce)
            {
                myAgent.enabled = false;
                doOnlyOnce = false;
                gun.NowWeapon.weapon.GetComponent<Collider>().enabled = false;
            }
            return true;
        }
        public virtual bool recover(ActionStatus actionStatus)
        {
            if (doOnlyOnce)
            {
                myAgent.enabled = true;
                doOnlyOnce = false;
            }
            return true;
        }

        #region FPSLikeMovement
        public void FPSLikeMovement(float moveSpeed,float rotSpeed)
        {
            if(Input.anyKey)
            {
                var dir = 
                    camera.transform.TransformDirection(Vector3.right*input.ad+Vector3.forward*input.ws);
                myAgent.velocity = Vector3.ClampMagnitude(dir.normalized*7f,moveSpeed);
            }
            var camPos = camera.transform.TransformDirection(Vector3.back);
            RotateTowardlerp(my.transform.position-camPos,rotSpeed);
        }

        public void FPSLikeMovement(float baseMultiper,float moveSpeed,float rotSpeed)
        {
            if(Input.anyKey)
            {
                var dir = 
                    camera.transform.TransformDirection(Vector3.right*input.ad+Vector3.forward*input.ws);
                myAgent.velocity = Vector3.ClampMagnitude(dir.normalized*baseMultiper,moveSpeed);
            }
            var camPos = camera.transform.TransformDirection(Vector3.back);
            RotateTowardlerp(my.transform.position-camPos,rotSpeed);
        }
        #endregion
    }


}
