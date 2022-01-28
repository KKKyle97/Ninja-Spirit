using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewScript : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos = 0;
    float[] pos;
    private float stableValue;
    private Vector3 stableSize;
    private bool isStable, isStableSize;

    // Start is called before the first frame update
    void Start()
    {
        stableValue = float.MinValue;
        stableSize = new Vector3(0,0,0);
        isStable = isStableSize = false;
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            isStable = isStableSize = false;
            
        }
        else if (isStable == false)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                    if (stableValue != scrollbar.GetComponent<Scrollbar>().value)
                    {
                        stableValue = scrollbar.GetComponent<Scrollbar>().value;
                    }
                    else
                    {
                        isStable = true;
                    }
                }
            }
        }

        if (isStableSize == false)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);
                    if (stableSize != transform.GetChild(i).localScale)
                    {
                        stableSize = transform.GetChild(i).localScale;
                    }
                    else
                    {
                        isStableSize = true;
                    }

                    for (int j = 0; j < pos.Length; j++)
                    {
                        if (j != i)
                        {
                            transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        }
                    }
                }
            }
        }
        
    }
}