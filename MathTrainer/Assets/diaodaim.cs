using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class diaodaim : MonoBehaviour
{
    public Text DescriptionText;
    public Text AnswerText;
    Problem timu;
    Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.main;
    }
    //生成一个题目
    void Generate(){
        timu = manager.GenerateProblem();
        DescriptionText.text = timu.ProblemDescription;
        timu.Start();
        Debug.Log("Click");
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Answer(){
        if(timu.InputAnswer(AnswerText.text)){
            WhenCorrect();
        }else{
            WhenWrong();
        }
    }
    void WhenCorrect(){
     DescriptionText.text ="you are good";
    }
    void WhenWrong(){
     DescriptionText.text = "you're wrong";
    }
    public void Click()
    {
        Generate();
        Debug.Log("Click");
    }

}
