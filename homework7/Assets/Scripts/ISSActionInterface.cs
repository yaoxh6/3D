using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISSActionCallback
{
    void SSActionEvent(SSAction source, int intParam = 0, GameObject objectParam = null);
}
