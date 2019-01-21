using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Avater;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.weapon
{
    class BulletClass:MonoBehaviour
    {        
        public int damage;
        public float blast;

        void Start()
        {
            var frontTransformDirection = gameObject.transform.TransformDirection(Vector3.forward);
            gameObject.GetComponent<Rigidbody>().AddForce(frontTransformDirection.normalized*100);
        }
        
        void OnCollisionEnter(Collision collision)
        {
            if (collision.rigidbody != null && collision.gameObject.tag != tag && collision.gameObject.GetComponent<AvaterMain>())
            {
                var hit = collision.gameObject;
                //執行"被打中"
                hit.GetComponent<AvaterMain>().OnHit();
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {        
            if (blast > 0)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, blast);
                foreach (Collider hit in colliders)
                {
                    var rb = hit.attachedRigidbody;
                    if (rb != null)
                        rb.AddExplosionForce(5f, transform.position, blast);
                }
            }        
        }
    }
}
