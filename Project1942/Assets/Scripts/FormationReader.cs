using UnityEngine;
using System.Collections;
using System.Xml;

public class FormationReader : MonoBehaviour
{
    public GameObject shipPrefab;
    public GameObject healthPrefab;
    public GameObject fireRatePrefab;

    public string fileName = "Formation";

    XmlDocument XMLFile;

    struct ObjectSpawn
    {
        public GameObject objectType;
        public Vector3 postion;
    }

    struct Wave
    {
        public float time;
        public ArrayList objects;
    }


    ArrayList listOflevels;
    // Use this for initialization
    void Start()
    {

        GetFile();
       listOflevels =  ReadLevels();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawning();
    }


    void UpdateSpawning()
    {
        if (listOflevels != null && listOflevels.Count > 0)
        {
            ArrayList currentLevel = (ArrayList)listOflevels[0];

            UpdateCurrentLevel(currentLevel);
        }
        // else the player has finihsed all levels and has won  

    }


    void UpdateCurrentLevel(ArrayList currentLevel)
    {
        if (currentLevel != null && currentLevel.Count > 0)
        {
            Wave wave = (Wave)currentLevel[0];

            if (Time.timeSinceLevelLoad > wave.time)
            {
                int size = wave.objects.Count;
                Debug.Log("spawn wave objects = " + size);
                for (int i = 0; i < size; i++)
                {
                    SpawnObject((ObjectSpawn)wave.objects[i]);
                }


                currentLevel.RemoveAt(0);
            }
        }
    }

    void SpawnObject(ObjectSpawn objectToSpawn)
    {
        Instantiate(objectToSpawn.objectType, objectToSpawn.postion, objectToSpawn.objectType.transform.rotation);
    }

    /// <summary>
    ///  reading from files 
    /// </summary>
    void GetFile()
    {
        XMLFile = new XmlDocument();
        string filepath = Application.dataPath + "/Data/" + fileName + ".xml";
        XMLFile.Load(filepath);

    }

    ArrayList ReadLevels()
    {
        ArrayList level = new ArrayList();

        if (XMLFile != null)
        {
            XmlNodeList levelsList = XMLFile.GetElementsByTagName("Level"); // array of the level nodes.

            foreach (XmlNode levelInfo in levelsList)
            {
                level.Add(ReadWavesInLevel(levelInfo));
            }
        }

        return level;
    }

    ArrayList ReadWavesInLevel(XmlNode levelInfo)
    {
        ArrayList level = new ArrayList();

        XmlNodeList levelcontent = levelInfo.ChildNodes;

        foreach (XmlNode levelsNodes in levelcontent)
        {
            level.Add(ReadWave(levelsNodes));
        }

        return level;
    }


    Wave ReadWave(XmlNode levelsNodes)
    {
        XmlNodeList waveList = levelsNodes.ChildNodes;

        Wave wave = new Wave();


        wave.objects = new ArrayList();

        foreach (XmlNode waveNode in waveList)
        {

            ReadWaveNode(waveNode, ref wave);
        }

        return wave;
    }


    void ReadWaveNode(XmlNode waveNode, ref Wave wave)
    {
        if (string.Compare(waveNode.Name, "Time") == 0)
        {

            wave.time = float.Parse(waveNode.FirstChild.Value);
         }

        if (string.Compare(waveNode.Name, "Ship") == 0)
        {
            float pos = float.Parse(waveNode.FirstChild.Value);

             ObjectSpawn objectSpawn = new ObjectSpawn();

            objectSpawn.objectType = shipPrefab;
            objectSpawn.postion = Camera.main.ViewportToWorldPoint(new Vector3(pos/100.0f, 1.0f, 10.0f));
        
            wave.objects.Add(objectSpawn);
        }


        if (string.Compare(waveNode.Name, "FireRate") == 0)
        {
            float pos = float.Parse(waveNode.FirstChild.Value);

            ObjectSpawn objectSpawn = new ObjectSpawn();

            objectSpawn.objectType = fireRatePrefab;
            objectSpawn.postion = Camera.main.ViewportToWorldPoint(new Vector3(pos / 100.0f, 1.0f, 10.0f));

            wave.objects.Add(objectSpawn);
        }


        if (string.Compare(waveNode.Name, "Health") == 0)
        {
            float pos = float.Parse(waveNode.FirstChild.Value);

            ObjectSpawn objectSpawn = new ObjectSpawn();

            objectSpawn.objectType = healthPrefab;
            objectSpawn.postion = Camera.main.ViewportToWorldPoint(new Vector3(pos / 100.0f, 1.0f, 10.0f));

            wave.objects.Add(objectSpawn);
        }
        
    }

   

}


