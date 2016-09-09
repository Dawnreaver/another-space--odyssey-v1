using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityStandardAssets.Characters.ThirdPerson;



public class CoreGame : MonoBehaviour
{
    #region player variables
    public bool m_playerSelected = false;
    public GameObject m_aiPlayer;
    public GameObject m_playerTarget;
    public bool m_playerIsBusy = false;
    #endregion

    #region game ui
    public InGameMenuLogic m_inGameMenuLogic;
    #endregion

    #region resource variables
    public float m_energyStored = 0.0f;
    public float m_energyStorageCapacity;
    public float m_temporaryStoredEnergy = 0.0f;
    public float m_gatherResourceTime = 10.0f;
    #endregion

    #region build mode
    public GameObject m_constructionGrid;
    public List<GameObject> m_constructionCells;
    public GameObject m_previewObject;
    public bool m_buildMode = false;
    public enum BuildingTypes { Greenhouse, PowerCube, ShipFloor, ShipHull, SolarPanel, WindTurbine }
    public BuildingTypes m_buildingType = BuildingTypes.WindTurbine;
    public Mesh[] m_buildingMeshes = new Mesh[9];
    public Material[] m_buildingMaterials = new Material[12];
    #endregion
    public float distance = 10.0f;

    #region buildings
    private List<GameObject> m_builtBuildings;
    #endregion

    #region object pools
    // Buildings
    public GameObject m_coreBuilding;
    public int m_usedBuildings = 0;
    // effects
    public GameObject m_resourceProductionEffect;
    /* pooling */
    public ObjectPoolingClass m_poolingObject;
    #endregion

    #region resources

    public List<GameObject> m_resources;
    #endregion

    void Start()
    {
        m_poolingObject = new ObjectPoolingClass(gameObject, m_inGameMenuLogic.gameObject, m_coreBuilding, m_resourceProductionEffect);
        m_resources = new List<GameObject>();
        m_builtBuildings = new List<GameObject>();
        m_constructionCells = new List<GameObject>();
        Transform[] cells = m_constructionGrid.GetComponentsInChildren<Transform>();
        for (int a = 0; a < cells.Length; a++)
        {
            if (cells[a].gameObject.GetComponent<MeshRenderer>())
            {
                m_constructionCells.Add(cells[a].gameObject);
            }
        }
        m_energyStorageCapacity = DetermineEnergyStorageCapacity();
    }

