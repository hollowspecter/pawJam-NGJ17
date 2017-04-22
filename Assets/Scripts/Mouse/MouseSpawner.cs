using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject m_prefabMouse;

    void Start()
    {
        SpawnMice();
    }

	// SPAWN THE POINTS
	void SpawnMice () {

        // Fetch the SpawnPoints
        Transform[] spawns = GetComponentsInChildren<Transform>();
        var spawnsList = new List<Transform>();
        for (int i = 0; i < spawns.Length; ++i)
        {
            spawnsList.Add(spawns[i]);
        }

        // fetch the player mapping array
        MiceManager m = MiceManager.s_instance;
        int[] playerMapping = m.getPlayerMapping();

        for (int i=0; i<playerMapping.Length; ++i)
        {
            // is player valid?
            if (playerMapping[i] < 0) break;

            // spawn player at random spawnpoint
            int r = Random.Range(0, spawnsList.Count);
            Vector3 pos = spawnsList[r].position;
            spawnsList.RemoveAt(r);
            GameObject mouse = Instantiate<GameObject>(m_prefabMouse);
            mouse.transform.position = pos;
            mouse.GetComponent<MouseController>().SetPlayerNumber(playerMapping[i]);
        }
	}
}