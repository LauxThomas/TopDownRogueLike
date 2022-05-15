using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;
    private List<Button> Buttons = new List<Button>();
    private List<TMP_Text> TextList = new List<TMP_Text>();
    private bool IsInstantiated;
    private List<GameObject> weapons = new List<GameObject>();

    private void InitButtons()
    {
        GameObject[] buttonGameObjects = GameObject.FindGameObjectsWithTag("LevelUpOption");
        foreach (GameObject buttonGO in buttonGameObjects)
        {
            var buttonComponent = buttonGO.GetComponent<Button>();
            Buttons.Add(buttonComponent);
            TextList.Add(buttonComponent.gameObject.GetComponentInChildren<TMP_Text>());
        }
        IsInstantiated = true;
    }

    public void HandleLevelUpSelection(int SelectedItem)
    {
        if (gameManager == null) { gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); }
        var selectedOptionName = TextList[SelectedItem];
        gameManager.CloseLevelSceneAndUnFreezeTime(selectedOptionName.text);
    }

    internal void SetWeapons(List<GameObject> weapons)
    {
        this.weapons = weapons;
    }

    private void OnEnable()
    {
        if (!IsInstantiated) InitButtons();
        RandomizeOptions();
    }

    private void RandomizeOptions()
    {
        foreach (var t in TextList)
        {
            //get random entry in weapons
            var weapon = weapons[UnityEngine.Random.Range(0, weapons.Count)];
            

           t.text = weapon.name;
        }
    }
}
