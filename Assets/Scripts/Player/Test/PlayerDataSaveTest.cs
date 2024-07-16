using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSaveTest : MonoBehaviour
{
    PlayerData playerData;

    void Start()
    {
        playerData = DataManager.Instance.PlayerFileLoad("PlayerInfo");
        //print($"PlayerDataSet => maxHP: {playerData.maxHP}, damage: {playerData.damage}, experience: {playerData.experience}");
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DataManager.Instance.PlayerInfoSave(100f, 10f, 0f, 1);

            //playerData = DataManager.Instance.FileLoad("PlayerInfo");
            //print($"PlayerDataSet => maxHP: {playerData.maxHP}, damage: {playerData.damage}, experience: {playerData.experience}");

        }
    }
}
