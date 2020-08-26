using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class diaodaim : MonoBehaviour
{
    public Text DescriptionText;
    public Text AnswerText;
    public Button Next;
    public Button Finish;
    Problem problem;
    Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.main;
    }
    //生成一个题目
    void Generate(){
        problem = manager.GenerateProblem();
        DescriptionText.text = problem.ProblemDescription;
        problem.Start();

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Answer(){
        if(problem.InputAnswer(AnswerText.text)){
            WhenCorrect();
        }else{
            WhenWrong();
        }
        Next.interactable = true;
        Finish.interactable = false;
        problem = null;
    }
    void WhenCorrect(){
        DescriptionText.text ="答对了，用时" + problem.FinishTime + "秒";
    }
    void WhenWrong(){
        DescriptionText.text = "回答错误，答案如下\n";
        for(int i = 0;i<problem.Answers.Length;i++){
            DescriptionText.text += $"答案项{i}为：{problem.Answers[i]}\n";
        }
    }
    public void Click()
    {
        Generate();
        Next.interactable = false;
        Finish.interactable = true;
    }
    void OnDestroy(){
        problem?.GiveUp();
        problem = null;
    }

}
