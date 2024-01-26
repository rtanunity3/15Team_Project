using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundRotate : MonoBehaviour
{
    // 계속 회전
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * 3);
    }
}
