using UnityEngine;
using System.Collections;

public class BuildingButton : MonoBehaviour
{
    public CoreGame m_coreGameObject;
    public CoreGame.BuildingTypes m_buildingType;

    public void SetBuildingType()
    {
        m_coreGameObject.m_buildingType = m_buildingType;
    }
}
