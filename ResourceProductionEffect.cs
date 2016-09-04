using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceProductionEffect : MonoBehaviour
{
    public GameObject m_coreGameObject;
    public Canvas m_interface;
	public enum ResourceTypes {Energy}
    public ResourceTypes m_resourceType;
    public Sprite[] m_resourceIcons = new Sprite[4];
    public Color[] m_resourceColour = new Color[4];

    public Image m_resourceIcon;
    public Text m_resourceText;
    public float m_lifetime = 5.0f;

    public Transform m_targetObject;
    public float m_effectMoveSpeed;
    float m_offset = 0.0f;

    void Awake()
    {  
    }
	void FixedUpdate ()
    {
	    if(m_lifetime > 3.0f)
        {
            m_lifetime -= 1.0f * Time.deltaTime;
        }
        if( m_lifetime <= 3.0f)
        {
            m_lifetime -= 1.0f * Time.deltaTime;
            AdjustTransparency();
        }
        m_offset += m_effectMoveSpeed * Time.fixedDeltaTime;
        SetEffectPosition(m_targetObject);

        if (m_lifetime <= 0.0f)
        {
            m_lifetime = 5.0f;
            m_offset = 0.0f;
            ReturnProductionEffect();
        }
    }

    public void SetResource(ResourceTypes resource, float value)
    {
        switch(resource)
        {
            case ResourceTypes.Energy:
                m_resourceIcon.sprite = m_resourceIcons[0];
                m_resourceIcon.color = m_resourceColour[0];
                m_resourceText.text = "+" + value;
                m_resourceText.color = m_resourceColour[0];
            break;
        }
    }

    void AdjustTransparency()
    {
        m_resourceIcon.color = new Color(m_resourceIcon.color.r, m_resourceIcon.color.g, m_resourceIcon.color.b, m_resourceIcon.color.a - 1 * Time.fixedDeltaTime);
        m_resourceText.color = new Color(m_resourceText.color.r, m_resourceText.color.g, m_resourceText.color.b, m_resourceText.color.a - 1 * Time.fixedDeltaTime);
    }
    
    public void InitializePosition(Transform spawnObject)
    {
        SetEffectPosition(spawnObject);
    }

    void SetEffectPosition(Transform spawnObject)
    {
        RectTransform canvasRect = m_interface.GetComponent<RectTransform>();
        Vector2 viewportPosition = (Camera.main.WorldToViewportPoint(spawnObject.localPosition));
        Vector2 worldObject_ScreenPosition = new Vector2(((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)), ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)) + m_offset);
        transform.localPosition = worldObject_ScreenPosition;
    }

    void ReturnProductionEffect()
    {
        m_coreGameObject.GetComponent<CoreGame>().ReturnUsedProductionEffect(gameObject);
    }
}
