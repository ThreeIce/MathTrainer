using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WeaknessController : MonoBehaviour
{
    [SerializeField]
    private Text PointTypeName;
    [SerializeField]
    private Text PercentText;
    [SerializeField]
    private RectTransform Image;
    public int Level;
    // Start is called before the first frame update
    void Awake()
    {
        var texts = GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.Length; i++){
            if(texts[i].gameObject.name == "TypeName"){
                PointTypeName = texts[i];
            }else if(texts[i].gameObject.name == "Percent"){
                PercentText = texts[i];
            }
        }
        Image = this.gameObject.GetComponentsInChildren<RectTransform>().Where(x => x.gameObject.name == "Image").First();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(string PointType,float level){
        PointTypeName.text = PointType;
        Level = (int)(level * 100f);
        PercentText.text = Level + "%";
        var pos = Image.localPosition;
        pos.y -= 90f - Level * 0.9f;
        Image.localPosition = pos;
    }
}
