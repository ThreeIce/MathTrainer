using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// 薄弱点系统，同时也是题目选择系统
/// </summary>
public class WeaknessManager : MonoBehaviour
{
    #region StaticPath
    public static readonly string PointDataPath = Application.persistentDataPath + "/WeaknessData/PointData.dat";
    public static readonly string EnablePointsDataPath = Application.persistentDataPath + "/WeaknessData/EnablePoints";
    #endregion
    #region StaticMember
    /// <summary>
    /// 指向全局唯一的WeaknessManager
    /// </summary>
    public static WeaknessManager main;
    #endregion
    /// <summary>
    /// 知识点和题目类型的对应字典，string为题目所属的知识点
    /// </summary>
    /// <value></value>
    //ThreeIce:这类型套娃看的我都难受
    public Dictionary<string,ProblemGenerator[]> PointsAndGenerators{get;private set;}
    /// <summary>
    /// 所有知识点
    /// </summary>
    public string[] Points{get;private set;}
    /// <summary>
    /// 所有题目类型
    /// </summary>
    public ProblemGenerator[] Generators{get;private set;}
    #region 需要储存的成员
    /// <summary>
    /// 纳入生成考虑范围的知识点
    /// </summary>
    public List<string> EnablePoints{get;private set;}
    /// <summary>
    /// 各知识点的各题目的完成信息
    /// </summary>
    public Dictionary<string,PointFinishMessage> PointsFinishMessage{get;private set;}
    /// <summary>
    /// 是否是首次使用该软件
    /// </summary>
    public bool IsFirstTime{get=>PointsFinishMessage == null;}
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
        ReadData();
    }
    /// <summary>
    /// 通过反射获取所有的ProblemGenerator并生成一个实例
    /// </summary>
    void GetGenerators(){
        Assembly temp = Assembly.GetExecutingAssembly();
        var types = temp.GetTypes();
        var generatortype = typeof(ProblemGenerator);
        List<ProblemGenerator> generators = new List<ProblemGenerator>();
        List<string> points = new List<string>();
        Dictionary<string,List<ProblemGenerator>> pointsandgenerators = new Dictionary<string,List<ProblemGenerator>>();
        for(int i = 0;i<types.Length;i++){
            //检测搜索到的类型是否是继承自ProblemGenerator的（并且该类型不能为ProblemGenerator本身）
            if(generatortype.IsAssignableFrom(types[i]) && !types[i].Equals(generatortype)){
                //是的情况下就将该题目添加到三个索引器中
                ProblemGenerator generator = (ProblemGenerator)Activator.CreateInstance(types[i]);
                generators.Add(generator);
                //如果该知识点已存在，只添加到dic里的list，如果不存在，dic新建一项，并添加到points中
                if(pointsandgenerators.ContainsKey(generator.pointType)){
                    pointsandgenerators[generator.pointType].Add(generator);
                }else{
                    var array = new List<ProblemGenerator>();
                    array.Add(generator);
                    pointsandgenerators.Add(generator.pointType,array);
                    points.Add(generator.pointType);
                }
            }
        }
        //将临时储存同步到主内容去（其实是为了省内存，现在多干点活接下来占用小一点）
        Generators = generators.ToArray();
        Points = points.ToArray();
        foreach(var point_and_generator in pointsandgenerators){
            PointsAndGenerators.Add(point_and_generator.Key,point_and_generator.Value.ToArray());
        }
    }
    /// <summary>
    /// 从磁盘中读出数据
    /// </summary>
    protected void ReadData(){
        //如果数据文件不存在（或者损坏？）就设成null然后不管
        if( !(File.Exists(PointDataPath)
        && File.Exists(EnablePointsDataPath) )){
            PointsFinishMessage = null;
            EnablePoints = null;
            return;
        }
        //先读出PointData
        using(FileStream fs = new FileStream(PointDataPath, FileMode.Open, FileAccess.Read)){
            BinaryFormatter bf = new BinaryFormatter();
            PointsFinishMessage = (Dictionary<string,PointFinishMessage>)bf.Deserialize(fs);
        }
        //再读出EnablePoint数据
        using(FileStream fs = new FileStream(EnablePointsDataPath, FileMode.Open, FileAccess.Read)){
            BinaryFormatter bf = new BinaryFormatter();
            EnablePoints = (List<string>)bf.Deserialize(fs);
        }
    }
    /// <summary>
    /// 保存数据到磁盘
    /// </summary>
    protected void SaveData(){
        //先写PointData
        using(FileStream fs = new FileStream(PointDataPath, FileMode.OpenOrCreate,FileAccess.Write)){
            var bf = new BinaryFormatter();
            bf.Serialize(fs,PointsFinishMessage);
        }
        //再写EnablePointsData
        using(FileStream fs = new FileStream(EnablePointsDataPath, FileMode.OpenOrCreate,FileAccess.Write)){
            var bf = new BinaryFormatter();
            bf.Serialize(fs,EnablePoints);
        }
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
    public virtual void EnablePoint(string pointType){

    }
    /// <summary>
    /// 将一个知识点移出生成范围
    /// </summary>
    /// <param name="pointType"></param>
    public virtual void DisablePoint(string pointType){

    }
}
