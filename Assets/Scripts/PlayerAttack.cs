using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    private List<Transform> weapons = new List<Transform>();

    private void Start()
    {
        activateGarlic();
    }

    private void activateGarlic()
    {
        foreach (Transform child in transform)
            weapons.Add(child);

        Transform garlic = weapons.Where(obj => obj.name == "GarlicItem(Clone)").SingleOrDefault();
        garlic.gameObject.SetActive(true);
    }
}
