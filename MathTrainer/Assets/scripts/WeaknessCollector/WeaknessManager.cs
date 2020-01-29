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
    #endregion
    /// <summary>
    /// 是否是首次使用该软件
    /// </summary>
    public bool IsFirstTime{get=>PointsFinishMessage == null;}

    /// <summary>
    /// 各知识点生成的概率比（实在不知道怎么取名了）
    /// </summary>
    protected Dictionary<string,float> PointsRandomPercentage;
    /// <summary>
    /// 概率比总和
    /// </summary>
    protected float TotalRandomRatio = 0f;
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
    private void GetGenerators(){
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
    /// 知识点系统初始化
    /// </summary>
    protected void InitPointsRandomPercentage(){
        PointsRandomPercentage = new Dictionary<string, float>();
        for(int i = 0;i<EnablePoints.Count;i++){
            AddPointRandomPercentage(EnablePoints[i]);
        }
    }
    /// <summary>
    /// 在知识点生成概率占比中加入新的知识点
    /// </summary>
    protected void AddPointRandomPercentage(string pointType){
        float Percentage = CalculatePointPercentage(pointType);
        TotalRandomRatio += Percentage;
        PointsRandomPercentage.Add(pointType,Percentage);
    }
    /// <summary>
    /// 设置单个知识点的生成概率占比
    /// </summary>
    protected void SetPointRandomPercentage(string pointType){
        TotalRandomRatio -= PointsRandomPercentage[pointType];
        PointsRandomPercentage[pointType] = CalculatePointPercentage(pointType);
        TotalRandomRatio += PointsRandomPercentage[pointType];
    }
    /// <summary>
    /// 从生成概率比池子里头移除一个知识点
    /// </summary>
    protected void RemovePointRandomPercentage(string pointType){
        TotalRandomRatio -= PointsRandomPercentage[pointType];
        PointsRandomPercentage.Remove(pointType);
    }
    /// <summary>
    /// 计算单个知识点的生成概率占比应为多少
    /// </summary>
    protected float CalculatePointPercentage(string pointType){
        //单个知识点的生成概率占比最高值暂定为5
        const float MaxPercentage = 5f;
        if(!PointsFinishMessage.ContainsKey(pointType)){
            return MaxPercentage;
        }
        PointFinishMessage pointMsg = PointsFinishMessage[pointType];
        if(pointMsg.LearnLevel < 1/ MaxPercentage){
            return MaxPercentage;
        }
        return 1 / pointMsg.LearnLevel;
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
        var generator = ChooseProblemGenerator();
        var problem = generator.GenerateProblem();
        BeginAnswering(problem);
        return problem;
    }
    /// <summary>
    /// 选择要生成的问题的类型
    /// </summary>
    /// <returns></returns>
    protected ProblemGenerator ChooseProblemGenerator(){
        string point = ChoosePoint();
        var generators = PointsAndGenerators[point];
        return generators[UnityEngine.Random.Range(0,generators.Length)];
    }
    /// <summary>
    /// 选择要生成的题目所属的知识点
    /// </summary>
    /// <returns></returns>
    protected string ChoosePoint(){
        float RandomNum = UnityEngine.Random.Range(0f,TotalRandomRatio);
        foreach(var point in PointsRandomPercentage){
            //通过减完判断是否小于等于零来识别随机数落到了哪个知识点的生成范围
            RandomNum -= point.Value;
            if(RandomNum <= 0){
                return point.Key;
            }
        }
        throw new Exception("随机数落到范围区间外去了");
    }
    /// <summary>
    /// 开始答题
    /// </summary>
    protected virtual void BeginAnswering(Problem p){
        p.ProblemFinishListener += EndAnswering;
    }
    /// <summary>
    /// 停止答题（题目完成回调）
    /// </summary>
    protected virtual void EndAnswering(Problem p,bool IsCorrect){
        var problemGenerator = p.Generator;
        var pointType = problemGenerator.pointType;
        //保存题目完成记录
        PointFinishMessage pointFinishMessage;
        if(PointsFinishMessage.ContainsKey(pointType)){
            pointFinishMessage = PointsFinishMessage[pointType];
        }else{//如果这种题目从来没有答过（即知识点完成信息列表里头没有这个知识点）就加进去
            pointFinishMessage = new PointFinishMessage(){
                PointType = pointType
            };
            PointsFinishMessage.Add(pointType, pointFinishMessage);
        }
        ProblemFinishMessage problemFinishMessage = new ProblemFinishMessage(){
            ProblemTypeName = problemGenerator.ProblemTypeName,
            BestFinishTime = problemGenerator.BestFinishTime,
            FinishTime = IsCorrect ? p.FinishTime : 0f
        };
        pointFinishMessage.Records.Add(problemFinishMessage);
        SaveData();
    }
    /// <summary>
    /// 将一个知识点纳入生成范围
    /// </summary>
    /// <param name="pointType"></param>
    public virtual void EnablePoint(string pointType){
        if(EnablePoints.Contains(pointType)){
            throw new ArgumentException("不要将两个相同的知识点纳入生成范围！！！");
        }
        EnablePoints.Add(pointType);
        AddPointRandomPercentage(pointType);
        SaveData();
    }
    /// <summary>
    /// 将一个知识点移出生成范围
    /// </summary>
    public virtual void DisablePoint(string pointType){
        EnablePoints.Remove(pointType);
        RemovePointRandomPercentage(pointType);
        SaveData();
    }
}
