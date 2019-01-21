using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.ActionControl;
using Assets.Script.Avater;
using Assets.Script.weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.AIGroup
{
    class AICommander:MonoBehaviour
    {
        private GameObject[] allMyAi;
        private GameObject target;

        public bool IsAwake;

        public NavGrid NavGrid = new NavGrid();
        private ActionStatusDictionary actionStatusDictionary = new ActionStatusDictionary();
        private ActionBasicBuilder actionConstructer = new ActionBasicBuilder();
        private AIConstructer aiConstructer = new AIConstructer();
        private WeaponFactory weaponFactory = new WeaponFactory();

        void Awake()
        {
            //初期化
            allMyAi = GameObject.FindGameObjectsWithTag("AI");
            target = GameObject.Find("UnityChan");
            weaponFactory.Init();
            foreach (GameObject ai in allMyAi)
            {
                var aimain = ai.gameObject.GetComponent<AINodeBase>();
                aimain.AiBase = aiConstructer.GetAI();
                aimain.AiBase.Init(ai, target);

                aimain.Init_Avater();               
            }
        }

        void Start()
        {
            //是否有巡邏路線
            NavGrid.GetGrid();
        }

        void Update()
        {
            //調整時間後再重新思考
            //StartCoroutine(ChangeTactic(10000));
        }

        void OnDrawGizmos()
        {
            //NavGrid.DrawGizmos();
        }

        IEnumerator ChangeTactic(float waittime)
        {
            //print("Change!");
            for (int i = 0; i < allMyAi.Length; i++)
            {
                if (!IsAwake)
                {
                    var nodeAi = allMyAi[i].GetComponent<AINodeBase>();
                    nodeAi.formationPoint = gameObject.transform.TransformVector(Vector3.right*i);//offsetPoint
                }

                if (IsAwake)
                {
                    var RandPoint = RandomPosition(
                                        target.transform.position,
                                            Vector2.Distance(
                                                new Vector2(transform.position.x,transform.position.z),
                                                new Vector2(target.transform.position.x,target.transform.position.z)
                                            )
                                        );
                        
                    allMyAi[i].GetComponent<NavMeshAgent>().SetDestination(RandPoint);
                }
            }
            yield return new WaitForSeconds(waittime);
        }

        public Vector3 RandomPosition(Vector3 origin, float radius)
        {
            //var randDirection = UnityEngine.Random.insideUnitSphere * radius;
            var randDirection = UnityEngine.Random.onUnitSphere * radius;
            randDirection += origin;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randDirection, out navHit, radius, -1);
            return navHit.position;
        }
    }
}
