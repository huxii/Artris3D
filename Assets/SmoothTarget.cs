using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothTarget : MonoBehaviour {

    public GameObject ARCamera;
    private float n1;
    private float n2;
    private float n3;
    private float x;
    private float y;
    private float z;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = ARCamera.transform.position;
        Vector3 angle = ARCamera.transform.eulerAngles;

        n1 = (pos.y / (Mathf.Tan(angle.x / 57.3f)));
        n2 = pos.x + (n1 + Mathf.Sin(angle.y / 57.3f));
        n3 = pos.z + (n1 + Mathf.Cos(angle.y / 57.3f));

        x = x + (n2 - x);
        z = z + (n3 - z);
        transform.position = new Vector3(x, 0, z);
	}
}
