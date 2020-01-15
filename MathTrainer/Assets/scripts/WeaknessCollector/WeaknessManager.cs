using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

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
    protected ProblemGenerator[] Generators;
    #region 需要储存的成员
    /// <summary>
    /// 知识点列表，bool表示是否加入题目生成考虑范围
    /// </summary>
    public Dictionary<PointType,bool> EnablePoints{get;private set;}
    #endregion
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
    /// <summary>
    /// 通过反射获取所有的ProblemGenerator并生成一个实例
    /// </summary>
    void GetGenerators(){
        Assembly temp = Assembly.GetExecutingAssembly();
        var types = temp.GetTypes();
        var generatortype = typeof(ProblemGenerator);
        List<ProblemGenerator> generators = new List<ProblemGenerator>();
        for(int i = 0;i<types.Length;i++){
            //检测搜索到的类型是否是继承自ProblemGenerator的（并且该类型不能为ProblemGenerator本身）
            if(generatortype.IsAssignableFrom(types[i]) && !types[i].Equals(generatortype)){
                generators.Add((ProblemGenerator)Activator.CreateInstance(types[i]));
            }
        }
        Generators = generators.ToArray();
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
    /// <summary>
    /// 将一个知识点纳入生成范围
    /// </summary>
    /// <param name="pointType"></param>
    public virtual void EnablePoint(PointType pointType){

    }
    /// <summary>
    /// 将一个知识点移出生成范围
    /// </summary>
    /// <param name="pointType"></param>
    public virtual void DisablePoint(PointType pointType){

    }
}
