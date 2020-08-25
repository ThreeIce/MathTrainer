using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaknessPanel : MonoBehaviour
{
    
    [UnityEngine.Tooltip("Important")]
    [SerializeField]
    private GameObject VeryGoodPrefab;
    [SerializeField]
    private GameObject GoodPrefab;
    [SerializeField]
    private GameObject BadPrefab;
    [SerializeField]
    private GameObject VeryGood;
    [SerializeField]
    private GameObject Good;
    [SerializeField]
    private GameObject Bad;
    [SerializeField]
    private Transform VeryGoodContent;
    [SerializeField]
    private Transform GoodContent;
    [SerializeField]
    private Transform BadContent;
    private WeaknessManager wmanager;
    
    // Start is called before the first frame update
    void Start()
    {
        VeryGood = GameObject.Find("VeryGood");
        Good = GameObject.Find("Good");
        Bad = GameObject.Find("Bad");

        VeryGoodContent = GetComponentInChildren<GridLayoutGroup>().transform;
        GoodContent = GetComponentInChildren<GridLayoutGroup>().transform;
        BadContent = GetComponentInChildren<GridLayoutGroup>().transform;

        wmanager = WeaknessManager.main;
        Init();
    }
    /// <summary>
    /// 初始化
    /// </summary>
    void Init(){
        var points = wmanager.PointsFinishMessage;
        foreach (var point in points){
            var finishMessage = point.Value;
            var level = finishMessage.LearnLevel;
            if(level >= 0.8f){
                var go = Instantiate(VeryGoodPrefab,VeryGoodContent);
                var controller = go.GetComponent<WeaknessController>();
                controller.Init(point.Key, level);
            }else if(level >= 0.6f){
                var go = Instantiate(GoodPrefab,GoodContent);
                var controller = go.GetComponent<WeaknessController>();
                controller.Init(point.Key,level);
            }else{
                var go = Instantiate(BadPrefab,BadContent);
                var controller = go.GetComponent<WeaknessController>();
                controller.Init(point.Key,level);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
