using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLight : MonoBehaviour
{
    public Transform lightPosition;
    // Update is called once per frame
    void Update()
    {
        transform.position = lightPosition.position;
    }
}
