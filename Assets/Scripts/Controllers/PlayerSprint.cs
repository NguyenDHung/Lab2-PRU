using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerSprint : MonoBehaviour
{
    public FloatVariable baseSpeed;
    public FloatVariable maxSpeed;
    public FloatVariable acceleration;

    [Header("Stamina Settings")]
    public float Stamina = 100f;
    public float sprintStaminaDrainRate = 10f;
    public float staminaRegenDelay = 3f;
    public float MaxStamina = 100f;
    public float staminaRegenRate = 1f;

    [Header("Sprint Force Settings")]
    public float sprintForceRampRate = 1f;
    public float maxSprintBoostForce = 5f;

    public BoolVariable IsAlive;
    public Canvas StaminaCanvas;

    [Tooltip("Event invoked when player sprints.")]
    public UnityEvent PlayerSprintEvent;

    private Rigidbody2D rigidBody;
    private Slider staminaSlider;
    private float currentSprintForce = 0f;
    private bool isSprinting;
    private float currentSprintBoost = 0f;
    private float timeSinceLastSprint = 0;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if (StaminaCanvas != null)
        {
            staminaSlider = StaminaCanvas.GetComponentInChildren<Slider>();
            if (staminaSlider != null)
            {
                staminaSlider.maxValue = Stamina;
                staminaSlider.value = Stamina;
            }
        }
    }

    private void Update()
    {
        Stamina= staminaSlider.value;
        if (!IsAlive.Value)
        {
            isSprinting = false;
            return;
        }

        if (Input.GetKey(KeyCode.Space) && Stamina > 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    private void FixedUpdate()
    {
        if (isSprinting && Stamina > 0)
        {
            timeSinceLastSprint = 0f;
            currentSprintBoost = Mathf.MoveTowards(
               currentSprintBoost,
               maxSprintBoostForce,
               sprintForceRampRate * Time.fixedDeltaTime
           );

            float sprintSpeed = baseSpeed.Value + currentSprintBoost;

            sprintSpeed = Mathf.Min(sprintSpeed, maxSpeed.Value);

            rigidBody.linearVelocity = new Vector2(sprintSpeed, rigidBody.linearVelocity.y);

            float staminaCost = sprintStaminaDrainRate * Time.fixedDeltaTime;
            Stamina -= staminaCost;
            if (Stamina < 0)
                Stamina = 0;

            if (staminaSlider != null)
            {
                staminaSlider.value = Stamina;
            }

            PlayerSprintEvent.Invoke();
        }
        else
        {
            currentSprintForce = Mathf.MoveTowards(
                currentSprintForce,
                0,
                sprintForceRampRate * Time.fixedDeltaTime
            );
            timeSinceLastSprint += Time.fixedDeltaTime;
            if (timeSinceLastSprint >= staminaRegenDelay)
            {
                Stamina += staminaRegenRate * Time.fixedDeltaTime;
                if (Stamina > MaxStamina)
                    Stamina = MaxStamina;

                if (staminaSlider != null)
                {
                    staminaSlider.value = Stamina;
                }
            }
        }
    }
}
