using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 知识点掌握信息
/// </summary>
[Serializable]
public class PointFinishMessage
{
    /// <summary>
    /// 知识点名
    /// </summary>
    public string ProblemPointType;
    /// <summary>
    /// 知识点的掌握程度，范围0~1
    /// </summary>
    /// <value></value>
    public float LearnLevel{get;}
    /// <summary>
    /// 所属题目的完成情况
    /// </summary>
    public List<ProblemFinishMessage> Records;
}
/// <summary>
/// 题目完成信息
/// </summary>
[Serializable]
public class ProblemFinishMessage{
    public string ProblemTypeName;
    public float FinishTime;
    public float BestFinishTime;
}