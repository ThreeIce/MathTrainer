using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arithmetic_AddGenrator : ProblemGenerator{
    /// <summary>
    /// 题目类型名
    /// </summary>
    public override string ProblemTypeName { get{return "加法运算";}}
    /// <summary>
    /// 题目所属知识点名
    /// </summary>
    public override string pointType{ get{ return PointType.Arithmetic; }}
    /// <summary>
    /// 最佳完成题目的时间
    /// </summary>
    /// <value></value>
    public override float BestFinishTime{get{return 10;}}
    /// <summary>
    /// 生成题目
    /// </summary>
    /// <returns>题目</returns>
    public override Problem GenerateProblem(){
        return null;
    }
}
public class Arithmetic_Add : Problem
{
    public override string ProblemDescription {get{return A + "+" + B;}}
    public override string[] Answers {get{return new string[]{Answer.ToString()};}}
    public override bool IsCorrect(params string[] Answers){
        return Answers.Length == 1 && Answers[0] == this.Answers[0];
    }
    public int A;
    public int B;
    public int Answer;
}
