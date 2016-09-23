using UnityEngine;
using UnityEngine.UI;

public class MarkerBehaviour : MonoBehaviour
{
    public float m_turnMultiplicator = 1.0f;
    public Color m_markerColour = new Color();
    public float m_markerFadeTime = 1.0f;
	
    void Start()
    {
        GetComponent<Renderer>().material.color = m_markerColour;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.Rotate(0.0f, m_turnMultiplicator * Time.deltaTime, 0.0f);
    }

    public void FadeMarker()
    {
        if (m_markerColour.a == 0.0f)
        {
            m_markerColour = new Color(m_markerColour.r, m_markerColour.g, m_markerColour.b, Mathf.Lerp(0.0f, 1.0f, m_markerFadeTime));
        }
        else if(m_markerColour.a == 1.0f)
        {
            m_markerColour = new Color(m_markerColour.r, m_markerColour.g, m_markerColour.b, Mathf.Lerp(1.0f, 0.0f, m_markerFadeTime));
        }
    }
}