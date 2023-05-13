using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{

    void Awake()
    {
        Debug.Log("Awake");
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
