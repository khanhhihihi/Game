using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Firebase.Auth;

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemapData;
    private allDetail mapdt;

    private FirebaseDatabaseManager databaseManager;
    private FirebaseUser user;
    private void Start()
    {
        mapdt = new allDetail();

        databaseManager = GameObject.Find("DatabaseManager").GetComponent<FirebaseDatabaseManager>();

        user = FirebaseAuth.DefaultInstance.CurrentUser;

        WriteAllTileMapToFirebase();
    }

    public void WriteAllTileMapToFirebase()
    {
        List<Map> allDetails = new List<Map>();
        for (int x = tilemapData.cellBounds.min.x; x < tilemapData.cellBounds.max.x; x++)
        {
            for (int y = tilemapData.cellBounds.min.y; y < tilemapData.cellBounds.max.y; y++)
            {
                Map map_dt = new Map(x, y, MapState.Ground);
                allDetails.Add(map_dt);
                }
            }
        mapdt = new allDetail(allDetails);
        Debug.Log(mapdt.ToString());

        databaseManager.WriteDatabase(user.UserId + "/allDetail", mapdt.ToString());

    }
    }

