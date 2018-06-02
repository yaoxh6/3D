using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUI : MonoBehaviour {

    public RectTransform rectBloodPos;
    public RectTransform blood;
    public int reduceBlood = 0;
    void Update()
    {
        this.gameObject.transform.Translate(Input.GetAxis("Horizontal") * 10 * Time.deltaTime, 0, 0);
        this.gameObject.transform.Translate(0, 0, Input.GetAxis("Vertical") * 10 * Time.deltaTime);

        Vector2 vec2 = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        blood.GetComponent<RectTransform>().Right(reduceBlood);
        rectBloodPos.anchoredPosition = new Vector2(vec2.x - Screen.width / 2 + 0, vec2.y - Screen.height / 2 + 60);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 30, 40, 20), "加血"))
        {
            reduceBlood -= 10;
            if(reduceBlood < 0)
            {
                reduceBlood = 0;
            }
        }
        if (GUI.Button(new Rect(70, 30, 40, 20), "减血"))
        {
            reduceBlood += 10;
            if(reduceBlood > 200)
            {
                reduceBlood = 200;
            }
        }
    }
}
