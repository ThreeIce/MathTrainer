using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    public RectTransform rect;
    public int designWidth = 9;
    public int designHeight = 16;
    public float designScale {get => designWidth/designHeight;}
    public float scaleRate {get => (float)Screen.width/(float)Screen.height;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"W:{Screen.width},H:{Screen.height}");
        if(scaleRate < designScale){
            //即Height的比例大于Width的比例，向Width靠拢
            float Rate = Screen.width / rect.localScale.x;
            var localScale = rect.localScale;
            localScale.x = Screen.width;
            localScale.y *= Rate;
            rect.localScale = localScale;
        }else{
            //反之向Height靠拢
            float Rate = Screen.height / rect.localScale.y;
            var localScale = rect.localScale;
            localScale.x *= Rate;
            localScale.y = Screen.height;
            rect.localScale = localScale;
        }
        Debug.Log($"MW:{rect.localScale.x},MH:{rect.localScale.y}");
    }
}
