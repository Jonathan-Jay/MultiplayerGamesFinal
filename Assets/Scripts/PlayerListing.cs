
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListing : MonoBehaviourPunCallbacks
{
	public TMP_Text playerName;
	public Image icon;
	public Player player;
	
	private void Start() {
		icon.color = new Color(Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), Random.Range(0f, 0.5f));
	}

	public void SetPlayerInfo(Player _player) {
		player = _player;
		playerName.text = _player.NickName;
	}
	
	public override void OnPlayerLeftRoom(Player otherPlayer) {
		if (player == otherPlayer) {
			Destroy(gameObject);
		}
	}
	
	public override void OnLeftRoom() {
		Destroy(gameObject);
	}
}
