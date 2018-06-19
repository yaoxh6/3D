using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class FirstSceneActionManager : SSActionManager
    {
        public void moveBoat(Boat boat)
        {
            CCMoveToAction action = CCMoveToAction.getAction(boat.getDestination(), boat.movingSpeed);
            this.addAction(boat.getGameobj(), action, this);
        }

        public void moveCharacter(Character characterCtrl, Vector3 destination)
        {
            Vector3 currentPos = characterCtrl.getPosition();
            Vector3 middlePos = currentPos;
            if (destination.y > currentPos.y)
            {
                middlePos.y = destination.y;
            }
            else
            {
                middlePos.x = destination.x;
            }
            SSAction action1 = CCMoveToAction.getAction(middlePos, characterCtrl.movingSpeed);
            SSAction action2 = CCMoveToAction.getAction(destination, characterCtrl.movingSpeed);
            SSAction seqAction = CCSequenceAction.getAction(1, 0, new List<SSAction> { action1, action2 });
            this.addAction(characterCtrl.getGameobj(), seqAction, this);
        }
    }