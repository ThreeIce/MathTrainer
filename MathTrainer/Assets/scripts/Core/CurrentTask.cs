using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// 代表当前用户正在使用的功能，如题目、算学小游戏等
/// </summary>
public abstract class CurrentTask
{
    /// <summary>
    /// 任务完成时调用
    /// </summary>
    /// <param name="args">完成参数</param>
    public virtual void Finish(params object[] args){
        FinishListener(this,args);
    }
    /// <summary>
    /// 任务完成回调
    /// </summary>
    public Action<CurrentTask,object[]> FinishListener;
}