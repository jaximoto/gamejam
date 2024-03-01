using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField] private fov _fov;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spot = new Vector3(-.2f, .7f, 0) + transform.position;
        _fov.SetOrigin(spot);
        _fov.SetAimDirection(transform.up);
    }
    
}
