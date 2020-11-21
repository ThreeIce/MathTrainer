using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using calculate;

public class Arithmetic_Equ1_2Generator : ProblemGenerator
{
    /// <summary>
    /// 题目类型名
    /// </summary>
    public override string ProblemTypeName { get { return "一元二次方程"; } }
    /// <summary>
    /// 题目所属知识点名
    /// </summary>
    public override string pointType { get { return PointType.YiYuanErCi; } }
    /// <summary>
    /// 最佳完成题目的时间
    /// </summary>
    /// <value></value>
    public override float BestFinishTime { get { return 20; } }
    /// <summary>
    /// 生成题目
    /// </summary>
    /// <returns>题目</returns>
    protected override Problem GenerateProblemInternal()
    {
        Arithmetic_Equ1_2 p = new Arithmetic_Equ1_2();
        return p;
    }
}
public class Arithmetic_Equ1_2 : Problem
{
    string[] Q = new CreateProblem7().GetEquation(0);
    public override string ProblemDescription { get { return "请计算：" + Q[0]; } }

    public override string[] Answers {
         get { 
            var result = new string[Q.Length-1];
            for(int i = 0;i<result.Length;i++){
                result[i] = Q[i+1];
            }
            return result;
        }
    }

    public override bool IsCorrect(params string[] Answers)
    {
        Debug.Log(Answers.Length);
        if(this.Answers.Length == Answers.Length){
            for(int i = 0; i < Answers.Length;i++){
                if(this.Answers[i] != Answers[i]){
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}
