using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ejemplo : MonoBehaviour
{
    public float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + horizontal);
    }
}
