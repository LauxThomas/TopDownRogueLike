using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Garlic behaviour
    public float radius = 2f;

    private void Start()
    {
        createGarlicChild();
    }

    /// <summary>
    /// Create empty Gameobject with current Gameobject as parent. Give this gameobject an circle Collider and set the isTrigger property to true.
    /// </summary>
    private void createGarlicChild()
    {
        var garlicChild = new GameObject("GarlicChild");
        garlicChild.transform.position = transform.position;
        garlicChild.transform.rotation = Quaternion.identity;
        garlicChild.transform.parent = transform;

        garlicChild.tag = "Garlic";
        garlicChild.AddComponent<CircleCollider2D>();
        garlicChild.GetComponent<CircleCollider2D>().isTrigger = true;
        garlicChild.GetComponent<CircleCollider2D>().radius = radius;
        garlicChild.AddComponent<Garlic>();

    }
}
