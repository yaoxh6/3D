using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CCSequenceAction : SSAction, ISSActionCallback
    {
        public List<SSAction> sequence;
        public int repeat = 1;
        public int currentActionIndex = 0;

        public static CCSequenceAction getAction(int repeat, int currentActionIndex, List<SSAction> sequence)
        {
            CCSequenceAction action = ScriptableObject.CreateInstance<CCSequenceAction>();
            action.sequence = sequence;
            action.repeat = repeat;
            action.currentActionIndex = currentActionIndex;
            return action;
        }

        public override void Update()
        {
            if (sequence.Count == 0) return;
            if (currentActionIndex < sequence.Count)
            {
                sequence[currentActionIndex].Update();
            }
        }

        public void actionDone(SSAction source)
        {
            source.destroy = false;
            this.currentActionIndex++;
            if (this.currentActionIndex >= sequence.Count)
            {
                this.currentActionIndex = 0;
                if (repeat > 0) repeat--;
                if (repeat == 0)
                {
                    this.destroy = true;
                    this.callback.actionDone(this);
                }
            }
        }

        public override void Start()
        {
            foreach (SSAction action in sequence)
            {
                action.gameObject = this.gameObject;
                action.transform = this.transform;
                action.callback = this;
                action.Start();
            }
        }

        void OnDestroy()
        {
            foreach (SSAction action in sequence)
            {
                DestroyObject(action);
            }
        }
    }
