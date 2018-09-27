//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public static class WolfSpawner
//{
//    static List<GameObject> pool;

//    static bool isActive;

//    public static void Initialize()
//    {
//        EventManager.AddGameOverListener(GameOver);
//        WolfSpawner.isActive = true;

//        pool = new List<GameObject>(5);
//        for (int i = 0; i < pool.Capacity; i++)
//        {
//            pool.Add(getNewWolf());
//        }
//    }

//    public static GameObject SpawnWolf(Vector2 position)
//    {
//        if(isActive)
//        {
//            GameObject wolf;

//            if (pool.Count > 0)
//            {
//                wolf = pool[pool.Count - 1];
//                pool.RemoveAt(pool.Count - 1);
//                wolf.transform.position = position;
//            }
//            else
//            {
//                pool.Capacity++;
//                wolf = getNewWolf();
//                wolf.transform.position = position;
//                pool.Add(wolf);
//            }

//            return wolf;
//        }

//        return null;
//    }

//    public static void ReturnWolf(GameObject wolf)
//    {
//        wolf.SetActive(false);
//        wolf.GetComponent<Wolf>().StopMoving();
//        pool.Add(wolf);
//    }

//    private static GameObject getNewWolf()
//    {
//        GameObject wolf = Object.Instantiate(Resources.Load("Wolf")) as GameObject;
//        wolf.GetComponent<Wolf>().Initialize();
//        wolf.SetActive(false);
//        GameObject.DontDestroyOnLoad(wolf);
//        return wolf;
//    }

//    static void GameOver()
//    {
//        WolfSpawner.isActive = false;
//    }
//}
