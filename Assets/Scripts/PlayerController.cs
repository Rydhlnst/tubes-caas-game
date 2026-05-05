using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Pergerakan Player Utama")]
    public float laneSpeed = 10f;
    public float xLimit = 8f;

    [Header("Initial Soldier")]
    [SerializeField] private GameObject initialSoldier;

    [Header("Pengaturan Kerumunan Masif")]
    public GameObject soldierPrefab;
    public int currentArmyCount = 1;
    public float smoothSpeed = 5f;

    [Range(1f, 10f)]
    public float maxCrowdSpread = 6f;

    private List<GameObject> armyList = new List<GameObject>();
    private List<float> armyRelativeX = new List<float>();
    private List<float> armyRelativeZ = new List<float>();

    private void Start()
    {
        if (initialSoldier != null)
        {
            RegisterSoldier(initialSoldier);
        }

        currentArmyCount = armyList.Count;
    }

    private void Update()
    {
        MoverPlayer();
        MoveMassiveCrowd();
    }

    private void MoverPlayer()
    {
        float horizontalInput = 0;

        if (Input.GetKey(KeyCode.A)) horizontalInput = -1;
        else if (Input.GetKey(KeyCode.D)) horizontalInput = 1;

        Vector3 newPos = transform.position + new Vector3(horizontalInput, 0, 0) * laneSpeed * Time.deltaTime;

        newPos.x = Mathf.Clamp(newPos.x, -xLimit, xLimit);
        transform.position = newPos;
    }

    private void MoveMassiveCrowd()
    {
        int count = Mathf.Min(armyList.Count, armyRelativeX.Count, armyRelativeZ.Count);

        for (int i = 0; i < count; i++)
        {
            if (armyList[i] == null) continue;

            float desiredX = transform.position.x + armyRelativeX[i];
            float desiredZ = transform.position.z + armyRelativeZ[i];

            float crowdPadding = 0.5f;
            desiredX = Mathf.Clamp(desiredX, -(xLimit + crowdPadding), (xLimit + crowdPadding));

            Vector3 safeTargetPos = new Vector3(desiredX, transform.position.y, desiredZ);

            armyList[i].transform.position = Vector3.Lerp(
                armyList[i].transform.position,
                safeTargetPos,
                smoothSpeed * Time.deltaTime
            );
        }
    }

    public void ExecuteGateLogic(GateLogic.GateType tipe, int nilai)
    {
        int amountToAdd = 0;

        switch (tipe)
        {
            case GateLogic.GateType.Tambah:
                amountToAdd = nilai;
                break;

            case GateLogic.GateType.Kurang:
                RemoveSoldiers(nilai);
                break;

            case GateLogic.GateType.Kali:
                amountToAdd = (currentArmyCount * nilai) - currentArmyCount;
                break;

            case GateLogic.GateType.Bagi:
                int targetCount = currentArmyCount / nilai;
                int amountToRemove = currentArmyCount - targetCount;
                RemoveSoldiers(amountToRemove);
                break;
        }

        for (int i = 0; i < amountToAdd; i++)
        {
            SpawnNewSoldier();
        }

        currentArmyCount = armyList.Count;
    }

    private void RemoveSoldiers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (armyList.Count <= 0) break;

            int lastIndex = armyList.Count - 1;
            GameObject soldierToDestroy = armyList[lastIndex];

            armyList.RemoveAt(lastIndex);

            if (lastIndex < armyRelativeX.Count)
            {
                armyRelativeX.RemoveAt(lastIndex);
            }

            if (lastIndex < armyRelativeZ.Count)
            {
                armyRelativeZ.RemoveAt(lastIndex);
            }

            if (soldierToDestroy != null)
            {
                Destroy(soldierToDestroy);
            }
        }

        currentArmyCount = armyList.Count;
    }

    private void SpawnNewSoldier()
    {
        float randomX = Random.Range(-maxCrowdSpread, maxCrowdSpread);
        float randomZ = Random.Range(-maxCrowdSpread, maxCrowdSpread);

        Vector3 spawnPos = transform.position + new Vector3(randomX, 0, randomZ);

        GameObject newSoldier = Instantiate(soldierPrefab, spawnPos, Quaternion.identity);

        RegisterSoldier(newSoldier);
    }

    private void RegisterSoldier(GameObject soldier)
    {
        if (soldier == null) return;
        if (armyList.Contains(soldier)) return;

        armyList.Add(soldier);
        armyRelativeX.Add(soldier.transform.position.x - transform.position.x);
        armyRelativeZ.Add(soldier.transform.position.z - transform.position.z);

        currentArmyCount = armyList.Count;
    }
}