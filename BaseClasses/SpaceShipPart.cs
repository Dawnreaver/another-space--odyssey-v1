using UnityEngine;

public class SpaceShipPart : MonoBehaviour
{
    public int m_spaceShipPartIdentifier = 0;
    public SpaceShipBehaviour m_spaceShip;
    public enum SpaceShipPartConditions { Broken, ReadyToRepair, FinishedRepair }
    public SpaceShipPartConditions m_spaceShipPartCondition;

    public bool m_isSelected = false;

    void FixedUpdate()
    {
        if (m_isSelected && m_spaceShipPartCondition == SpaceShipPartConditions.ReadyToRepair && gameObject.GetComponent<Renderer>().material != m_spaceShip.m_spaceShipPartMatrials[1])
        {
            gameObject.GetComponent<Renderer>().material = m_spaceShip.m_spaceShipPartMatrials[1];
        }
        else if (!m_isSelected)
        {
            switch (m_spaceShipPartCondition)
            {
                case SpaceShipPartConditions.Broken:
                    if (gameObject.GetComponent<Renderer>().material != m_spaceShip.m_spaceShipPartMatrials[0])
                    {
                        gameObject.GetComponent<Renderer>().material = m_spaceShip.m_spaceShipPartMatrials[0];
                    }
                    break;

                case SpaceShipPartConditions.FinishedRepair:
                    if (gameObject.GetComponent<Renderer>().material != m_spaceShip.m_spaceShipPartMatrials[2])
                    {
                        gameObject.GetComponent<Renderer>().material = m_spaceShip.m_spaceShipPartMatrials[2];
                    }
                    break;
            }
        }
    }
}