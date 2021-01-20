using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charcter : MonoBehaviour
{
    int healthCount = 100;
    int coinCount = 0;
    int rand;
    int jumpcount = 0;

    float speed = 10f;
    float jumpforce = 5 ;

    public AudioClip[] AudioClipArr;
    public AudioSource SoundEffect;
    public Text healthTxt;
    public Text coinTxt;
    public Animator PlayerAnim;
    public Rigidbody2D PlayerRb;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRb.GetComponent<Rigidbody2D>();
        PlayerAnim.GetComponent<Animator>();

        rand = Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        jumping();
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerAnim.SetBool("Run", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            PlayerAnim.SetBool("Run", true);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            PlayerAnim.SetBool("Run", false);
        }
    }

    private void jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpcount == 0)
        {
            jumpcount = 1;
            SoundEffect.PlayOneShot(AudioClipArr[3]);
            PlayerAnim.SetTrigger("Jump");
            PlayerRb.AddForce(Vector3.up * jumpforce, ForceMode2D.Impulse);
        }
        else
        {
            PlayerAnim.SetBool("Idle", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            SoundEffect.PlayOneShot(AudioClipArr[rand]);
            healthCount -= 10;
            healthTxt.GetComponent<Text>().text = "Health: " + healthCount;
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            SoundEffect.PlayOneShot(AudioClipArr[0]);
            coinCount += 1;
            Destroy(collision.gameObject);
            coinTxt.GetComponent<Text>().text = "Coin: " + coinCount;
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            jumpcount = 0;
        }
    }
}
