using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Text timeText;

    private Stopwatch sw;
    private Rigidbody rb;
    private int count;
    private float currentTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        UpdateCountText();
        winText.text = "";
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        UpdateTimeText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed, ForceMode.Acceleration);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            this.count++;
            UpdateCountText();
        }
    }
    
    void UpdateCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You win!";
            enabled = false;
        }
    }

    void UpdateTimeText()
    {
        timeText.text = this.currentTime.ToString("0.00") + " sec";
        if (this.currentTime > 15 && count < 12)
        {
            winText.text = "You lose!";
            enabled = false;
        }
    }
}
