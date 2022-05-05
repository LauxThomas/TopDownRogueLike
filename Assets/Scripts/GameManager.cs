using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public string pathToPrefabs = "Assets/Resources/";
    public Transform player = null;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        

        var info = new DirectoryInfo(pathToPrefabs);
        var fileInfo = info.GetFiles();

        foreach (var file in fileInfo)
        {
            if (file.Extension == ".prefab")
            {
                var itemName = file.Name.Replace(".prefab", "");
                GameObject item = Resources.Load(itemName) as GameObject;
                var go = Instantiate(item, player);
                //go.transform.parent = player;
                items.Add(go);
                go.SetActive(false);
            }

            //Debug.Log(file.Name);
            //var item = Resources.Load(pathToPrefabs + file.Name) as GameObject;
            //items.Add(item);
        }


        //TODO: Write paths from prefabs into textfile or loop through complete folder
        //UnityEngine.Object pPrefab = Resources.Load("Assets/Prefab/Items/Garlic");
        //var pref = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity);
        //items.Add(pref);
        //pref.SetActive(false);
        player.gameObject.GetComponent<PlayerAttack>().enabled = true;
    }


    public GameObject GetItem(int index)
    {
        return items[index % items.Count];
    }
    public string GetItemName(int index)
    {
        return items[index % items.Count].name;
    }

    internal int GetItemCount()
    {
        return items.Count;
    }

    private UnityEngine.Object LoadPrefabFromFile(string filename)
    {
        Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
        var loadedObject = Resources.Load("Levels/" + filename);
        if (loadedObject == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        return loadedObject;
    }


}
