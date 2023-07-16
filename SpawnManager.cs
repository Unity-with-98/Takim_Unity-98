using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;

    public static SpawnManager Instance {  get { return _instance; } }

    public GameObject player;
    private Vector3 startPos;
    private float repeatWidth;
    public float numberOfTile = 0;
    private BoxCollider bgBoxCol;
    public GameObject[] tilePrefabs;

    public float spawnInterval = 5f;

    private float timer = 0f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SpawnTile(int index)
    {
        GameObject tile = tilePrefabs[index];
        Vector3 tilePos = new Vector3(0, 0, 43.25f * numberOfTile);

        Instantiate(tile, tilePos, Quaternion.Euler(0, 0, 0));
        
    }
    

    void Update()
    {
        /*timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            int index = Random.Range(0, tilePrefabs.Length + 1);
            DeleteTile();
            SpawnTile(index);
            numberOfTile++;
            timer = 0f;
            
        }*/

        
        
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        

        
    }
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        
    }
    
}
