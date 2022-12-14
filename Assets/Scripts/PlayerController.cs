using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player attributes\\
    public float speed = 5.0f;



    private Rigidbody playerRb;
    private GameObject focalPoint;
    private AudioSource playerAudioSource;
    
    public AudioClip enemyHitAudio;
    private float lowerBound = 15.0f;
    public bool gameOver = false;
    [SerializeField] private bool isGrounded = true;

    [SerializeField] private float normalHit = 3.0f;
    

    //UI
    public GameObject restartGameButton;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI gemCount;
    public TextMeshProUGUI gemScore;
    public TextMeshProUGUI jumpInstructions;
    public TextMeshProUGUI powerUpTimerText;
    public float powerUpTimer;

    // powerUp \\
    public bool hasPowerUp = false;
    public float powerupStrength = 15.0f;
    public float powerupRotationSpeed = 30.0f;
    public GameObject powerupIndicator;
    public AudioClip powerupAudio;

    //jump\\
    public bool hasJump = false;
    public float jumpIndicatorRotationSpeed = -15.0f;
    public GameObject jumpIndicator;
    public AudioClip jumpAudio;

    //pointer\\
    public GameObject pointer;
    private Vector3 pointerLocalScale;
    private float pointerMultiplier = 2f;


    void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        pointerLocalScale = pointer.transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        restartGameButton.SetActive(false);
        highScore.gameObject.SetActive(false);

        //attributes load\\
        playerRb.mass = PlayerDataManager.Instance.Mass;
        powerupStrength = PlayerDataManager.Instance.PowerupForce;
        speed = PlayerDataManager.Instance.Speed;
        normalHit = PlayerDataManager.Instance.Strength;

    }

    // Update is called once per frame
    void Update()
    {
        //UI\\
        gemCount.text = MainManager.Instance.getTotalGems().ToString();
        powerUpTimer += Time.deltaTime;
        powerUpTimerText.text = powerUpTimer.ToString("F2");

        //if (!isGrounded)
        //{
        //    speed /= 2;
        //}
        //else
        //{
        //    float forwardInput = Input.GetAxis("Vertical"); //for up and down
        //    playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed * Time.deltaTime);
        //    float rightInput = Input.GetAxis("Horizontal");
        //    playerRb.AddForce(focalPoint.transform.right * rightInput * speed * Time.deltaTime);
        //}


        //powerUp RING\\
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        powerupIndicator.transform.Rotate(Vector3.up, powerupRotationSpeed * Time.deltaTime, Space.Self);

        jumpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        jumpIndicator.transform.Rotate(Vector3.up, jumpIndicatorRotationSpeed * Time.deltaTime, Space.Self);



        pointer.transform.position = new Vector3(transform.position.x, -0.659f, transform.position.z);

        if (transform.position.y > 0.1f)
        {
            pointer.transform.localScale = (pointerLocalScale * Vector3.Distance(pointer.transform.position, transform.position) * pointerMultiplier);
        }
        else
        {
            pointer.transform.localScale = (pointerLocalScale * pointerMultiplier);
        }

        if (Input.GetKeyDown(KeyCode.Space) && hasJump)
        {
            playerRb.AddForce(Vector3.up * 500);
            hasJump = false;
            jumpIndicator.gameObject.SetActive(false);
        }

        //GAME OVER\\
        //GOTTA OPTIMIZE\\
        if (CheckBounds())
        {
            if (CheckHighScore())
            {
                MainManager.Instance.SaveWave(SpawnManager.waveCount);
            }

            

            restartGameButton.SetActive(true);
            highScore.text = "High Score: " + MainManager.Instance.LoadWave();
            highScore.gameObject.SetActive(true);

            SaveGems();

            Destroy(gameObject);
            Destroy(pointer);
            gameOver = true;
        }
    }

    private bool CheckBounds()
    {
        return transform.position.y < -lowerBound;
    }

    private bool CheckHighScore()
    {
        return SpawnManager.waveCount > MainManager.Instance.LoadWave();
    }

    private void SaveGems()
    {
        if (!gameOver)
        {
            if (SpawnManager.waveCount > 1)
            {
                int gemsEarned;
                if (MainManager.Instance.difficulty == 1) //if easy
                {
                    gemsEarned = (SpawnManager.waveCount - 1) * 2;
                }
                else //if hard
                {
                    gemsEarned = (SpawnManager.waveCount - 1) * 3;
                }
                
                gemScore.text = "Gems Earned: " + gemsEarned;
                MainManager.Instance.setTotalGems(gemsEarned);
                gemScore.transform.parent.gameObject.SetActive(true);
            }

            MainManager.Instance.SaveGems();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp") && !hasPowerUp)
        {
            hasPowerUp = true;
            playerAudioSource.PlayOneShot(powerupAudio, 2.0f);
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);

            StartCoroutine(PowerupCountdownRoutine());
        }

        if (other.CompareTag("Gem"))
        {
            MainManager.Instance.setTotalGems(1);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Jump") && !hasJump)
        {
            hasJump = true;
            playerAudioSource.PlayOneShot(jumpAudio, 2.0f);
            jumpIndicator.SetActive(true);
            Destroy(other.gameObject);
            jumpInstructions.gameObject.SetActive(true);
            jumpInstructions.CrossFadeAlpha(0, 3f, true);

        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        powerUpTimer = 0;
        powerUpTimerText.gameObject.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        powerupIndicator.SetActive(false);
        hasPowerUp = false;
        powerUpTimerText.gameObject.transform.parent.gameObject.SetActive(false);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

        if (isEnemy(collision.gameObject) && hasPowerUp)
        {
            playerAudioSource.PlayOneShot(enemyHitAudio);
            
            if (collision.gameObject.CompareTag("Boss"))
            {
                enemyRigidBody.AddForce(awayFromPlayer * powerupStrength * 3, ForceMode.Impulse);
            }
            else
            {
                enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }

        } 
        else if (isEnemy(collision.gameObject) && !hasPowerUp)
        {
            enemyRigidBody.AddForce(awayFromPlayer * normalHit, ForceMode.Impulse);
        }



        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private bool isEnemy(GameObject collision)
    {
        return (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"));
    }


    //Skill System \\
    public void IncrementMass()
    {
        playerRb.mass += 0.5f;
    }

    
    public void IncrementVolume()
    {
        
    }

    public void IncrementSpeed()
    {
        
    }
}
