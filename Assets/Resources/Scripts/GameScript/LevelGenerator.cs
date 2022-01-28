using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 50f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private GameObject player;
    private List<GameObject> levelPartList = new List<GameObject>();
    private Vector3 lastEndPosition;
    private GameObject levelPart1, levelPart2, levelPart3, levelPart4, levelPart5, levelPart6;

    private void Awake()
    {
        InitializeLevelPart();   
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART) 
        {
            SpawnLevelPart();
        }

        if (PlayerController.GetPosition().x > lastEndPosition.x)
        {
            StartCoroutine(ExecuteAfterTime(1f));
        }
    }

    private void SpawnLevelPart() {
        GameObject chosenLevelPart = levelPartList[Random.Range(0,levelPartList.Count)];
        GameObject lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.transform.Find("EndPosition").position;
    }

    private GameObject SpawnLevelPart(GameObject levelPart,Vector3 spawnPosition) {
        GameObject levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    private void InitializeLevelPart() {
        levelPart1 = Resources.Load<GameObject>("Prefab/Platform/EasySetOne");
        levelPart2 = Resources.Load<GameObject>("Prefab/Platform/EasySetTwo");
        levelPart3 = Resources.Load<GameObject>("Prefab/Platform/EasySetThree");
        levelPart4 = Resources.Load<GameObject>("Prefab/Platform/EasySetFour");
        levelPart5 = Resources.Load<GameObject>("Prefab/Platform/EasySetFive");
        levelPart6 = Resources.Load<GameObject>("Prefab/Platform/EasySetSix");

        levelPartList.Add(levelPart1);
        levelPartList.Add(levelPart2);
        levelPartList.Add(levelPart3);
        levelPartList.Add(levelPart4);
        levelPartList.Add(levelPart5);
        levelPartList.Add(levelPart6);

        Debug.Log(levelPart1);
        Debug.Log(levelPart2);
        Debug.Log(levelPart3);
        Debug.Log(levelPart4);
        Debug.Log(levelPart5);
        Debug.Log(levelPart6);
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        // Code to execute after the delay
        Destroy(gameObject);
    }
}
