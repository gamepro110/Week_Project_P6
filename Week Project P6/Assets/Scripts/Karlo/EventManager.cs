using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void ResetLevelDelegate();

    public static event ResetLevelDelegate ResetLevelEvent;

    public static void ResetLevelFunc()
    {
        ResetLevelEvent();
    }
}