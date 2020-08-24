using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    public WeaknessManager wmanager;
    //由于实例化要的是父对象的transform所以就不存GO了
    public Transform ContentObject;
    public GameObject PointPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //初始化
        wmanager = WeaknessManager.main;
        ContentObject = this.GetComponentInChildren<GridLayoutGroup>().transform;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 初始化Point列表
    /// </summary>
    public void Init(){
        //获得Point列表
        var points = wmanager.Points;
        for(int i = 0; i < points.Length; i++){
            //每个Point实例化一个GO
            GameObject go = GameObject.Instantiate<GameObject>(PointPrefab,ContentObject);
            //初始化PointController
            PointController controller = go.GetComponent<PointController>();
            controller.Init(points[i],wmanager.IsEnabled(points[i]));
        }
    }
    /// <summary>
    /// 回到主页面
    /// </summary>
    public void BackToMainPage(){
        Manager.main.OpenMainPage();
    }
}
