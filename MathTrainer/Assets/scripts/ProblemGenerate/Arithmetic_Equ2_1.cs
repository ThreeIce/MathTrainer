﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using calculate;

public class Arithmetic_Equ2_1Generator : ProblemGenerator
{
    /// <summary>
    /// 题目类型名
    /// </summary>
    public override string ProblemTypeName { get { return "二元一次方程"; } }
    /// <summary>
    /// 题目所属知识点名
    /// </summary>
    public override string pointType { get { return PointType.ErYuanYiCi; } }
    /// <summary>
    /// 最佳完成题目的时间
    /// </summary>
    /// <value></value>
    public override float BestFinishTime { get { return 12; } }
    /// <summary>
    /// 生成题目
    /// </summary>
    /// <returns>题目</returns>
    protected override Problem GenerateProblemInternal()
    {
        Arithmetic_Equ2_1 p = new Arithmetic_Equ2_1();
        return p;
    }
}
public class Arithmetic_Equ2_1 : Problem
{
    string[] Q = new CreateProblem6().GetEquation();

    public override string ProblemDescription { get { return "请计算：" + Q[0]; } }

    public override string[] Answers { get { return new string[] { Q[1],Q[2] }; } }

    public override bool IsCorrect(params string[] Answers)
    {
        return Answers.Length == 2 && Answers[0] == this.Answers[0] && Answers[1] == this.Answers[1];
    }
}
