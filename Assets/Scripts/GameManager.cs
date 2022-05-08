using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> weapons = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> drops = new List<GameObject>();
    private string pathToPrefabs = "Assets/Resources/";
    private Transform player = null;
    private int numberOfEnemies = 50;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InitPrefabs();
        SpawnEnemies(numberOfEnemies);
        //spawnAtEdge();

        player.gameObject.GetComponent<PlayerAttack>().enabled = true;
    }

    internal void GivePlayerItem()
    {
        //TODO: Open selction dialog with weapons and give player weapon
        Debug.Log("Given player an item");

    }

    //private void spawnAtEdge()
    //{
    //    //get position at the edge of the camera

    //    var direction = (Direction)UnityEngine.Random.Range(0, 3);
    //    Vector3 spawnPosition = GetSpawnPositionFromDirection(direction);

    //    int randomEnemy = UnityEngine.Random.Range(0, enemies.Count);
    //    GameObject enemy = enemies[randomEnemy];
    //    Instantiate(enemy, spawnPosition, Quaternion.identity);
    //}

    private Vector3 GetSpawnPositionFromDirection(Direction direction)
    {
        //generate random value between 0 and 1
        float randomValue = UnityEngine.Random.value;


        switch (direction)
        {
            case Direction.North:
                return (Camera.main.ViewportToWorldPoint(new Vector3(randomValue, 1+0.1f, 0)));
            case Direction.South:
                return (Camera.main.ViewportToWorldPoint(new Vector3(randomValue, 0 - 0.1f, 0)));
            case Direction.West:
                return (Camera.main.ViewportToWorldPoint(new Vector3(0 - 0.1f, randomValue, 0)));
            case Direction.East:
                return (Camera.main.ViewportToWorldPoint(new Vector3(1 + 0.1f, randomValue, 0)));
            default:
                return (Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)));
        }
    }

    private void SpawnEnemies(int numberOfEnemies)
    {
        //spawn enemies
        for (int i = 0; i < numberOfEnemies; i++)
        {
            //select random enemy
            int randomEnemy = UnityEngine.Random.Range(0, enemies.Count);
            GameObject enemy = enemies[randomEnemy];

            var direction = (Direction)UnityEngine.Random.Range(0, 3);
            Vector3 spawnPosition = GetSpawnPositionFromDirection(direction);

            Instantiate(enemy, spawnPosition, Quaternion.identity);
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
                    case "Drop":
                        {
                            AddDrop(currentPrefab);
                            break;
                        }
                }
            }
        }
    }


    internal void DropItem(Transform killedEnemy)
    {
        //get random drop   
        int randomDrop = UnityEngine.Random.Range(0, drops.Count);
        GameObject drop = drops[randomDrop];
        Instantiate(drop, killedEnemy.position, killedEnemy.rotation);

    }
    private void AddDrop(GameObject currentPrefab)
    {
        drops.Add(currentPrefab);
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
