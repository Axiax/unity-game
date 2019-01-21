using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Avater;
using UnityEngine;

namespace Assets.Script.weapon
{
    class MeleeClass:MonoBehaviour
    {
        public int basedamage;
        public int buff;

        void Start()
        {

        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.rigidbody != null && collision.gameObject.tag != tag &&
                collision.gameObject.GetComponent<AvaterMain>())
            {
                var hit = collision.gameObject;
                //執行"被打中"
                hit.GetComponent<AvaterMain>().OnHit();
            }
        }
    }
}
