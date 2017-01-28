using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour {

	public Camera viewCam;

	private GameObject upButton;
    private GameObject downButton;
    private GameObject leftButton;
    private GameObject rightButton;

    float r = 100.0f;

	// Use this for initialization
	void Start () 
    {
        upButton = transform.Find("UpButton").gameObject;
        downButton = transform.Find("DownButton").gameObject;
        leftButton = transform.Find("LeftButton").gameObject;
        rightButton = transform.Find("RightButton").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {

		Vector3 anchor0 = new Vector3(0, 0, 0);
		Vector3 screenAnchor0 = viewCam.WorldToScreenPoint(anchor0);
		Vector3 anchor1 = new Vector3(1.0f, 0, 0);
		Vector3 screenAnchor1 = viewCam.WorldToScreenPoint(anchor1);
		Vector3 screenAxisX = screenAnchor1 - screenAnchor0;

		float deltaAngle = Angle (
			new Vector3(anchor1.x, anchor1.y, 0), 
            new Vector3(screenAxisX.x, screenAxisX.y, 0)
			);

        upButton.transform.localRotation = (Quaternion.Euler (new Vector3 (0, 0, deltaAngle)));
        upButton.transform.localPosition = new Vector3(
            r * Mathf.Cos(Mathf.Deg2Rad * (deltaAngle + 90)),
            r * Mathf.Sin(Mathf.Deg2Rad * (deltaAngle + 90)),
            0.0f
        );

        rightButton.transform.localRotation = (Quaternion.Euler (new Vector3 (0, 0, deltaAngle + 90)));
        rightButton.transform.localPosition = new Vector3(
            r * Mathf.Cos(Mathf.Deg2Rad * (deltaAngle + 180)),
            r * Mathf.Sin(Mathf.Deg2Rad * (deltaAngle + 180)),
            0.0f
        );

        leftButton.transform.localRotation = (Quaternion.Euler (new Vector3 (0, 0, deltaAngle - 90)));
        leftButton.transform.localPosition = new Vector3(
            r * Mathf.Cos(Mathf.Deg2Rad * deltaAngle),
            r * Mathf.Sin(Mathf.Deg2Rad * deltaAngle),
            0.0f
        );

        downButton.transform.localRotation = (Quaternion.Euler (new Vector3 (0, 0, deltaAngle + 180)));
        downButton.transform.localPosition = new Vector3(
            r * Mathf.Cos(Mathf.Deg2Rad * (deltaAngle - 90)),
            r * Mathf.Sin(Mathf.Deg2Rad * (deltaAngle - 90)),
            0.0f
        );
            
	}

	float Angle(Vector3 from, Vector3 to)
	{
		Vector3 crossProduct = Vector3.Cross (from, to);
		if (crossProduct.z > 0) 
		{
			return Vector3.Angle(from, to);
		} 
		else 
		{
			return 360 - Vector3.Angle(from, to);
		}
	}
}
