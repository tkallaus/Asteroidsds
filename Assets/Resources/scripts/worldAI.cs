using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class worldAI : MonoBehaviour
{
    public UnityEngine.UI.Text UIscore;
    public GameObject UIgameOver;
    public GameObject UItryAgain;
    public GameObject UIstageNum;
    public GameObject UIpressStart;
    public GameObject UIwinMessage;
    public GameObject UIfinalTime;
    public GameObject UIescapeToExit;
    public GameObject UIpaused;
    public GameObject UItitle;
    private float UITimer = 2f;
    
    static public int score = 0;
    static public bool scoreUpdate = false;
    static public int stage = 0;
    static public int scoreForNextStage = 1000;
    private bool newStage = false;

    static public bool screenWrapActive = true;
    static public bool gameOver = false;
    static public bool isQuitting = false;
    static public bool paused = false;
    private float gameOverTimer = 4f;

    public GameObject pWrapper;

    private GameObject player;
    static public List<GameObject> obstacleEnemyList = new List<GameObject>();

    private float spawnTimer = 3f;

    private bool spawnSwitch = false;
    static public bool stage3Transition = false;
    static public bool hasBegun = false;
    private bool post3 = false;

    public GameObject startBackground;

    static public float HalfScreenWidth;
    static public float HalfScreenHeight;
    private Camera cam;
    static public Vector2 camGrid;
    static public Vector2 spawnPoint;

    public bool debugScore = false;
    public bool debugScreenwrap = false;
    public bool debugLoc = false;

    static public bool bossTime = false;

    private AudioSource[] sounds; //0 is music-high, 1 is music-low
    private float musicTimer;
    private bool musicBool;

    private float endTimer;
    static public bool theEnd = false;

    private Stopwatch speedrunTimer;
    private bool timerStarted = false;
    private bool timeUIThing = true;
    
    private void Awake()
    {
        player = FindObjectOfType<pController>().gameObject;
        cam = Camera.main;
        HalfScreenWidth = 18.596215f;
        HalfScreenHeight = 10f;
        camGrid = new Vector2(0, 0);
        spawnPoint = new Vector2(0, 0);

        musicBool = false;
        musicTimer = 1f;
        sounds = GetComponents<AudioSource>();

        endTimer = 9f;

        speedrunTimer = new Stopwatch();
    }

    void Update()
    {
        if (debugScore)
        {
            score += 1000;
            debugScore = false;
        }
        if (debugScreenwrap)
        {
            screenWrapActive = !screenWrapActive;
            debugScreenwrap = false;
        }
        if (debugLoc)
        {
            UnityEngine.Debug.Log(camGrid);
            debugLoc = false;
        }
        if (hasBegun && !gameOver)
        {
            if (!timerStarted)
            {
                speedrunTimer.Start();
                timerStarted = true;
            }
            if (scoreUpdate)
            {
                UIscore.text = score.ToString();
                scoreUpdate = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape))//add variable to disallow pause during transitions, or just makes sure it's always set to false when transitioning //or don't lol, doesn't seem to be a problem
            {
                if (!paused)
                {
                    paused = true;
                    Time.timeScale = 0;
                    UIpaused.SetActive(true);
                }
                else if (paused)
                {
                    paused = false;
                    Time.timeScale = 1;
                    UIpaused.SetActive(false);
                }
            }
            if (score >= scoreForNextStage)
            {
                if(stage == 3 && !stage3Transition)
                {
                    stage3Transition = true;
                    foreach (GameObject obsEnemy in obstacleEnemyList)
                    {
                        Destroy(obsEnemy);
                    }
                    obstacleEnemyList.Clear();
                    obstacleEnemyList.TrimExcess();
                    randActivate.staticGo = true;
                }
                else if(stage < 3)
                {
                    stageProgress();
                }
            }
            if (newStage && !post3)
            {
                newStageUI();
            }
            if (!spawnSwitch)
            {
                rngSpawning();
            }
            if (!screenWrapActive)
            {
                pWrapper.SetActive(false);
                Vector3 newPos = player.transform.position;

                if (newPos.x > transform.position.x + HalfScreenWidth)
                {
                    transform.position += new Vector3(HalfScreenWidth * 2, 0);
                    camGrid.x += 1;
                }
                if (newPos.x < transform.position.x - HalfScreenWidth)
                {
                    transform.position += new Vector3(-HalfScreenWidth * 2, 0);
                    camGrid.x -= 1;
                }
                if (newPos.y > transform.position.y + HalfScreenHeight)
                {
                    transform.position += new Vector3(0, HalfScreenHeight * 2);
                    camGrid.y += 1;
                }
                if (newPos.y < transform.position.y - HalfScreenHeight)
                {
                    transform.position += new Vector3(0, -HalfScreenHeight * 2);
                    camGrid.y -= 1;
                }
            }
            else
            {
                if (!bossTime)
                {
                    pWrapper.SetActive(true);
                }
                
            }
        }
        else if (gameOver)
        {
            gameOverMethod();
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            UIpressStart.SetActive(false);
            UItitle.SetActive(false);
            hasBegun = true;
            stageProgress();
        }
        if (bossFight.bossGo)
        {
            if(musicTimer < 0f && !musicBool)
            {
                sounds[1].Play();
                musicTimer = 1f;
                musicBool = true;
            }
            if (musicTimer < 0f && musicBool)
            {
                sounds[0].Play();
                musicTimer = 1f;
                musicBool = false;
            }
            musicTimer -= Time.deltaTime;
        }
        if (theEnd)
        {
            if (timerStarted)
            {
                speedrunTimer.Stop();
                timerStarted = false;
            }

            bossTime = false;
            endTimer -= Time.deltaTime;

            if(endTimer < 6f)
            {
                UIgameOver.SetActive(true);
            }
            if(endTimer < 3f)
            {
                UIwinMessage.SetActive(true);
            }
            if(endTimer < 0f)
            {
                UIfinalTime.SetActive(true);
                if (timeUIThing)
                {
                    UIfinalTime.GetComponent<UnityEngine.UI.Text>().text += speedrunTimer.Elapsed.ToString("hh\\:mm\\:ss\\.ff");
                    timeUIThing = false;
                }
                UIescapeToExit.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
            }
        }
        if (paused && Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();
        }
    }
    private void FixedUpdate()
    {
        if (bossTime)
        {
            if(transform.position.y < 70)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 70, -10), 0.1f);
            }
            if(cam.orthographicSize < 20)
            {
                cam.orthographicSize += 0.1f;
            }
            if (HalfScreenWidth == 18.596215f)
            {
                HalfScreenWidth *= 2;
            }
            if (HalfScreenHeight == 10f)
            {
                HalfScreenHeight *= 2;
            }
        }
    }
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void stageProgress()
    {
        stage++;
        score = 0;
        scoreUpdate = true;
        scoreForNextStage = stage * 1000;
        foreach(GameObject obsEnemy in obstacleEnemyList)
        {
            Destroy(obsEnemy);
        }
        obstacleEnemyList.Clear();
        obstacleEnemyList.TrimExcess();
        if (post3)
        {
            if (camGrid.x != 0)
            {
                transform.position += new Vector3(HalfScreenWidth * 2 * -camGrid.x, 0);
                camGrid.x = 0;
            }
            if (camGrid.y != 0)
            {
                transform.position += new Vector3(0, HalfScreenHeight * 2 * -camGrid.y);
                camGrid.y = 0;
            }
        }
        player.transform.position = transform.position;
        player.transform.position += Vector3.forward * 10;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        newStage = true;
    }
    private void gameOverMethod()
    {
        if (stage3Transition)
        {
            randActivate.staticGo = true;
            stage3Transition = false;
            screenWrapActive = false;
            UIgameOver.SetActive(false);
            UItryAgain.SetActive(false);
            gameOver = false;
            startBackground.SetActive(false);
            UIscore.gameObject.SetActive(false);
            player.SetActive(true);
            post3 = true;
            stageProgress();
        }
        else if (post3)
        {
            gameOverTimer -= Time.deltaTime;
            if (gameOverTimer <= 2f)
            {
                UIgameOver.SetActive(true);
            }
            if (gameOverTimer <= 0f)
            {
                UItryAgain.SetActive(true);
            }
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) && gameOverTimer <= 0f)
            {
                UIgameOver.SetActive(false);
                UItryAgain.SetActive(false);
                gameOver = false;
                gameOverTimer = 1f;

                if (bossTime)
                {
                    HalfScreenWidth = 18.596215f;
                    HalfScreenHeight = 10f;
                    cam.orthographicSize = 10;
                    transform.position -= new Vector3(0, 10);
                    bossTime = false;
                    foreach (GameObject obsEnemy in obstacleEnemyList)
                    {
                        Destroy(obsEnemy);
                    }
                    obstacleEnemyList.Clear();
                    obstacleEnemyList.TrimExcess();
                }
                if (camGrid.x != spawnPoint.x)
                {
                    transform.position += new Vector3(HalfScreenWidth * 2 * (spawnPoint.x-camGrid.x), 0);
                    camGrid.x = spawnPoint.x;
                }
                if (camGrid.y != spawnPoint.y)
                {
                    transform.position += new Vector3(0, HalfScreenHeight * 2 * (spawnPoint.y-camGrid.y));
                    camGrid.y = spawnPoint.y;
                }

                player.SetActive(true);
                player.transform.position = transform.position;
                player.transform.position += Vector3.forward * 10;
                player.transform.rotation = Quaternion.identity;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
        else
        {
            gameOverTimer -= Time.deltaTime;
            if (gameOverTimer <= 2f)
            {
                UIgameOver.SetActive(true);
            }
            if (gameOverTimer <= 0f)
            {
                UItryAgain.SetActive(true);
            }
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) && gameOverTimer <= 0f)
            {
                UIgameOver.SetActive(false);
                UItryAgain.SetActive(false);
                gameOver = false;
                gameOverTimer = 1f;
                player.SetActive(true);
                stage--;
                stageProgress();
            }
        }
    }
    private void newStageUI()
    {
        if(UITimer == 2f)
        {
            UIstageNum.GetComponent<UnityEngine.UI.Text>().text += stage;
        }
        UITimer -= Time.deltaTime;
        
        if (UITimer > 0f)
        {
            UIstageNum.SetActive(true);
        }
        else
        {
            UIstageNum.SetActive(false);
            UIstageNum.GetComponent<UnityEngine.UI.Text>().text = "STAGE ";
            UITimer = 2f;
            newStage = false;
        }
    }
    private void rngSpawning()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            Vector3 spawnPoint = Random.insideUnitCircle.normalized;
            if (spawnPoint == Vector3.zero)
            {
                spawnPoint.x = 1;
                spawnPoint.y = 1;
            }
            spawnPoint *= 20;
            spawnPoint.z += 10;
            if (stage3Transition)
            {
                Instantiate(Resources.Load("prefabs/obstacles/asteroidAlien"), transform.position + spawnPoint, Quaternion.identity);
                spawnSwitch = true;
            }
            else
            {
                switch (rngSpawnWeight())
                {
                    case 0:
                        Instantiate(Resources.Load("prefabs/obstacles/asteroidS" + Random.Range(0, 2)), transform.position + spawnPoint, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(Resources.Load("prefabs/obstacles/asteroidM" + Random.Range(0, 3)), transform.position + spawnPoint, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Resources.Load("prefabs/obstacles/asteroidL" + Random.Range(0, 1)), transform.position + spawnPoint, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Resources.Load("prefabs/enemies/enemyShip"), transform.position + spawnPoint, Quaternion.identity);
                        break;
                    default:
                        UnityEngine.Debug.Log("What? How?");
                        break;
                }
            }
            spawnTimer = 6f - stage;
        }
    }
    private int rngSpawnWeight()
    {
        int i = Random.Range(0,10);
        switch (stage)
        {
            case 1:
                if(i < 4)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            case 2:
                if(i < 1)
                {
                    return 0;
                }
                else if(i < 4)
                {
                    return 1;
                }
                else if(i < 8)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            case 3:
                if(i < 3)
                {
                    return 0;
                }
                else if(i < 6)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
        }
        return -1;
    }
    static public void particleSpawn(Vector3 pos)
    {
        GameObject p = Instantiate(Resources.Load("prefabs/effects/deathPuff") as GameObject, pos, Quaternion.identity);
        p.GetComponent<ParticleSystem>().Play();
    }
}
