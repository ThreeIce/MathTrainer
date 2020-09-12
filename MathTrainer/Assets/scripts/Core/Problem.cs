using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Collections;
using System.Diagnostics;

/// <summary>
/// 题目生成器的基类
/// </summary>
/// <typeparam name="T">要生成的题目的类型</typeparam>
public abstract class ProblemGenerator
{
    /// <summary>
    /// 题目类型名
    /// </summary>
    public abstract string ProblemTypeName { get;}
    /// <summary>
    /// 题目所属知识点名
    /// </summary>
    public abstract string pointType{ get;}
    /// <summary>
    /// 最佳完成题目的时间
    /// </summary>
    /// <value></value>
    public abstract float BestFinishTime{get;}
    /// <summary>
    /// 生成题目
    /// </summary>
    /// <returns>题目</returns>
    public Problem GenerateProblem(){
        var result = GenerateProblemInternal();
        result.Generator = this;
        return result;
    }
    protected abstract Problem GenerateProblemInternal();
}
/// <summary>
/// 题目的基类
/// </summary>
public abstract class Problem : CurrentTask
{
    public ProblemGenerator Generator;
    /// <summary>
    /// 题目的描述
    /// </summary>
    public abstract string ProblemDescription { get;}
    /// <summary>
    /// 检测答案是否正确
    /// </summary>
    /// <param name="Answer">用户输入的答案</param>
    /// <returns>是否正确（废话）</returns>
    public abstract bool IsCorrect(params string[] Answers);
    /// <summary>
    /// 题目的答案
    /// </summary>
    public abstract string[] Answers { get; }
    
    /// <summary>
    /// 题目的完成回调
    /// </summary>
    public Action<Problem,bool> ProblemFinishListener;
    /// <summary>
    /// 当题目放弃时调用
    /// </summary>
    public Action<Problem> OnGiveUp;
    /// <summary>
    /// 输入答案
    /// </summary>
    public virtual bool InputAnswer(params string[] Answers){
        if(IsCorrect(Answers)){
            ProblemFinish(true);
            return true;
        }else{
            ProblemFinish(false);
            return false;
        }
    }
    /// <summary>
    /// Problem调用Task里Finish的参数转换接口
    /// </summary>
    public void ProblemFinish(bool IsRight){
        base.Finish();
        ProblemFinishListener(this, IsRight);
        ProblemFinishListener = null;
        OnGiveUp = null;
    }
    /// <summary>
    /// 放弃完成该题目
    /// </summary>
    public void GiveUp(){
        OnGiveUp?.Invoke(this);
        base.Finish();
        ProblemFinishListener = null;
        OnGiveUp = null;
    }
}





/// <summary>
/// 知识点类型名称统一列表
/// </summary>
public class PointType{
    public static readonly string Arithmetic = "四则运算";
    public static readonly string YiYuanYiCi = "一元一次方程";
    public static readonly string ErYuanYiCi = "二元一次方程";
    public static readonly string YiYuanErCi = "一元二次方程";
    public static readonly string YinShiFenJie = "因式分解";
}
