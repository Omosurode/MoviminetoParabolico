using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private enum FIELD_TYPE {Init_Vel, Angle, Guess };  //Enum used in order to only have one verifyinput function

    [Header("MainMenu and error panel reference")]
    public GameObject menuPanel;
    public GameObject errorMsg;

    [Header("InputFields")]
    public TMPro.TMP_InputField initVel;
    public TMPro.TMP_InputField angle;
    public TMPro.TMP_InputField guess;

    private bool validVel;
    private bool validAngle;
    private bool validGuess;

    [Header("Reference to SimulationManager")]
    public MovimientoParabolico manager;

    // Start is called before the first frame update
    void Start()
    {
        initVel.onEndEdit.AddListener(delegate { VerifyInput(FIELD_TYPE.Init_Vel); });
        angle.onEndEdit.AddListener(delegate { VerifyInput(FIELD_TYPE.Angle); });
        guess.onEndEdit.AddListener(delegate { VerifyInput(FIELD_TYPE.Guess); });

        validVel = false;
        validAngle = false;
        validGuess = false;

        ToggleError();
    }

    private void VerifyInput(FIELD_TYPE type)   //Check that user input is valid trough events
    {
        switch (type)
        {
            case FIELD_TYPE.Init_Vel:
                if(initVel.text == "" || float.Parse(initVel.text) < 0)
                {
                    initVel.text = "";
                    validVel = false;
                }
                else
                {
                    validVel = true;
                }
                break;
            case FIELD_TYPE.Angle:
                if(angle.text != "")
                {
                    angle.text = Mathf.Clamp(int.Parse(angle.text), 0, 90).ToString();  //Clamp angle value to ensure it stays between 0 and 90
                    validAngle = true;
                }
                else
                {
                    validAngle = false;
                }
                break;
            case FIELD_TYPE.Guess:
                if (guess.text == "" || float.Parse(guess.text) < 0)
                {
                    guess.text = "";
                    validGuess = false;
                }
                else
                {
                    validGuess = true;
                }
                break;
        }
    }

    public void BeginSimulation()
    {
        if (validVel && validAngle && validGuess)
        {
            menuPanel.SetActive(false); //Deactivate the menu and clear the values on the fields

            manager.StartSimulation(float.Parse(initVel.text), int.Parse(angle.text), float.Parse(guess.text)); //Start the simulation with the input values

            initVel.text = "";
            angle.text = "";
            guess.text = "";
        }
        else
        {
            ToggleError();
        }
    }

    public void ToggleError()   //Function to turn error message on and off
    {
        if (errorMsg.activeInHierarchy)
        {
            errorMsg.SetActive(false);
        }
        else
        {
            errorMsg.SetActive(true);
        }
    }
}
