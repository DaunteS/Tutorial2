using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public Text score;
    public Text lives;
    public Text win;
    public AudioClip musicClip;
    public AudioClip musicClip2;
    public AudioSource musicSource;

    private int scoreValue = 0;
    private int livesValue = 3;
    


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score:" + scoreValue.ToString();
        lives.text = "Lives:" + livesValue.ToString();
        musicSource.clip = musicClip;
        musicSource.Play();
        musicSource.loop = true;
    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 5)
            {
                transform.position = new Vector2(51.0f, 0.0f);
                livesValue = 3;
                lives.text = "Lives:" + livesValue.ToString();
            }
        }
        else if (collision.collider.tag == "Enemy")
        {
            livesValue -=1;
            lives.text = "Lives:" + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
     if (scoreValue == 9)
     {
        win.text = "You win! Game by Daunte Smith";
        musicSource.loop = false;
        musicSource.clip = musicClip2;
        musicSource.Play();
        Destroy(this);
     }
     if (livesValue == 0)
     {
        win.text = "You lose...";
        Destroy(this);
     }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
