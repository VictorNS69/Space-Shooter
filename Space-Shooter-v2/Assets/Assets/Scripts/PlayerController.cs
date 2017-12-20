
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    private AudioSource au;

    //spacecraft movement
    public float speed;
    public Boundary boundary;
    public float tilt;

    //bolt movement
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    private void Update(){
        if (Input.GetButton("Fire1") && Time.time > nextFire){
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            au.Play();
            //Other way:
            //GameObject clone = Instantiate (shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
        }
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

        rb.velocity = movement * speed;
        rb.position = new Vector3((Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax)), 0, (Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)));
        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);
    }

    private void Start(){
        rb = GetComponent<Rigidbody>();
        au = GetComponent<AudioSource>();
    }
}