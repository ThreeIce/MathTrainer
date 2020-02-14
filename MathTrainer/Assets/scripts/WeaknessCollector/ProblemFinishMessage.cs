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
    public string PointType;
    /// <summary>
    /// 知识点的掌握程度，范围0~1
    /// </summary>
    /// <value></value>
    //按近五十次算
    public float LearnLevel{
        get{
            float result = 0f;
            if(Records.Count < 50){
                for(int i = 0; i < Records.Count;i++){
                    result += Records[i].LearnLevel;
                }
                result /= Records.Count;
            }else{
                for(int i = 0; i < 50;i++){
                    result += Records[i].LearnLevel;

                }
                result /= 50;
            }
            return result > 1f ? 1f : result; 
        }
    }
    /// <summary>
    /// 所属题目的完成情况
    /// </summary>
    public List<ProblemFinishMessage> Records = new List<ProblemFinishMessage>();
}
/// <summary>
/// 题目完成信息
/// </summary>
[Serializable]
public class ProblemFinishMessage{
    /// <summary>
    /// 题目的类型名称
    /// </summary>
    public string ProblemTypeName;
    /// <summary>
    /// 用户完成这道题的时间，错了算零
    /// </summary>
    public float FinishTime;
    /// <summary>
    /// 题目的最佳完成时间（考虑到搜索效率，干脆存了算了）
    /// </summary>
    public float BestFinishTime;
    /// <summary>
    /// 单次掌握情况
    /// </summary>
    //这里不考虑溢出1，因为大于1的部分可以跟别的失误小于1的部分互补
    public float LearnLevel{
        get{ 
            if(FinishTime == 0)
                return 0;
            else
                return BestFinishTime/FinishTime;
            
        }
    }
}