using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns projectiles and enemies
/// </summary>
public static class Spawner
{
    // object pool for pellets
    static List<GameObject> pelletPool;
    // object pool for barks
    static List<GameObject> barkPool;
    // object pool for wolves
    static List<GameObject> wolfPool;

    // This ensure that we don't get so unusual behaviour when
    // the GameOver screen is displayed
    static bool isActive;

    /// <summary>
    /// Initialize the pools and event handling.
    /// </summary>
    public static void Initialize()
    {
        EventManager.AddGameOverListener(GameOver);
        Spawner.isActive = true;

        pelletPool = new List<GameObject>(6);
        for (int i = 0; i < pelletPool.Capacity; i++)
        {
            pelletPool.Add(getNewProjectile(ProjectileType.PELLET));
        }

        barkPool = new List<GameObject>(6);
        for (int i = 0; i < barkPool.Capacity; i++)
        {
            barkPool.Add(getNewProjectile(ProjectileType.BARK));
        }

        wolfPool = new List<GameObject>(5);
        for (int i = 0; i < wolfPool.Capacity; i++)
        {
            wolfPool.Add(getNewWolf());
        }
    }

    /// <summary>
    /// Spawns projectiles (either pellets or barks)
    /// </summary>
    /// <returns>The projectile.</returns>
    /// <param name="type">Type.</param>
    /// <param name="position">Position.</param>
    public static GameObject SpawnProjectile(ProjectileType type, Vector2 position)
    {
        GameObject projectile = null;

        if (Spawner.isActive)
        {
            if (type == ProjectileType.PELLET)
            {
                if (pelletPool.Count > 0)
                {
                    projectile = pelletPool[pelletPool.Count - 1];
                    pelletPool.RemoveAt(pelletPool.Count - 1);
                    projectile.transform.position = position;
                }
                else
                {
                    pelletPool.Capacity++;
                    projectile = getNewProjectile(type);
                    projectile.transform.position = position;
                    pelletPool.Add(projectile);
                }
            }
            else
            {
                if (barkPool.Count > 0)
                {
                    projectile = barkPool[barkPool.Count - 1];
                    barkPool.RemoveAt(barkPool.Count - 1);
                    projectile.transform.position = position;
                }
                else
                {
                    barkPool.Capacity++;
                    projectile = getNewProjectile(type);
                    projectile.transform.position = position;
                    barkPool.Add(projectile);
                }

            }
        }

        return projectile;
    }

    /// <summary>
    /// Spawns wolves
    /// </summary>
    /// <returns>The wolf.</returns>
    /// <param name="position">Position.</param>
    public static GameObject SpawnWolf(Vector2 position)
    {
        if (isActive)
        {
            GameObject wolf;

            if (wolfPool.Count > 0)
            {
                wolf = wolfPool[wolfPool.Count - 1];
                wolfPool.RemoveAt(wolfPool.Count - 1);
                wolf.transform.position = position;
            }
            else
            {
                wolfPool.Capacity++;
                wolf = getNewWolf();
                wolf.transform.position = position;
                wolfPool.Add(wolf);
            }

            return wolf;
        }

        return null;
    }

    /// <summary>
    /// Returns projectiles to the appropriate pool
    /// </summary>
    /// <param name="projectile">Projectile.</param>
    public static void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.StopMoving();
        if (projectileComponent.Type() == ProjectileType.PELLET)
            pelletPool.Add(projectile);
        else
            barkPool.Add(projectile);
    }

    /// <summary>
    /// Returns wolves to the appropriate pool
    /// </summary>
    /// <param name="wolf">Wolf.</param>
    public static void ReturnWolf(GameObject wolf)
    {
        wolf.SetActive(false);
        wolf.GetComponent<Wolf>().StopMoving();
        wolfPool.Add(wolf);
    }

    /// <summary>
    /// Creates a new projectile of the specified type
    /// </summary>
    /// <returns>The new projectile.</returns>
    /// <param name="type">Type.</param>
    private static GameObject getNewProjectile(ProjectileType type)
    {
        GameObject projectile = null;

        switch (type)
        {
            case ProjectileType.PELLET:
                projectile = Object.Instantiate(Resources.Load("Pellet")) as GameObject;
                break;
            case ProjectileType.BARK:
                projectile = Object.Instantiate(Resources.Load("Bark")) as GameObject;
                break;
        }

        if (projectile != null)
        {
            projectile.GetComponent<Projectile>().Initialize();
            projectile.SetActive(false);
            GameObject.DontDestroyOnLoad(projectile);
        }

        return projectile;
    }

    /// <summary>
    /// Creates a new wolf
    /// </summary>
    /// <returns>The new wolf.</returns>
    private static GameObject getNewWolf()
    {
        GameObject wolf = Object.Instantiate(Resources.Load("Wolf")) as GameObject;

        if (wolf != null)
        {
            wolf.GetComponent<Wolf>().Initialize();
            wolf.SetActive(false);
            GameObject.DontDestroyOnLoad(wolf);
        }

        return wolf;
    }

    /// <summary>
    /// Disable spawner when game ends
    /// </summary>
    static void GameOver()
    {
        Spawner.isActive = false;
    }
}
