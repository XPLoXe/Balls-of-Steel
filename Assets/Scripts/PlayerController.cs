using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private AudioSource playerAudioSource;
    public AudioClip powerupAudio;
    public AudioClip enemyHitAudio;
    private float lowerBound = 15.0f;
    public bool gameOver = false;
    

    //UI
    public GameObject restartGameButton;
    public TextMeshProUGUI highScore;

    // powerUp \\
    public bool hasPowerUp = false;
    public float powerupStrength = 10.0f;
    public float powerupRotationSpeed = 30.0f;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        restartGameButton.SetActive(false);
        highScore.gameObject.SetActive(false);
        playerRb = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical"); //for up and down
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        float rightInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.right * rightInput * speed);

        //powerUp RING\\
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        powerupIndicator.transform.Rotate(Vector3.up, powerupRotationSpeed * Time.deltaTime, Space.Self);

        if (transform.position.y < -lowerBound)
        {
            if (SpawnManager.waveCount > MainManager.Instance.LoadWave())
            {
                MainManager.Instance.SaveWave(SpawnManager.waveCount);
            }
           
            restartGameButton.SetActive(true);
            highScore.text = "High Score: " + MainManager.Instance.LoadWave();
            highScore.gameObject.SetActive(true);
            gameOver = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
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
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5f);
        powerupIndicator.SetActive(false);
        hasPowerUp = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            playerAudioSource.PlayOneShot(enemyHitAudio);
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

            Debug.Log("Collided with: " + collision.gameObject.name + " With Power Up set to " + hasPowerUp);
        }
    }
}
