using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
	public TMP_InputField nameInput;
	public GameObject loadingText;
	public GameObject nameHolder;

	void Start()
	{
		//Debug.Log("Connecting");
		PhotonNetwork.ConnectUsingSettings();
		nameInput.text = "Player " + Random.Range(0, 100).ToString("000");
		loadingText.SetActive(true);
		nameHolder.SetActive(false);
	}

	public void OnClickJoin() {
		if (string.IsNullOrEmpty(nameInput.text)) {
			return;
		}
		PhotonNetwork.JoinLobby();
	}

	public override void OnConnectedToMaster()
	{
		//Debug.Log("Connected");
		//PhotonNetwork.JoinLobby();
		loadingText.SetActive(false);
		nameHolder.SetActive(true);
	}

	public override void OnJoinedLobby()
	{
		//Debug.Log("Joined Main Lobby");
		SceneManager.LoadScene("Lobby");
		PhotonNetwork.NickName = nameInput.text;
	}
}
