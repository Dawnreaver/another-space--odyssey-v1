using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpaceShipBehaviour : MonoBehaviour
{
	public string m_spaceShipFriendlyName = "Athena";
	public List<GameObject> m_spaceShipParts;
	public float m_spaceShipHullintegrity = 0.0f;
	public float m_spaceShipUseability = 0.0f;

	public bool m_aiCoreLive = false;
	public bool m_reactorDriveLive = false;

	public enum AiCoreSensorLevels{Offline, SensorLevel1, SensorLevel2, SensorLevel3, SensorLevel4}
	public AiCoreSensorLevels m_currentAiCoreSensorLevel;
	public float m_sensorScanCooldownTime = 30.0f;
	private float tempSensorTime = 0.0f;
	public bool m_sensorScanReady = true;

	public float m_spaceShipStorageCapacity = 250.0f;
	public enum SpaceShipStorageLevels{ StorageLevle1, StorageLevel2, StorageLevel3}
	public SpaceShipStorageLevels m_spaceShipStrorageLevel;


	void Start()
	{
		m_spaceShipParts = new List<GameObject>();
		foreach(Transform shipPart in GetComponentInChildren<Transform>())
		{
			m_spaceShipParts.Add(shipPart.gameObject);
		}
		AdjustSpaceShipStorage();
	}

	void FixedUpdate()
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
	void AdjustSpaceShipStorage()
	{
		switch(m_spaceShipStrorageLevel)
		{
			case SpaceShipStorageLevels.StorageLevle1 :
				m_spaceShipStorageCapacity = 250.0f;
			break;

			case SpaceShipStorageLevels.StorageLevle2 :
				m_spaceShipStorageCapacity = 500.0f;
			break;

			case SpaceShipStorageLevels.StorageLevle3 :
				m_spaceShipStorageCapacity = 1000.0f;
			break;
		}
	}
	public void ScanEnvironment()
	{
		// scan environment
		switch(m_currentAiCoreSensorLevel)
		{
			case AiCoreSensorLevels.SensorLevel1 :
			// can detect and identify resources x units from ship
			break;

			case AiCoreSensorLevels.SensorLevel2 :
			// can detect and identify resources xx units from ship
			// can detect pods of other astronauts xx units from ship
			break;

			case AiCoreSensorLevels.SensorLevel3 :
			// can detect and identify resources xxx units from ship
			// can detect pods of other astronauts xxx units from ship
			// can detect previous civilization
			break;

			case AiCoreSensorLevels.SensorLevel4 :
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
			if(m_spaceShipParts[a].GetComponent<SpaceShipPart>().m_spaceShipPartCondition == SpaceShipPart.SpaceShipPartConditions.FinishedRepair)
			{
				tempfixedParts +=1;
			}
		}
		m_spaceShipHullintegrity = tempfixedParts/m_spaceShipParts.Count()*100.0f;
	}
}