using UnityEngine;

public class ConstructionCell : MonoBehaviour
{
    public CoreGame m_CoreGameObject;
    public bool m_selected = false;
    public bool m_active = false;
    void Awake()
    {
        if(m_CoreGameObject == null)
        {
            m_CoreGameObject = GameObject.FindGameObjectWithTag("CoreGameObject").GetComponent<CoreGame>();
        }
    }
    void FixedUpdate()
    {
        if(m_selected && Input.GetButtonDown("Fire1"))
        {
            m_active = true;
            m_CoreGameObject.m_previewObject = gameObject;
            m_CoreGameObject.DisableNonActiveCells();
            m_CoreGameObject.m_inGameMenuLogic.OpenBuildMenu(gameObject.transform);
        }

        if(m_active || m_selected)
        {
            gameObject.GetComponent<MeshRenderer>().material = m_CoreGameObject.m_buildingMaterials[1];
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = m_CoreGameObject.m_buildingMaterials[0];
        }
    }

    void OnMouseOver()
    {
        m_selected = true;
    }

    void OnMouseExit()
    {
        m_selected = false;
    }

    public void SetCellIncative()
    {
        m_active = false;
    }

   
}
