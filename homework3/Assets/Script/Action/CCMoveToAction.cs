using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CCMoveToAction : SSAction
    {
        public Vector3 target;
        public float speed;

        private CCMoveToAction() { }
        public static CCMoveToAction getAction(Vector3 target, float speed)
        {
            CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();
            action.target = target;
            action.speed = speed;
            return action;
        }

        public override void Update()
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
            if (this.transform.position == target)
            {
                this.destroy = true;
                this.callback.actionDone(this);
            }
        }

        public override void Start()
        {
            //
        }

    }