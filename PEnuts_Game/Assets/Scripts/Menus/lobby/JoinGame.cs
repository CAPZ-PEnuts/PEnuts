﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour
{
    
    private Text status;
    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        InvokeRepeating("RefreshRoomList", 0, 3f);
    }

    public void RefreshRoomList()
    {
        status.text = "Loading...";
        networkManager.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
    }

    private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";
        if (!success)
        {
            status.text = "An issue occured with your connection...";
            return;
        }


        int id = 1;
        
        foreach (var match in matchList)
        {
            var roomGameObject = GameObject.Find("room_" + id);
            roomGameObject.GetComponentInChildren<TextMeshProUGUI>().SetText(match.name);
            roomGameObject.GetComponent<Button>().gameObject.SetActive(true);
            roomGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            roomGameObject.GetComponent<Button>().onClick.AddListener(
                delegate
                {
                    status.text = "Joining...";
                    networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
                });
            id++;
        }

        //status.text
        if (id == 1)
            status.text = "No rooms for the time...";
        
        else
            status.text = "[" + (id-1) + "/10] rooms";

        //disable empty rooms
        while (id <= 10)
        {
            var roomGameObject = GameObject.Find("room_" + id);
            roomGameObject.GetComponentInChildren<TextMeshProUGUI>().SetText("");
            roomGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            roomGameObject.GetComponent<Button>().gameObject.SetActive(false);
            id++;
        }
    }

    public void OnMatchJoined(bool success, string extendInfo, MatchInfo matchInfo)
    {
        if (success)
            networkManager.StartClient(matchInfo);
    }
}
