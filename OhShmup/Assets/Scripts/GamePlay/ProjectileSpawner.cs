//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public static class ProjectileSpawner {

//    static List<GameObject> pelletPool;
//    static List<GameObject> barkPool;

//    static bool isActive;

//    public static void Initialize()
//    {
//        EventManager.AddGameOverListener(GameOver);
//        ProjectileSpawner.isActive = true;

//        pelletPool = new List<GameObject>(6);
//        for (int i = 0; i < pelletPool.Capacity; i++)
//        {
//            pelletPool.Add(getNewProjectile(ProjectileType.PELLET));
//        }

//        barkPool = new List<GameObject>(6);
//        for (int i = 0; i < barkPool.Capacity; i++)
//        {
//            barkPool.Add(getNewProjectile(ProjectileType.BARK));
//        }
//    }

//    public static GameObject SpawnProjectile(ProjectileType type, Vector2 position)
//    {
//        GameObject projectile = null;

//        if (ProjectileSpawner.isActive)
//        {
//            if (type == ProjectileType.PELLET)
//            {
//                if (pelletPool.Count > 0)
//                {
//                    projectile = pelletPool[pelletPool.Count - 1];
//                    pelletPool.RemoveAt(pelletPool.Count - 1);
//                    projectile.transform.position = position;
//                }
//                else
//                {
//                    pelletPool.Capacity++;
//                    projectile = getNewProjectile(type);
//                    projectile.transform.position = position;
//                    pelletPool.Add(projectile);
//                }
//            }
//            else
//            {
//                if (barkPool.Count > 0)
//                {
//                    projectile = barkPool[barkPool.Count - 1];
//                    barkPool.RemoveAt(barkPool.Count - 1);
//                    projectile.transform.position = position;
//                }
//                else
//                {
//                    barkPool.Capacity++;
//                    projectile = getNewProjectile(type);
//                    projectile.transform.position = position;
//                    barkPool.Add(projectile);
//                }

//            }
//        }

//        return projectile;
//    }

//    public static void ReturnProjectile(GameObject projectile)
//    {
//        projectile.SetActive(false);
//        Projectile projectileComponent = projectile.GetComponent<Projectile>();
//        projectileComponent.StopMoving();
//        if (projectileComponent.Type() == ProjectileType.PELLET)
//            pelletPool.Add(projectile);
//        else
//            barkPool.Add(projectile);
//    }

//    private static GameObject getNewProjectile(ProjectileType type)
//    {
//        GameObject projectile = null;

//        switch (type)
//        {
//            case ProjectileType.PELLET:
//                projectile = Object.Instantiate(Resources.Load("Pellet")) as GameObject;
//                break;
//            case ProjectileType.BARK:
//                projectile = Object.Instantiate(Resources.Load("Bark")) as GameObject;
//                break;
//        }

//        if (projectile != null)
//        {
//            projectile.GetComponent<Projectile>().Initialize();
//            projectile.SetActive(false);
//            GameObject.DontDestroyOnLoad(projectile);
//        }

//        return projectile;
//    }

//    static void GameOver()
//    {
//        ProjectileSpawner.isActive = false;
//    }
//}
