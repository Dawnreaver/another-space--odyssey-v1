using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectPoolingClass
{
    GameObject m_coreGameObject;
    GameObject m_coreBuilding;

    GameObject m_resourceProductionEffect;

    GameObject m_guiObject;

    public int m_poolIncrement = 5;

    public int m_buildingPoolSize = 30;
    public int m_resourceProductionEffectPoolSize = 15;

    public List<GameObject> m_buildingPool;
    public List<GameObject> m_resourceProductionEffectPool;

    public ObjectPoolingClass(GameObject coreGame, GameObject guiObject, GameObject building, GameObject effect)
    {
        Debug.Log("initialized object pooling class");
        m_coreGameObject = coreGame;
        m_coreBuilding = building;
        m_resourceProductionEffect = effect;
        m_guiObject = guiObject;
        InitializePools();
    }

    void InitializePools()
    {
        m_buildingPool = new List<GameObject>();
        m_resourceProductionEffectPool = new List<GameObject>();
        for (int i = 0; i < m_buildingPoolSize; i++)
        {
            AddBuildingToPool();
        }
        for (int j = 0; j < m_resourceProductionEffectPoolSize; j++)
        {
            AddResourceProductionEffectToPool();
        }
    }

    public void AddBuildingToPool()
    {
        GameObject building = UnityEngine.GameObject.Instantiate(m_coreBuilding);

        building.name = "CoreBuilding";
        building.transform.SetParent(m_coreGameObject.transform);
        building.transform.position = m_coreGameObject.transform.position;
        building.SetActive(false);
        m_buildingPool.Add(building);
    }

    public void AddResourceProductionEffectToPool()
    {
        GameObject productionEffect = UnityEngine.GameObject.Instantiate(m_resourceProductionEffect);
        ResourceProductionEffect productionEffectComponent = productionEffect.GetComponent<ResourceProductionEffect>();

        productionEffect.name = "ProductionEffect";
        productionEffect.transform.SetParent(m_guiObject.gameObject.transform);
        productionEffectComponent.m_coreGameObject = m_coreGameObject;
        productionEffectComponent.m_interface = m_guiObject.gameObject.GetComponent<Canvas>();
        productionEffect.SetActive(false);
        m_resourceProductionEffectPool.Add(productionEffect);

    }
}
