using UnityEngine;
using System.Collections;

public class PreviewBuilding : MonoBehaviour
{

    public Material m_materialOccupied;
    public Material m_materialFree;
    // Use this for initialization
    void Start ()
    {	
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnColliderEnter(Collider other)
    {
        if(other.gameObject.tag != "Ground")
        {
            gameObject.GetComponent<Renderer>().material = m_materialOccupied;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = m_materialFree;
        }
    }
}
