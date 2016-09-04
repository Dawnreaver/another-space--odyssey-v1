using UnityEngine;
using System.Collections;

public class ResourceBehaviour : MonoBehaviour
{
    public GameObject m_coreGameObject;
    //public TextMesh m_label;
    public enum ResourceTypes {ScrapMetal, Ice, Rock, AsteroidMetal, Crystal, Food}
    public ResourceTypes m_resourceType;
    public bool m_isConsumable = false;
    public bool m_timerActive = false;
    public bool m_isInteractedWithByPlayer = false;

    public float m_gatheringTime;


    // Use this for initialization
    void Start ()
    {
        m_gatheringTime = m_coreGameObject.GetComponent<CoreGame>().m_gatherResourceTime;
        //m_label.text = "";
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if(m_timerActive)
        {
            m_gatheringTime -= 1 * Time.deltaTime;
            //m_label.text = (int) m_gatheringTime + "";

            if( m_gatheringTime < 0.0f)
            {
                m_timerActive = false;
                m_gatheringTime = m_coreGameObject.GetComponent<CoreGame>().m_gatherResourceTime;
                //m_label.text ="";
                if( m_isConsumable)
                {
                    Destroy(gameObject);
                }
            }
        }
	}

    void OnMouseEnter()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            m_timerActive = true;
        }
    }
}
