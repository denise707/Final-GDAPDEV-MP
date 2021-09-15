using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Enemy Copies
    [SerializeField] private GameObject waspCopy;
    [SerializeField] private GameObject metalArmCopy;
    [SerializeField] private GameObject insectCopy;
    [SerializeField] private GameObject mutantCopy;
    [SerializeField] private GameObject fatCopy;
    [SerializeField] private GameObject cobraCopy;

    //Enemy List
    [SerializeField] public List<GameObject> enemyList;
    [SerializeField] private List<GameObject> enemyCopies;

    //Spawn Locations
    [SerializeField] private List<GameObject> flyingLoc;
    [SerializeField] private List<GameObject> groundLoc;

    public static int count = 0;
    bool waveReleased = false;
    int index = 0;
    float ticks = 0.0f;
    int total_enemies = 0;
    public int boss_num = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Add availlable enemies
        enemyCopies.Add(waspCopy);
        enemyCopies.Add(metalArmCopy);
        enemyCopies.Add(insectCopy);
        enemyCopies.Add(mutantCopy);
        enemyCopies.Add(fatCopy);
        enemyCopies.Add(cobraCopy);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.next && !GameSystem.boss_level)
        {
            //To change possible number of enemies per wave
            total_enemies = Random.Range(1, GameSystem.enemy_increment + 1);
            for (int i = 0; i < total_enemies; i++)
            {
                GameObject enemyCopy = enemyCopies[Random.Range(0, 3)];
                Vector3 newLocation = GetLocation(enemyCopy)[Random.Range(0, GetLocation(enemyCopy).Count - 1)].transform.position;
                enemyCopy = ObjectUtils.SpawnDefault(enemyCopy, this.transform.parent, newLocation);
                enemyCopy.SetActive(true);
                enemyList.Add(enemyCopy);                
            }
            GameSystem.next = false;
            count = enemyList.Count;
            waveReleased = false;
        }

        if (GameSystem.next && GameSystem.boss_level)
        {
            //To change possible number of enemies per wave
            total_enemies = 1;
            for (int i = 0; i < total_enemies; i++)
            {
                GameObject enemyCopy = enemyCopies[boss_num];
                Vector3 newLocation = groundLoc[1].transform.position;
                enemyCopy = ObjectUtils.SpawnDefault(enemyCopy, this.transform.parent, newLocation);
                enemyCopy.SetActive(true);
                enemyList.Add(enemyCopy);
            }
            GameSystem.next = false;
            count = enemyList.Count;
            waveReleased = false;
        }

        //If all enemies for a wave is released, check if need to delete from list
        if (waveReleased)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null)
                {
                    enemyList.RemoveAt(i);
                }
            }
            count = enemyList.Count;
            if (count <= 0) index = 0;
        }

        //Check if wave is released
        if (index > total_enemies - 1)
        {
            waveReleased = true;
        }

        if (waveReleased == false)
        {
            ticks += Time.deltaTime;

            if (ticks >= Random.Range(3.0f, 13.0f))
            {
                enemyList[index].GetComponent<Enemy>().move = true;
                index++;
                ticks = 0.0f;
            }
        }
    }

    private List<GameObject> GetLocation(GameObject toSpawn)
    {
        List<GameObject> location = null;

        switch (toSpawn.GetComponent<Enemy>().type)
        {
            case "WASP":
                location = this.flyingLoc;
                break;
            default: location = this.groundLoc;
                break;
        }

        return location;
    }
}

public class ObjectUtils
{
    public static GameObject SpawnDefault(GameObject toSpawn, Transform parent, Vector3 localPos)
    {
        GameObject myObj = GameObject.Instantiate(toSpawn, parent);
        myObj.transform.localPosition = localPos;
        
        return myObj;
    }   
}
