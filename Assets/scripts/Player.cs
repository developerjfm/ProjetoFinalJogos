using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float forcaPulo;
	public float velocidadeMaxima;

	public int lives;
	public int rings;

	public Text TextLives;
	public Text TextRings;

	public bool isGrounded;
	
	void Start ()
	{
		TextLives.text = lives.ToString();
		TextRings.text = rings.ToString();
	}
	
	void Update ()
	{
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
		
		float movimento = Input.GetAxis("Horizontal");
		
		rigidbody.velocity = new Vector2(movimento*velocidadeMaxima,rigidbody.velocity.y);

		if (movimento < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}else if (movimento > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}


		if (movimento > 0 || movimento < 0)
		{
			GetComponent<Animator>().SetBool("walking", true);
		}
		else
		{
			GetComponent<Animator>().SetBool("walking", false);
		}

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rigidbody.AddForce(new Vector2(0,forcaPulo));
			GetComponent<AudioSource>().Play();
		}

		if (isGrounded)
		{
			GetComponent<Animator>().SetBool("jumping", false);
		}
		else
		{
			GetComponent<Animator>().SetBool("jumping", true);
		}
		
	}

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Moedas"))
        {
            Destroy(collision2D.gameObject);
            rings++;
            TextRings.text = rings.ToString();
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
	{
		if ( collision2D.gameObject.CompareTag("Monstros"))
		{
			//Criar lógica para perder vida
		}
		
		if ( collision2D.gameObject.CompareTag("Plataformas"))
		{
			isGrounded = true;
		}
		
	}

	
	void OnCollisionExit2D(Collision2D collision2D)
	{
		if ( collision2D.gameObject.CompareTag("Plataformas"))
		{
			isGrounded = false;
		}
	}
}
