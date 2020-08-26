using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 单个Point的管理器
/// </summary>
[AddComponentMenu("PointView/PointController")]
public class PointController : MonoBehaviour
{
    [UnityEngine.SerializeField]
    private Toggle toggle;
    [SerializeField]
    private Text text;
    public string PointType;
    public WeaknessManager wmanager;
    void Awake(){
        // 基本组件引用初始化
        toggle = this.GetComponent<Toggle>();
        text = this.GetComponentInChildren<Text>();
    }
    void Start(){
        //获取Manager引用
        wmanager = WeaknessManager.main;
    }
    /// <summary>
    /// 初始化PointControl
    /// </summary>
    /// <param name="pointName">对应的知识点的名字</param>
    public void Init(string pointType,bool IsEnabled){
        //初始化PointType信息
        PointType = pointType;
        text.text = pointType;
        //添加监听
        toggle.onValueChanged.AddListener(OnValueChanged);
        //将默认是否勾选设为IsEnabled的值
        toggle.SetIsOnWithoutNotify(IsEnabled);
    }
    public void OnValueChanged(bool value){
        if(value){
            wmanager.EnablePoint(PointType);
        }else{
            try{
                wmanager.DisablePoint(PointType);
            }
            catch(System.Exception e){
                //TODO:时间有限，做个简单的禁止去勾处理
                Debug.Log(e);
                toggle.SetIsOnWithoutNotify(true);
            }
        }
    }
}
