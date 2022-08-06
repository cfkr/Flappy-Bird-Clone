using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    private int spriteIndex;

    private Vector3 direction;

    public float gravity = -9.8f;

    public float strength = 5f;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) //Fare sol týk ve space tuþumuza basýlýrsa
        {
            direction = Vector3.up * strength ; //Verilen güc orantisinda yukarý hareket edecegiz.
        }
        if (Input.touchCount > 0)  //Mobil entegrasyýn için ekrana dokunduysak
        {
            Touch touch = Input.GetTouch(0); //dokunma degiskeni atadýk
            if (touch.phase == TouchPhase.Began) //ekrana herhangi bir temesata
            {
                direction = Vector3.up * strength; // yukarý hareketimizi aktiflestirdik
            }
        }
        direction.y += gravity * Time.deltaTime; // oyunun her anýnda yer cekimini etkinlkestirdik.
        transform.position += direction * Time.deltaTime; // player transformuna dyoblerimemizi yaptik.
    }
    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >=sprites.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = sprites[spriteIndex];
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
