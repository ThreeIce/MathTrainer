using System;
using UnityEngine;
using System.Collections.Generic;

public class PointFinishMessage
{
    /// <summary>
    /// 知识点名
    /// </summary>
    public PointType ProblemPointType;
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
/// <summary>
/// 知识点类型
/// </summary>
public enum PointType{

}
/// <summary>
/// 知识点的扩展类
/// </summary>
public static class PointTypeExtention{
    /// <summary>
    /// 将知识点enum转换成对应的知识点名称
    /// </summary>
    public static string GetName(this PointType type){
        switch(type){

        }
    }
}