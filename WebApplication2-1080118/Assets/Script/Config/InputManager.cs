using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Config
{
    public class InputManager:MonoBehaviour
    {
        public float ws;
        public float ad;

        public float charge;//蓄力

        public Animator Animator;

        void Start()
        {
            Animator = gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            ws = Input.GetAxis("ws");
            ad = Input.GetAxis("ad");

            
            if (ws*ws+ad*ad != 0)
            {
                Animator.SetBool("input_move",true);
            }
            else
            {
                Animator.SetBool("input_move", false);
            }

            if (Input.GetButtonDown("Jump"))
            {
                Animator.SetTrigger("avatermain_jump");
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Animator.SetBool("input_fire", true);
                Animator.SetTrigger("input_melee");
            }

            if (Input.GetButtonUp("Fire1"))
            {
                Animator.SetFloat("input_charge", charge);
                Animator.SetBool("input_fire", false);
                charge = 0;
            }

            if (Input.GetButton("Fire1"))
            {
                charge += Time.deltaTime;
            }

            Animator.SetFloat("input_ws", ws);
            Animator.SetFloat("input_ad", ad);
            /*
            if(Input.GetButton("skill1"))
            {
                Animator.SetTrigger("input_skill1");
            }
            if(Input.GetButton("dodge"))
            {
                Animator.SetTrigger("input_dodge");
            }
            */
        }
    }
}