    void FixedUpdate()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            GetWorldPosition();
        }
    }

    void GetWorldPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distance))
        {
            Vector3 point = hit.point;
            if (hit.transform.tag == "Ground")
            {
                Debug.Log("World point " + point);
                if (m_playerSelected)
                {
                    m_playerTarget.transform.position = point;
                    m_aiPlayer.GetComponent<AICharacterControl>().target = m_playerTarget.transform;
                }
            }
            else if (hit.transform.tag == "Damaged")
            {
                m_buildingSelected = true;
            }
            else if (hit.transform.tag == "Player" && !m_playerSelected)
            {
                m_playerSelected = true;
                m_buildingSelected = false;
            }
            else if (hit.transform.tag == "Player" && m_playerSelected)
            {
                m_playerSelected = false;
            }

            else if (hit.transform.tag == "UI")
            {
                Debug.Log("Selecting UI element");
            }
        }
        else
        {
            Debug.Log("Uneselecting ...");
            m_playerSelected = false;
            m_buildingSelected = false;
        }
    }

    public void SetBuilding(BuildingTypes buildingType)
    {
        m_buildingType = buildingType;
    }

    public void CheckPoolStartBuilding(BuildingTypes buildingType, Vector3 buildingPosition)
    {
        if (m_usedBuildings < m_poolingObject.m_buildingPoolSize)
        {
            StartBuilding(buildingType, buildingPosition);
        }
        else
        {
            for (int a = 0; a < m_poolingObject.m_poolIncrement; a++)
            {
                m_poolingObject.AddBuildingToPool();
            }
            m_poolingObject.m_buildingPoolSize += m_poolingObject.m_poolIncrement;
            StartBuilding(buildingType, buildingPosition);

        }
    }
    public void EnableBuildeMode()
    {
        m_buildMode = true;
        m_constructionGrid.SetActive(true);
        EnableAllCells();
    }

    public void DisableBuildMode()
    {
        m_buildMode = false;
        m_constructionGrid.SetActive(false);
    }

    public void DisableNonActiveCells()
    {
        foreach (GameObject cell in m_constructionCells)
        {
            if (!cell.GetComponent<ConstructionCell>().m_active)
            {
                cell.SetActive(false);
            }
        }
    }

    public void EnableAllCells()
    {
        foreach (GameObject cell in m_constructionCells)
        {
            cell.SetActive(true);
            cell.GetComponent<ConstructionCell>().m_active = false;
        }
    }

    public void SetBuildingPreview(BuildingTypes buildingType, GameObject constructionCell)
    {
        switch (buildingType)
        {
            case CoreGame.BuildingTypes.Greenhouse:
                constructionCell.GetComponent<MeshFilter>().mesh = m_buildingMeshes[1];
                break;

            case CoreGame.BuildingTypes.PowerCube:
                constructionCell.GetComponent<MeshFilter>().mesh = m_buildingMeshes[2];
                break;

            case CoreGame.BuildingTypes.ShipFloor:
                constructionCell.GetComponent<MeshFilter>().mesh = m_buildingMeshes[3];
                break;

            case CoreGame.BuildingTypes.ShipHull:
                constructionCell.GetComponent<MeshFilter>().mesh = m_buildingMeshes[4];
                break;

            case CoreGame.BuildingTypes.SolarPanel:
                constructionCell.GetComponent<MeshFilter>().mesh = m_buildingMeshes[5];
                break;

            case CoreGame.BuildingTypes.WindTurbine:
                constructionCell.GetComponent<MeshFilter>().mesh = m_buildingMeshes[6];
                break;
        }
    }

    public void ResetBuildingPreview()
    {
        m_previewObject.GetComponent<MeshFilter>().mesh = m_buildingMeshes[0];
        m_previewObject.GetComponent<MeshRenderer>().material = m_buildingMaterials[0];
    }

    void StartBuilding(BuildingTypes buildingType, Vector3 buildingPosition)
    {
        GameObject building = m_poolingObject.m_buildingPool[m_usedBuildings];
        Debug.Log(m_poolingObject.m_buildingPool.Count);
        Debug.Log(m_poolingObject.m_buildingPool[0]);
        CoreBuilding buildingComponent = building.GetComponent<CoreBuilding>();

        building.transform.parent = null;
        buildingComponent.SetBuildingType(buildingType);
        building.transform.position = buildingPosition;
        building.SetActive(true);
        buildingComponent.m_coreGame = this;
        buildingComponent.enabled = true;
        m_builtBuildings.Add(building);
        m_usedBuildings += 1;
        m_constructionCells.Remove(m_previewObject);
        Destroy(m_previewObject);
        DisableBuildMode();
    }

    public void SendProductionEffect(Transform productionObjectPosition, ResourceProductionEffect.ResourceTypes resourceType, float resourceAmount)
    {
        GameObject productionEffect = m_poolingObject.m_resourceProductionEffectPool.Last();
        ResourceProductionEffect productionEffectComponent = productionEffect.GetComponent<ResourceProductionEffect>();

        m_poolingObject.m_resourceProductionEffectPool.Remove(productionEffect);
        productionEffectComponent.m_targetObject = productionObjectPosition;
        productionEffectComponent.InitializePosition(productionObjectPosition);
        productionEffect.SetActive(true);
        productionEffectComponent.SetResource(resourceType, resourceAmount);
    }

    public void ReturnUsedProductionEffect( GameObject usedProductionEffect)
    {
        GameObject usedEffect = usedProductionEffect;

        m_poolingObject.m_resourceProductionEffectPool.Add(usedEffect);
        usedEffect.SetActive(false);
    }

    public float DetermineEnergyStorageCapacity()
    {
        float tempCapacity = 0.0f;
        for( int a = 0; a < m_builtBuildings.Count(); a++)
        {
            if(m_builtBuildings[a].GetComponent<CoreBuilding>().m_buildingType == BuildingTypes.PowerCube)
            {
                tempCapacity += m_builtBuildings[a].GetComponent<CoreBuilding>().m_energyStorageCapacityBuilding;
            }
        }
        return tempCapacity;
    }
}