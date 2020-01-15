using System;
using UnityEngine;
using System.Collections.Generic;

public class PointFinishMessage
{
    /// <summary>
    /// 知识点名
    /// </summary>
    public string ProblemPointName;
    /// <summary>
    /// 知识点的掌握程度
    /// </summary>
    /// <value></value>
    public float LearnLevel{get;}
    public List<ProblemFinishMessage> Records;
}
public class ProblemFinishMessage{
    public string ProblemTypeName;
    public float FinishTime;
    public float BestFinishTime;
}