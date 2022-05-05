using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    private string pathToPrefabs = "Assets/Resources/";
    public Transform player = null;
    public int numberOfEnemies = 10;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InitPrefabs();
        SpawnEnemies(numberOfEnemies);


        player.gameObject.GetComponent<PlayerAttack>().enabled = true;
    }

    private void SpawnEnemies(int numberOfEnemies)
    {

        //spawn enemies
        for (int i = 0; i < numberOfEnemies; i++)
        {
            //select random enemy
            int randomEnemy = UnityEngine.Random.Range(0, enemies.Count);
            GameObject enemy = enemies[randomEnemy];

            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), 0);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(enemy, spawnPosition, spawnRotation);
        }
    }

    private void InitPrefabs()
    {
        var info = new DirectoryInfo(pathToPrefabs);
        var fileInfo = info.GetFiles();

        foreach (var file in fileInfo)
        {
            if (file.Extension == ".prefab")
            {
                var itemName = file.Name.Replace(".prefab", "");
                GameObject currentPrefab = Resources.Load(itemName) as GameObject;

                switch (currentPrefab.tag)
                {
                    case "Weapon":
                        {
                            AddWeapon(currentPrefab);
                            break;
                        }
                    case "Enemy":
                        {
                            AddEnemy(currentPrefab);
                            break;
                        }
                }
            }
        }
    }

    internal void DropItem(GameObject gameObject)
    {
        Debug.Log("Drop item");
    }

    private void AddEnemy(GameObject currentPrefab)
    {
        enemies.Add(currentPrefab);
    }

    private void AddWeapon(GameObject currentPrefab)
    {
        var currentWeapon = Instantiate(currentPrefab, player);
        weapons.Add(currentWeapon);
        currentWeapon.SetActive(false);
    }

    public GameObject GetItem(int index)
    {
        return weapons[index % weapons.Count];
    }
    public string GetItemName(int index)
    {
        return weapons[index % weapons.Count].name;
    }

    internal int GetItemCount()
    {
        return weapons.Count;
    }



}
