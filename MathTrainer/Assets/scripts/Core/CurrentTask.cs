using System.Collections.Generic;
using System;
using UnityEngine;
using System.Diagnostics;
/// <summary>
/// 代表当前用户正在使用的功能，如题目、算学小游戏等
/// </summary>
public abstract class CurrentTask
{
    /// <summary>
    /// 任务完成时调用
    /// </summary>
    /// <param name="args">完成参数</param>
    public virtual void Finish(){
        FinishTimeWatch.Stop();
        FinishListener(this);
    }
    /// <summary>
    /// 任务完成回调
    /// </summary>
    public Action<CurrentTask> FinishListener;
    public float FinishTime{get{
        if(FinishTimeWatch.IsRunning){
            UnityEngine.Debug.LogError("Task尚未完成，完成后才能获取完成时间！");
        }
        return (float)FinishTimeWatch.Elapsed.TotalSeconds;
    }}
    public bool IsFinished {get{return !FinishTimeWatch.IsRunning;}}
    public Stopwatch FinishTimeWatch = new Stopwatch();
    public virtual void Start(){
        FinishTimeWatch.Start();
    }
}