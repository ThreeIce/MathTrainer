using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// 代表当前用户正在使用的功能，如题目、算学小游戏等
/// </summary>
public abstract class CurrentTask
{

    public virtual void Finish(){
        FinishListener(this);
    }
    public Action<CurrentTask> FinishListener;
}