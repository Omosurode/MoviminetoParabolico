using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovimientoParabolico : MonoBehaviour
{
    const float GRAVITY = -9.8f;

    private float v;        //Initial Velocity
    private int angle;
    private float guess_dist;    //User's distance guess

    private float vx, vy;   //Values for intial velocity in the separate axis
    private float x, y;     //Current position in each axis

    private float timeElapsed = 0.1f;  //Track of time passed since start of simulation to calculate position

    private bool simulating = false;

    [Header("References")]
    public GameObject ball;
    public GameObject resultsPanel;
    public GameObject results;

    public GameObject simulationPanel;
    public GameObject menuPanel;

    private TextMeshProUGUI resultsText;

    // Start is called before the first frame update
    void Start()
    {
        resultsText = results.gameObject.GetComponent<TextMeshProUGUI>();   //Get the text component, then hide the end screen panel and ball
        ball.SetActive(false);
        resultsPanel.SetActive(false);
        simulationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (simulating)
        {
            x = vx * timeElapsed;
            y = vy * timeElapsed + (0.5f * GRAVITY) * Mathf.Pow(timeElapsed, 2);       //Use gravity as acceleration

            if (y < 0)  //Ball touches ground
            {
                Debug.Log("Distance is: " + x + "m");
                simulating = false;
                timeElapsed = 0;
     
                resultsPanel.SetActive(true);

                if (guess_dist < x * 1.05f && guess_dist > x * 0.95)     //Check if user guess is within 5% margin
                {
                    resultsText.SetText("FELICITACIONES!!!\n\n"); 
                }
                else
                {
                    resultsText.SetText("LASTIMA\n\n");
                }

                resultsText.text += "Tu estimacion fue de " + guess_dist + " y la distancia final fue " + x;
            }
            else
            {
                ball.transform.position = new Vector2(x * 25, y * 25);
                timeElapsed += Time.deltaTime;
            }
        }
    }

    public void StartSimulation(float initVel, int initAngle, float initGuess)
    {
        v = initVel;
        angle = initAngle;
        guess_dist = initGuess;

        vx = v * Mathf.Cos(angle * Mathf.Deg2Rad); //Set initial velocities
        vy = v * Mathf.Sin(angle * Mathf.Deg2Rad);

        simulating = true;
        ball.SetActive(true);
        simulationPanel.SetActive(true);
    }

    public void EndSimulation()
    {
        ball.SetActive(false);
        resultsPanel.SetActive(false);
        simulationPanel.SetActive(false);

        menuPanel.SetActive(true);
    }
}
