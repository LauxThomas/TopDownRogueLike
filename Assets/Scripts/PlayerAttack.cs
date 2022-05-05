using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    //Garlic behaviour
    public float radius = 2f;

    private List<Transform> attacks = new List<Transform>();

    private void Start()
    {
        activateGarlic();
    }

    private void activateGarlic()
    {
        foreach (Transform child in transform)
            attacks.Add(child);

        Transform garlic = attacks.Where(obj => obj.name == "GarlicItem(Clone)").SingleOrDefault();
        garlic.gameObject.SetActive(true);
    }
}
