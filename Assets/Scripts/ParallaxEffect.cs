using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    //speed movement
    public float parallaxEffect;

    private Transform cameraPos;
    private Vector3 cameraLastPosition;

    // Start is called before the first frame update
    private void Start()
    {
        cameraPos = Camera.main.transform;
        cameraLastPosition = cameraPos.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 backgroundMove = cameraPos.position - cameraLastPosition;
        transform.position += new Vector3(backgroundMove.x * parallaxEffect, backgroundMove.y, 0);
        cameraLastPosition = cameraPos.position;
    }
}
