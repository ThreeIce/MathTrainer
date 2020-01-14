using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

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
    public abstract string PointType{ get;}
    /// <summary>
    /// 生成题目
    /// </summary>
    /// <returns>题目</returns>
    public abstract Problem GenerateProblem();
}
/// <summary>
/// 题目的基类
/// </summary>
public abstract class Problem : CurrentTask
{
    /// <summary>
    /// 题目的描述
    /// </summary>
    /// <value></value>
    public abstract string ProblemDescription { get;}
    /// <summary>
    /// 检测答案是否正确
    /// </summary>
    /// <param name="Answer">用户输入的答案</param>
    /// <returns>是否正确（废话）</returns>
    public abstract bool IsCorrect(params string[] Answer);
}
