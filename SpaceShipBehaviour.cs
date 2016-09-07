using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpaceShipBehaviour : MonoBehaviour
{
	public List<GameObject> m_spaceShipParts;
	public float m_spaceShipHullintegrity = 0.0f;
	public float m_spaceShipUseability = 0.0f;

	public bool m_aiCoreLive = false;
	public bool m_reactorDriveLive = false;

	public enum AiCoreSensorLevels{Offline, Level1, Level2, Level3, Level4}
	public AiCoreSensorLevels m_currentAiCoreSensorLevel;
	public float m_sensorScanCooldownTime = 30.0f;
	private float tempSensorTime = 0.0f;
	public bool m_sensorScanReady = true;

	void Start()
	{
		m_spaceShipParts = new List<GameObject>();
		foreach(Transform shipPart in GetComponentInChildren<Transform>())
		{
			m_spaceShipParts.Add(shipPart.gameObject);
		} 
	}

	public FixedUpdate()
	{
		// Player can scan once the AiCore is live meaning the sensor can be utilised
		if(!m_sensorScanReady && m_currentAiCoreSensorLevel != AiCoreSensorLevels.Offline)
		{
			if(tempSensorTime < m_sensorScanCooldownTime)
			{
				tempSensorTime += 1.0f*Time.deltaTime;
			}
			else if( tempSensorTime >= m_sensorScanCooldownTime)
			{
				tempSensorTime = 0.0f;
				m_sensorScanReady = true; 
			}
		}
	}

	public void ScanEnvironment()
	{
		// scan environment
		switch(m_currentAiCoreSensorLevel)
		{
			case AiCoreSensorLevels.Level1 :
			// can detect and identify resources x units from ship
			break;

			case AiCoreSensorLevels.Level2 :
			// can detect and identify resources xx units from ship
			// can detect pods of other astronauts xx units from ship
			break;

			case AiCoreSensorLevels.Level3 :
			// can detect and identify resources xxx units from ship
			// can detect pods of other astronauts xxx units from ship
			// can detect previous civilization
			break;

			case AiCoreSensorLevels.Level4 :
			// can highlight previous civilization pictograms
			// can understand pictograms
			break;

		}
	}

	public void DetermineSpaceShipUseability()
	{
		int tempfixedParts = 0;
		for( int a = 0; a < m_spaceShipParts.Count(); a++)
		{
			if(m_spaceShipParts[a].GetComponent<SpaceShipPart>().m_spaceShipPartCondition == SpaceShipPart.Conditions.FinishedRepair)
			{
				tempfixedParts +=1;
			}
		}
		m_spaceShipHullintegrity = tempfixedParts/m_spaceShipParts.Count()*100.0f;
	}
}