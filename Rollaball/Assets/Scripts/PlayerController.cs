using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 1;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject explicationText;
    public float explicationDuration = 5.0f; // Cuanto tiempo (en segundos) se mostrará el texto
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody>(); 
        count = 0; 
        // Para poner el texto al principio del juego
        SetCountText();
        // Para esconder el mensaje de win al principio del juego
        winTextObject.SetActive(false);
        StartCoroutine(HideExplicationText()); // Para ocultar el mensaje después del tiempo puesto
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }
    void SetCountText() 
    {
       countText.text =  "Count: " + count.ToString();
       // Para que aparezca cuando se hayan recolectado todos los puntos
        if (count >= 8)
        {
           winTextObject.SetActive(true);
        }
    }
    // Función para hacer que el texto se esconda a los 5 segundos
    IEnumerator HideExplicationText()
    {
        yield return new WaitForSeconds(explicationDuration);
        explicationText.SetActive(false);
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
   
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
       {
           other.gameObject.SetActive(false);
           count = count + 1;
           SetCountText();
       }
    }

}
