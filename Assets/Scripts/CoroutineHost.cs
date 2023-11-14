using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHost : MonoBehaviour
{
    // singleton
    public static CoroutineHost Instance
    {
        get
        {
            // try to find if instance exists
            if (m_Instance == null)
            {
                m_Instance = (CoroutineHost)FindObjectOfType(typeof(CoroutineHost));

                // if instance doesn't exist, we create a new instance
                if (m_Instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "CoroutineHost";
                    m_Instance = go.AddComponent<CoroutineHost>();
                }
                DontDestroyOnLoad(m_Instance.gameObject);
            }

            return m_Instance;
        }
    }

    private static CoroutineHost m_Instance = null;
}
