using System.Collections;
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
        //先将要测试的知识点纳入生成范围
        WeaknessManager wm = WeaknessManager.main;
        if(!wm.EnablePoints.Contains(PointType.Arithmetic)){
            wm.EnablePoint(PointType.Arithmetic);
        }
        //题目获取
        Manager manager = Manager.main;
        Problem p = manager.GenerateProblem();
        //加个完成回调
        p.ProblemFinishListener += (sender,IsRight) =>{
            Debug.Log("答案"+IsRight);
            Debug.Log("完成时间"+ sender.FinishTime);
        };
        //开始答题
        p.Start();
        Debug.Log("题目为" + p.ProblemDescription); 
        Debug.Log("答案为" + p.Answers[0]);
        //等待两秒
        yield return new WaitForSeconds(2);
        //输入结果
        p.InputAnswer(p.Answers);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
