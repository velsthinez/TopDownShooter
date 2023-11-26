using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private RectTransform reticle;
    
    // Start is called before the first frame update
    void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (reticle == null)
            return;

        reticle.position = Input.mousePosition;
        
        if(Cursor.visible)
            Cursor.visible = false;

    }
}
