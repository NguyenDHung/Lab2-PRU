using UnityEngine;
using UnityEngine.UI;

public class PlayerRotation : MonoBehaviour
{
    private AudioManager audioManager;
    [Header("Stamina Recovery Settings")]
    public FloatVariable maxStamina;
    public Canvas StaminaCanvas;
    public float recoveryRateBasedMax = 3f;
    public BoolVariable IsAlive;
    public GroundedTrigger groundedTrigger;
    private float recoveryAmount => maxStamina.Value / recoveryRateBasedMax;
    private bool isGrounded = false;
    private float cumulativeRotation = 0f;
    private float previousAngle;

    private Slider staminaSlider;

    private void Awake()
    {
        groundedTrigger.PlayerGroundedEvent.AddListener(OnPlayerGrounded);
        groundedTrigger.PlayerAirbornEvent.AddListener(OnPlayerAirborne);
        previousAngle = transform.eulerAngles.z;
        if (StaminaCanvas != null)
        {
            staminaSlider = StaminaCanvas.GetComponentInChildren<Slider>();
            if (staminaSlider != null)
            {
                staminaSlider.maxValue = maxStamina.Value;
            }
        }
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnPlayerGrounded()
    {
        //Debug.Log("Player đã chạm đất");
        cumulativeRotation = 0;
        isGrounded = true;
    }

    private void OnPlayerAirborne()
    {
        //Debug.Log("Player rời đất");
        isGrounded = false;
    }

    private void Update()
    {
        if(!IsAlive.Value)
        {
            return;
        }
        if (!isGrounded)
        {
            float currentAngle = transform.eulerAngles.z;
            float deltaAngle = Mathf.DeltaAngle(previousAngle, currentAngle);

            if (Mathf.Approximately(currentAngle, 0f) && !Mathf.Approximately(previousAngle, 0f))
            {
                deltaAngle = 0f;
            }

            cumulativeRotation += Mathf.Abs(deltaAngle);
            previousAngle = currentAngle;
            //Debug.Log("cumulativeRotation: " + cumulativeRotation);
            if (cumulativeRotation >= 360f && staminaSlider != null && IsAlive)
            {
                Debug.Log("Đã quay 1 vòng");
                ScoreDisplay.instance.AddScore(1);
                float currentStamina = staminaSlider.value;
                currentStamina += recoveryAmount;
                if (currentStamina > maxStamina.Value)
                    currentStamina = maxStamina.Value;
                staminaSlider.value = currentStamina;

                cumulativeRotation -= 360f;
            }
            
        }
    }
}
