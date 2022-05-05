using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZRotator : MonoBehaviour
{
    public int rotationSpeed = 10;
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationSpeed*Time.deltaTime);
    }
}
