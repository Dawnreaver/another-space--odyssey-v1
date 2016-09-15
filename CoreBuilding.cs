using UnityEngine;
using System.Collections;

public class CoreBuilding : MonoBehaviour
{
    public CoreGame m_coreGame;
    public Mesh[] m_buildingMesh;
    public Material[] m_buildingMaterials;

    public string m_buildingFriendlyName ="Building";
    public string m_buildingSystemName ="Building001";
    public CoreGame.BuildingTypes m_buildingType;
    public bool m_producesEnergy = false;
    public bool m_consumesEnergy = false;
    public float m_energyProduction = 0.0f;
    public float m_energyConsumption = 0.0f;
    public float m_energyStorageCapacityBuilding = 500.0f;
    private float m_energyDepot = 0.0f;
    

    // Use this for initialization
    void Awake()
    {
	    if(m_coreGame == null)
        {
            m_coreGame = GameObject.FindGameObjectWithTag("CoreGameObject").GetComponent<CoreGame>();
        }
        if ()
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(m_producesEnergy)
        {
            m_energyDepot += 5.0f * Time.deltaTime;
        }
        if(m_energyDepot >= 50.0f)
        {
            ReleaseEnergy(m_energyDepot);
        }
	}

    public void SetBuildingType(CoreGame.BuildingTypes buildingType)
    {
        switch(buildingType)
        {
            m_buildingType = buildingType;

            case CoreGame.BuildingTypes.Greenhouse:
                SetBuilding(0);
                gameObject.name = "Green House";
                Debug.Log("I'm a greenhouse");
            break;

            case CoreGame.BuildingTypes.PowerCube:
                SetBuilding(1);
                gameObject.name = "Power Cube";
                Debug.Log("I'm a powercube");
            break;

            case CoreGame.BuildingTypes.ShipFloor:
                SetBuilding(2);
                gameObject.name = "Ship Floor";
                Debug.Log("I'm a ship floor");
            break;

            case CoreGame.BuildingTypes.ShipHull:
                SetBuilding(3);
                gameObject.name = "Ship Hull";
                Debug.Log("I'm a ship hull");
            break;

            case CoreGame.BuildingTypes.SolarPanel:
                SetBuilding(4);
                gameObject.name = "Solar Panel";
                Debug.Log("I'm a solar panel");
                m_producesEnergy = true;
            break;

            case CoreGame.BuildingTypes.WindTurbine:
                SetBuilding(5);
                gameObject.name = "Wind Turbine";
                Debug.Log("I'm a wind turbine");
                m_producesEnergy = true;
            break;
        }
    }
    void SetBuilding(int buildingProperties)
    {
        gameObject.GetComponent<MeshFilter>().mesh = m_buildingMesh[buildingProperties];
        gameObject.GetComponent<Renderer>().material = m_buildingMaterials[buildingProperties];
    }

    void ReleaseEnergy(float energyAmount)
    {
        m_energyDepot -= energyAmount;
        m_coreGame.m_storedEnergy += energyAmount;
        GetProductionEffect(energyAmount);
    }

    void GetProductionEffect(float value)
    {
        m_coreGame.SendProductionEffect(gameObject.transform, ResourceProductionEffect.ResourceTypes.Energy, value);
    }
}
