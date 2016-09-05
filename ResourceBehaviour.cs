using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ResourceBehaviour : MonoBehaviour
{
    public bool m_debugScript = false;
    public bool m_manualPurge = false;
    public GameObject m_coreGameObject;
    
    public enum ResourceTypes {ScrapMetal, Ice, Rock, AsteroidMetal, Crystal, Food}
    public ResourceTypes m_resourceType;

    public enum ResourceBreakdownTypes {Shrinks, Splits, Produced}
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
    // Produceable resource
    public float m_productionTime;
    public bool m_automatedProduction = false;
    
    public bool m_timerActive = false;
    public bool m_isInteractedWithByPlayer = false;

    public float m_gatheringTime;


    // Use this for initialization
    void Start ()
    {
        m_resourceBreakdownModel = new List<Transform>();
        InitialiseResourceBrakedown();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if(m_timerActive && m_automatedProduction)
        {
            m_gatheringTime -= 1 * Time.deltaTime;

            if( m_gatheringTime < 0.0f)
            {
                m_timerActive = false;
                m_gatheringTime = m_coreGameObject.GetComponent<CoreGame>().m_gatherResourceTime;
                /*if( m_isConsumable)
                {
                    Destroy(gameObject);
                }*/
            }
        }

        if(m_manualPurge)
        {
            PurgeResource();
            m_manualPurge = false;
        }

        if(m_isInteractedWithByPlayer)
        {
            switch(m_resourceBreakdownType)
            {
                case ResourceBreakdownTypes.Produced :
                break;

                case ResourceBreakdownTypes.Shrinks :
                    AdjustResourceHeight();
                break;

                case ResourceBreakdownTypes.Splits :
                break;
            }
        }
    }

    void InitialiseResourceBrakedown()
    {
         m_gatheringTime = m_coreGameObject.GetComponent<CoreGame>().m_gatherResourceTime;

        switch (m_resourceBreakdownType)
        {
            case ResourceBreakdownTypes.Splits :
                foreach( Transform fragment in GetComponentInChildren<Transform>())
                {
                    m_resourceBreakdownModel.Add(fragment);
                    fragment.gameObject.GetComponent<Renderer>().enabled = false;
                    fragment.gameObject.GetComponent<Collider>().enabled = false;   
                }
                m_breakdownCount = m_resourceBreakdownModel.Count();
                if(m_debugScript)
                {
                    Debug.Log(m_breakdownCount);
                }
                m_resourceBreakdownTreshold = m_resourceAmmountMaximum / m_breakdownCount;
                m_resourceBreakdownModel.OrderBy( Transform => Transform.transform.position.z);
                PurgeResource();
            break;

            case ResourceBreakdownTypes.Shrinks :
                // Adjust heigth of object depending on the maximunm amount of available resources
                m_resourceTransformPositionY = gameObject.transform.position.y;
                AdjustResourceHeight();
            break;

            case ResourceBreakdownTypes.Produced :
                // Do fancy things if the resource is produced e.g. in the greenhouse
            break;
        }
       
    }

    void PurgeResource()
    {
        if(m_debugScript)
        {
            Debug.Log("Purging Resource");
        }
        //m_resourceBreakdownModel.OrderBy( Transform => Transform.transform.position.z);
        int tempResourceCount = Mathf.RoundToInt(m_resourceAmmount / m_resourceBreakdownTreshold);
        if(m_debugScript)
        {
            Debug.Log("CurrentFragmentCount: "+ tempResourceCount);
        }
        for (int b = 0; b < m_breakdownCount; b++)
        {
            m_resourceBreakdownModel[b].gameObject.GetComponent<Renderer>().enabled = false;
            m_resourceBreakdownModel[b].gameObject.GetComponent<Collider>().enabled = false;
        }
        for (int a = 0; a <= tempResourceCount; a++)
        {
            m_resourceBreakdownModel[a].gameObject.GetComponent<Renderer>().enabled = true;
            m_resourceBreakdownModel[a].gameObject.GetComponent<Collider>().enabled = true;
        }
        
    }

    void AdjustResourceHeight()
    {
        m_shrinkOffset = m_resourceAmmount / m_resourceAmmountMaximum;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,m_resourceTransformPositionY * m_shrinkOffset, gameObject.transform.position.z);
        if(m_debugScript)
        {
            Debug.Log("Adjusting resource height: Offset = "+ m_shrinkOffset+"; Y-Position ="+m_resourceTransformPositionY*m_shrinkOffset);
        }
    }
}