using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CorePlayer : MonoBehaviour
{
	public string m_playerFriendlyName ="Jade";
	
	public enum PlayerJobClasses {ScienceOfficer, BioMedicalOfficer, ChiefEngineer}
	public PlayerJobClasses m_playerJob = PlayerJobClasses.ScienceOfficer;
	public enum PlayerGenders {male, female, undefined}
	public PlayerGenders m_playerGender = PlayerGenders.undefined;


	public void SetJobClass( PlayerJobClasses playerJobClass)
	{
		m_playerJob = playerJobClass;
	}

	public void SetPlayerGender(PlayerGenders playerGender)
	{
		m_playerGender = playerGender;
	}

	public void SetPlayerName (string playerName)
	{
		m_playerFriendlyName = playerName;
	}
}