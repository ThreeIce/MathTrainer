﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 测试脚本，起示范作用，正式编程时把他从场景里删除
/// </summary>
public class testscript : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //题目获取
        Manager manager = Manager.main;
        Problem p = manager.GenerateProblem();
        //开始答题
        p.Start();
        Debug.Log("题目为" + p.ProblemDescription); 
        Debug.Log("答案为" + p.Answers[0]);
        //等待两秒
        yield return new WaitForSeconds(2);
        //输入结果
        if(p.InputAnswer()){
            //正确
        }else{
            //错误
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
