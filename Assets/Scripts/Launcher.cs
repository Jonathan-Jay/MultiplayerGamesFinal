using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
	public GameObject lobbyPanel;
	public GameObject roomPanel;

	public TMP_InputField roomInput;
	public TMP_Text error;

	public TMP_Text roomName;
	public TMP_Text playerCount;

	public GameObject playerListing;
	public Transform playerListContent;

	public Button startButton;
	public Player currentPlayer;

	//public GameObject readyButton;
	//public Transform buttonOrganizer;
	private void Start() {
		lobbyPanel.SetActive(true);
		roomPanel.SetActive(false);
		error.gameObject.SetActive(false);
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public void CreateRoom()
	{
		if (string.IsNullOrEmpty(roomInput.text)) {
			return;
		}

		PhotonNetwork.CreateRoom(roomInput.text);
	}

	public void JoinRoom()
	{
		PhotonNetwork.JoinRoom(roomInput.text);
	}

	public override void OnJoinedRoom()
	{
		//PhotonNetwork.LoadLevel("Game");

		lobbyPanel.SetActive(false);
		roomPanel.SetActive(true);

		roomName.text = PhotonNetwork.CurrentRoom.Name;

		Player[] players = PhotonNetwork.PlayerList;

		playerCount.text = players.Length.ToString();

		for (int i = 0; i < players.Length; i++) {
			Instantiate(playerListing, playerListContent).GetComponent<PlayerListing>().SetPlayerInfo(players[i]);
			
			currentPlayer = players[i];

			startButton.interactable = i == 0;
		}
		
		//Instantiate(readyButton, buttonOrganizer).GetComponent<ReadyButton>().SetCurrentPlayer(playerListContent.gameObject);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		error.gameObject.SetActive(true);
		error.text = message;
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		error.gameObject.SetActive(true);
		error.text = message;
	}

	public void OnClickLeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("LoadingScene");
	}
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Instantiate(playerListing, playerListContent).GetComponent<PlayerListing>().SetPlayerInfo(newPlayer);

		playerCount.text = PhotonNetwork.PlayerList.Length.ToString();
	}

	public override void OnPlayerLeftRoom(Player player) {
		int count = PhotonNetwork.PlayerList.Length;
		playerCount.text = count.ToString();
		//test for new leader
		PlayerListing[] players = playerListContent.GetComponentsInChildren<PlayerListing>();
		if (players[0].player == player && players.Length > 1) {
			startButton.interactable = players[1].player == currentPlayer;
		}
	}
	
	public void OnClickStartGame()
	{
		PhotonNetwork.LoadLevel("Arena");
	}
}
