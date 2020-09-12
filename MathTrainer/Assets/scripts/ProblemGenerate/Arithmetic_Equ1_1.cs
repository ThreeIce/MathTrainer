using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using calculate;

public class Arithmetic_Equ1_1Generator : ProblemGenerator
{
    /// <summary>
    /// 题目类型名
    /// </summary>
    public override string ProblemTypeName { get { return "一元一次方程"; } }
    /// <summary>
    /// 题目所属知识点名
    /// </summary>
    public override string pointType { get { return PointType.YiYuanYiCi; } }
    /// <summary>
    /// 最佳完成题目的时间
    /// </summary>
    /// <value></value>
    public override float BestFinishTime { get { return 15; } }
    /// <summary>
    /// 生成题目
    /// </summary>
    /// <returns>题目</returns>
    protected override Problem GenerateProblemInternal()
    {
        Arithmetic_Equ1_1 p = new Arithmetic_Equ1_1();
        return p;
    }
}
public class Arithmetic_Equ1_1 : Problem
{
    string[] Q = new CreateProblem4().GetEquation();

    public override string ProblemDescription { get { return "请计算：" + Q[0]; } }

    public override string[] Answers { get { return new string[] { Q[1] }; } }

    public override bool IsCorrect(params string[] Answers)
    {

        return Answers.Length == 1 && Answers[0] == this.Answers[0];

    }
}
