using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(GridCreator))]
public class PickUpSpawner : MonoBehaviour {

    [SerializeField]
    private Vector2 m_minMaxSpawnCooldown = new Vector2(20f, 50f);
    [SerializeField]
    private GameObject m_prefabPickUp;

    private float timer = 20f;
    private GridCreator gridder;
    private int lastGridIndex;

    void Awake()
    {
        Assert.IsNotNull<GameObject>(m_prefabPickUp);
        gridder = GetComponent<GridCreator>();
        Assert.IsNotNull<GridCreator>(gridder);
    }

	void Update () {
        if (timer > 0f)
            timer -= Time.deltaTime;
        else
        {
            // spawn one randomly
            // calc location
            int index = Random.Range(0, gridder.CellList.Count);
            index += (index == lastGridIndex) ? 1 : 0;
            lastGridIndex = index;
            Transform randomCell = ((GameObject) gridder.CellList[index]).transform;

            GameObject pickup = Instantiate<GameObject>(m_prefabPickUp);
            pickup.transform.position = randomCell.position;
            pickup.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 0f));
             
            // set a new timer for the next cooldown
            timer = Random.Range(m_minMaxSpawnCooldown.x, m_minMaxSpawnCooldown.y);
            //print("Spawn Pickup, next timer on " + timer);
        }
	}
}
