using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
	public GameObject playerPrefab;
	public GameObject cameraPrefab;

	public Vector2 minRange;
	public Vector2 maxRange;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 random = transform.position
			+ Vector3.right * Random.Range(minRange.x, maxRange.x)
			+ Vector3.forward * Random.Range(minRange.y, maxRange.y);
		GameObject test = PhotonNetwork.Instantiate(playerPrefab.name, random, Quaternion.identity);
		if (test.GetComponent<PhotonView>().IsMine) {
			test.GetComponent<Movement>().SetData(Instantiate(cameraPrefab, random, Quaternion.identity));
		}
    }
}