using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ResourceBehaviour : MonoBehaviour
{
    public string m_resourceFriendlyName ="Resource";
    public string m_resourceSystemName = "Resource001";
    public bool m_manualPurge = false;
    public GameObject m_coreGameObject;
    public GameObject m_buildingGrid;
    public GameObject m_buildingGridCell;
    
    public enum ResourceTypes { AsteroidMetal, Crystal, Ice, Rock, ScrapMetal}
    public ResourceTypes m_resourceType;

    public enum ResourceBreakdownTypes { Breaks, Shrinks}
    public ResourceBreakdownTypes m_resourceBreakdownType;
    
    public float m_resourceAmmount = 100.0f;
    public float m_resourceAmmountMaximum = 300.0f;
    // Splitable resource
    public List<Transform> m_resourceBreakdownModel;
    public float m_resourceBreakdownTreshold;
    public int m_breakdownCount;
    // Shrinkable resource
    public float m_resourceTransformPositionY;
    public float m_shrinkOffset = 0.0f;

    private float m_resourceDepot = 0.0f;

    // Produceable resource
    public float m_productionTime;
    
    public bool m_productionTimerActive = false;
    public bool m_isHarvestedByPlayer = false;

    public float m_gatheringTime;


    // Use this for initialization
    void Start ()
    {
        m_resourceBreakdownModel = new List<Transform>();
        InitialiseResourceBrakedown();
    }
	
	void FixedUpdate ()
    {
        if(m_manualPurge)
        {
            PurgeResource();
            m_manualPurge = false;
        }

        if(m_isHarvestedByPlayer)
        {  
            if (m_productionTimerActive && m_resourceAmmount > 0.0f)
            {
                m_gatheringTime -= 1 * Time.deltaTime;
                m_resourceDepot += 1 * Time.deltaTime;
                if (m_resourceBreakdownType == ResourceBreakdownTypes.Shrinks)
                {
                    m_resourceAmmount -= 1 * Time.deltaTime;
                    AdjustResourceHeight();
                }
                if ( m_gatheringTime < 0.0f)
                {
                    m_productionTimerActive = false;
                    m_gatheringTime = m_coreGameObject.GetComponent<CoreGame>().m_gatherResourceTime;
                }              
            }
            else if(!m_productionTimerActive && m_resourceAmmount > 0.0f && m_resourceDepot > 0.0f)
            {
                GetProductionEffect(m_resourceType, Mathf.Round(m_resourceDepot));
                m_coreGameObject.GetComponent<PlayerInventory>().AddResourceToInventory(m_resourceType, m_resourceDepot);

                if (m_resourceBreakdownType == ResourceBreakdownTypes.Breaks)
                { 
                    m_resourceAmmount -= m_resourceDepot;
                    PurgeResource();
                }
                m_resourceDepot = 0.0f;
            }
            if(!m_productionTimerActive && m_resourceAmmount <= 0.0f)
            {
                Debug.Log("Resource depleted!");
                if(m_resourceType != ResourceTypes.ScrapMetal)
                {
                    gameObject.SetActive(false);
                    SpawnBuildingGridCell();
                }
                else
                {
                    GetComponent<SpaceShipPart>().m_spaceShipPartCondition = SpaceShipPart.SpaceShipPartConditions.ReadyToRepair;
                    gameObject.GetComponent<Renderer>().enabled = false;
                }

                m_isHarvestedByPlayer = false;
            }
        }
    }

    void InitialiseResourceBrakedown()
    {
         m_gatheringTime = m_coreGameObject.GetComponent<CoreGame>().m_gatherResourceTime;

        switch(m_resourceBreakdownType)
        {
            case ResourceBreakdownTypes.Breaks :
                foreach( Transform fragment in GetComponentInChildren<Transform>())
                {
                    m_resourceBreakdownModel.Add(fragment);
                    fragment.gameObject.GetComponent<Renderer>().enabled = false;
                    fragment.gameObject.GetComponent<Collider>().enabled = false;   
                }
                m_breakdownCount = m_resourceBreakdownModel.Count();
                m_resourceBreakdownTreshold = m_resourceAmmountMaximum / m_breakdownCount;
                m_resourceBreakdownModel.OrderBy( Transform => Transform.transform.position.z);
                PurgeResource();
            break;

            case ResourceBreakdownTypes.Shrinks :
                // Adjust heigth of object depending on the maximunm amount of available resources
                m_resourceTransformPositionY = gameObject.transform.position.y;
                AdjustResourceHeight();
            break;
        }
    }

    public void StopHarvest()
    {
    	switch(m_resourceBreakdownType)
    	{
    		case ResourceBreakdownTypes.Breaks :
    			PurgeResource();
                m_isHarvestedByPlayer = false;
    		break;

    		case ResourceBreakdownTypes.Shrinks :
                // do something when shrinkable resource harveesting is stopped
                m_isHarvestedByPlayer = false;
    		break;
    	}	
    }

    void PurgeResource()
    {
        int tempResourceCount = Mathf.RoundToInt(m_resourceAmmount / m_resourceBreakdownTreshold);

        for(int b = 0; b < m_breakdownCount; b++)
        {
            m_resourceBreakdownModel[b].gameObject.GetComponent<Renderer>().enabled = false;
            m_resourceBreakdownModel[b].gameObject.GetComponent<Collider>().enabled = false;
        }

        for(int a = 0; a <= tempResourceCount; a++)
        {
            m_resourceBreakdownModel[a].gameObject.GetComponent<Renderer>().enabled = true;
            m_resourceBreakdownModel[a].gameObject.GetComponent<Collider>().enabled = true;
        }   
    }

    void AdjustResourceHeight()
    {
        m_shrinkOffset = m_resourceAmmount / m_resourceAmmountMaximum;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,m_resourceTransformPositionY * m_shrinkOffset, gameObject.transform.position.z);
    }

    void OnTriggerEnter( Collider other)
    {
    	if(other.gameObject.tag =="Player" || other.gameObject.tag == "Drone")
    	{
    		m_isHarvestedByPlayer = true;
    	}
    }

    void SpawnBuildingGridCell()
    {
        GameObject buildingGridHex = Instantiate(m_buildingGridCell, new Vector3(gameObject.transform.position.x, 0.0f, gameObject.transform.position.z), Quaternion.identity) as GameObject;
        buildingGridHex.name = "hexagon";
        buildingGridHex.transform.parent = m_buildingGrid.transform;
        m_coreGameObject.GetComponent<CoreGame>().m_constructionCells.Add(buildingGridHex);
    }

    void GetProductionEffect(ResourceTypes resourceType, float value)
    {
        switch (resourceType)
        {
            case ResourceTypes.AsteroidMetal:
                m_coreGameObject.GetComponent<CoreGame>().SendProductionEffect(gameObject.transform, ResourceProductionEffect.ResourceTypes.Metal, value);
            break;

            case ResourceTypes.Crystal:
                m_coreGameObject.GetComponent<CoreGame>().SendProductionEffect(gameObject.transform, ResourceProductionEffect.ResourceTypes.Crystal, value);
            break;

            case ResourceTypes.Ice:
                m_coreGameObject.GetComponent<CoreGame>().SendProductionEffect(gameObject.transform, ResourceProductionEffect.ResourceTypes.Water, value);
            break;

            case ResourceTypes.Rock:
                m_coreGameObject.GetComponent<CoreGame>().SendProductionEffect(gameObject.transform, ResourceProductionEffect.ResourceTypes.Rock, value);
            break;

            case ResourceTypes.ScrapMetal:
                m_coreGameObject.GetComponent<CoreGame>().SendProductionEffect(gameObject.transform, ResourceProductionEffect.ResourceTypes.Metal, value);
            break;
        }
    }
}