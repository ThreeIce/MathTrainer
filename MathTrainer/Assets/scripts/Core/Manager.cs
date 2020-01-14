using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//*****表示未完成 
//ThreeIce：UI那边获取题目后与输入交互，通过IsCorrect检测是否正确，如果正确直接调用Finish即可，不需要回来调用别的函数，一切功能由钩子处理
public class Manager : MonoBehaviour
{
    #region StaticMember
        
    /// <summary>
    /// 指向全局唯一的Manager
    /// </summary>
    public static Manager main;

    #endregion
    /// <summary>
    /// WeaknessManager的引用
    /// </summary>
    public WeaknessManager WeaknessMg;
    /// <summary>
    /// 当前正在进行的task，如果当前什么也没有进行就为null
    /// </summary>
    /// 
    public CurrentTask CurrentTask;
    /// <summary>
    /// 创建事件
    /// </summary>
    void Awake()
    {
        //如果不存在Manager实例，将main设为当前Manager，如果存在，报错
        if(main == null){
            main = this;
        }else{
            Debug.LogError("不能同时存在两个Manager！");
        }
    }
    /// <summary>
    /// 删除事件
    /// </summary>
    void Destroy(){
        main = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        WeaknessMg = WeaknessManager.main;
    }
    /// <summary>
    /// 生成题目
    /// </summary>
    /// <returns>题目对象</returns>
    public Problem GenerateProblem(){
        if(CurrentTask != null){
            throw new CurrentTaskException("未完成当前正在进行的任务，请勿开启下一个任务！");
        }
        var result = WeaknessMg.GenerateProblem();
        CurrentTask = result;
        CurrentTask.FinishListener += CurrentTaskFinish;
        return result;
    }

    /// <summary>
    /// 当前任务完成时回调 *******
    /// </summary>
    protected void CurrentTaskFinish(CurrentTask task){
        if(CurrentTask != task){
            throw new CurrentTaskException("当前进行中人物与当前任务完成回调对应的任务不同！（可能是当前任务为null）");
        }
        CurrentTask = null;
    }
}
public class CurrentTaskException : Exception
{
    public CurrentTaskException(string message) : base(message)
    {
    }
}
