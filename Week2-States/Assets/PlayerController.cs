using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float forward = 20;
    public float strafing = 50;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float Yforce = Input.GetAxisRaw("Vertical") * forward * Time.deltaTime;
        rb.AddForce(transform.forward * Yforce);

        float Xforce = Input.GetAxisRaw("Horizontal") * strafing * Time.deltaTime;
        rb.transform.Rotate(0,Xforce,0);
	}
}
