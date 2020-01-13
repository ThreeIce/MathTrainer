using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

/// <summary>
/// 薄弱点系统，同时也是题目选择系统
/// </summary>
public class WeaknessManager : MonoBehaviour
{
    #region StaticMember
    /// <summary>
    /// 指向全局唯一的WeaknessManager
    /// </summary>
    public static WeaknessManager main;
    #endregion
    public ProblemGenerator[] generators;
    /// <summary>
    /// 创建事件
    /// </summary>
    void Awake()
    {
        //如果不存在Manager实例，将main设为当前Manager，如果存在，报错
        if(main == null){
            main = this;
        }else{
            Debug.LogError("不能同时存在两个Manager！");
        }
        //获得所有的ProblemGenerator
        GetGenerators();
    }
    void GetGenerators(){
        Assembly temp = Assembly.GetExecutingAssembly();
    }
    /// <summary>
    /// 删除事件
    /// </summary>
    void Destroy(){
        main = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// 生成问题
    /// </summary>
    /// <returns>问题对象</returns>
    public Problem GenerateProblem(){
        return null;
    }
    /// <summary>
    /// 选择要生成的问题的类型
    /// </summary>
    /// <returns></returns>
    protected ProblemGenerator ChooseProblemGenerator(){
        return null;
    }

}
