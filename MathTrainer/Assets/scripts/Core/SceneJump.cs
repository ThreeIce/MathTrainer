using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 各场景加载器
/// </summary>
public class SceneJump : MonoBehaviour
{
    //自动跳转顺便在这写了
    void Start(){
        if(WeaknessManager.main.IsFirstTime){
            WeaknessManager.main.EnablePoint(PointType.Arithmetic);
            OpenPointPage();
        }
    }
    /// <summary>
    /// 加载回答问题页面
    /// </summary>
    public void OpenProblemPage(){
        SceneManager.LoadScene("ProblemScene");
    }
    /// <summary>
    /// 加载主页面
    /// </summary>
    public void OpenMainPage(){
        SceneManager.LoadScene("MainScene");
    }
    /// <summary>
    /// 加载知识点选择页面
    /// </summary>
    public void OpenPointPage(){
        SceneManager.LoadScene("PointChoose");
    }
    /// <summary>
    /// 加载薄弱知识点显示页面
    /// </summary>
    public void OpenWeaknessView(){
        SceneManager.LoadScene("WeaknessView");
    }
}
