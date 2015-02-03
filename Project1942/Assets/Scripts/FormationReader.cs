using UnityEngine;
using System.Collections;
using System.Xml;

public class FormationReader : MonoBehaviour
{
    public GameObject shipPrefab;
    public GameObject healthPrefab;
    public GameObject fireRatePrefab;

    public GameObject playerPrefab;



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


    ArrayList level;

    bool playerSpawned = false;
    bool startWave = true;
    // Use this for initialization
    void Start()
    {

        GetFile();
        level = ReadWavesInLevel(XMLFile.FirstChild);


    }
    
    // Update is called once per frame
    void Update()
    {
        if (ObjectPool.instance.finishedPooling)
        {
            if (playerSpawned == false)
            {
                playerSpawned = true;
                Instantiate(playerPrefab, transform.position, transform.rotation);
            }

            UpdateSpawning();

        }
    }


    void UpdateSpawning()
    {
        if (level != null && level.Count > 0)
        {

            UpdateCurrentLevel(level);
        }

    }
    void UpdateCurrentLevel(ArrayList currentLevel)
    {
        if (currentLevel != null && currentLevel.Count > 0)
        {
            if (startWave == true)
            {
                startWave = false;
                GameProgressHandler.instance.StartLevel(currentLevel.Count);
            }

            Wave wave = (Wave)currentLevel[0];

            if (Time.timeSinceLevelLoad > wave.time)
            {
                int size = wave.objects.Count;
                //  Debug.Log("spawn wave objects = " + size);
                for (int i = 0; i < size; i++)
                {
                    SpawnObject((ObjectSpawn)wave.objects[i]);
                }

                GameProgressHandler.instance.WaveFinished();
                currentLevel.RemoveAt(0);
            }
        }
    }
    void SpawnObject(ObjectSpawn objectToSpawn)
    {
        GameObject spawn = ObjectPool.instance.GetObject(objectToSpawn.objectType.name);
        spawn.transform.position = objectToSpawn.postion;
        spawn.transform.rotation = objectToSpawn.objectType.transform.rotation;
        spawn.SetActive(true);
        spawn.SendMessage("Init", SendMessageOptions.DontRequireReceiver);

    }

    /// <summary>
    ///  reading from files 
    /// </summary>
    void GetFile()
    {
        XMLFile = new XmlDocument();
        TextAsset file = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
        XMLFile.LoadXml(file.text);

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
            objectSpawn.postion = Camera.main.ViewportToWorldPoint(new Vector3(pos / 100.0f, 1.0f, 10.0f));

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


