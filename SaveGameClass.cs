using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveGameClass : MonoBehaviour
{
	public CoreGame m_coreGameObject;
	public SpaceShipBehaviour m_spaceShipObject;

    #region resources
    private string m_resourcesString;
    public List<GameObject> m_resourceObjects;
	#endregion

	public void LoadPlayer()
	{

	}

	public void LoadResources()
	{

	}

	public void LoadBuildings()
	{

	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameinfo.dat");

        GameInfo data = new GameInfo();
        // fill the data 
        // data. ....

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath+"/gameifo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameifo.dat",FileMode.Open);
            GameInfo data = (GameInfo)bf.Deserialize(file);
            file.Close();

            // file data in local variables
            // variable x = data.x
        }
    }
}

[Serializable]
class GameInfo
{

}