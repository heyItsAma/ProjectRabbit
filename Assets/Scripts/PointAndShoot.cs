using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PointAndShoot : MonoBehaviour
{
    private Vector3 target;
    public GameObject crosshairs;
    public GameObject player;
    public GameObject missilePrefab;
    public GameObject missileStart;

    public float missileSpeed = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButtonDown(0))
        {
            //fire missile
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireMissile(direction, rotationZ);
        }
    }

    void fireMissile(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(missilePrefab) as GameObject;
        b.transform.position = missileStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * missileSpeed;
    }
}
