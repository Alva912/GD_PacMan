using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDestroy : MonoBehaviour
{
    [SerializeField] private float delay, timer;

    private void Awake()
    {
        timer = 0;
    }
    private void Update()
    {
        // NOTE No matter time scale is 0 or not
        timer += Time.unscaledDeltaTime;
        if (timer > delay)
        {
            Destroy(this.gameObject);
        }
        // Debug.Log(timer);
    }
}
