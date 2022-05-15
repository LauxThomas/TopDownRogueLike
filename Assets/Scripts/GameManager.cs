using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> weapons = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> drops = new List<GameObject>();
    private string pathToPrefabs = "Assets/Resources/";
    private Transform player = null;
    private int numberOfEnemies = 50;

    [SerializeField]private LevelUpScreen levelUpScreen;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;


        InitPrefabs();
        SpawnEnemies(numberOfEnemies);
        //spawnAtEdge();

        player.gameObject.GetComponent<PlayerAttack>().enabled = true;
        if (levelUpScreen == null) { levelUpScreen = GameObject.FindGameObjectWithTag("LevelUpScreen").GetComponent<LevelUpScreen>(); }
        levelUpScreen.SetWeapons(weapons);
        levelUpScreen.gameObject.SetActive(false);
    }

    internal void OpenLevelSceneAndFreezeTime()
    {
        StopTime(true);
        SetLevelUpScreenActive(true);
    }

    public void CloseLevelSceneAndUnFreezeTime(string SelectedItem)
    {
        UpdateLevelOfSelectedItem(SelectedItem);
        StopTime(false);
        SetLevelUpScreenActive(false);
    }

    private void UpdateLevelOfSelectedItem(string selectedItem)
    {
        var weapon = GetWeaponBasedOfName(selectedItem);
        if (weapon != null)
        {
            weapon.GetComponent<WeaponStats>().Level += 1;
            Debug.Log("Waffenlevel von " + weapon.name + " ist nun " + weapon.GetComponent<WeaponStats>().Level);
        }
    }

    private GameObject GetWeaponBasedOfName(string selectedItem)
    {
        return(weapons.Where(obj => obj.name == selectedItem).SingleOrDefault());
    }

    private void SetLevelUpScreenActive(bool isActive) => levelUpScreen.gameObject.SetActive(isActive);

    private void StopTime(bool isStopped) => Time.timeScale = isStopped ? 0 : 1;


    private Vector3 GetSpawnPositionFromDirection(Direction direction)
    {
        //generate random value between 0 and 1
        float randomValue = UnityEngine.Random.value;


        return direction switch
        {
            Direction.North => (Camera.main.ViewportToWorldPoint(new Vector3(randomValue, 1 + 0.1f, 0))),
            Direction.South => (Camera.main.ViewportToWorldPoint(new Vector3(randomValue, 0 - 0.1f, 0))),
            Direction.West => (Camera.main.ViewportToWorldPoint(new Vector3(0 - 0.1f, randomValue, 0))),
            Direction.East => (Camera.main.ViewportToWorldPoint(new Vector3(1 + 0.1f, randomValue, 0))),
            _ => (Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0))),
        };
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

                    default:
                        break;
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
        currentWeapon.name = currentWeapon.name.Replace("(Clone)", "");
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
