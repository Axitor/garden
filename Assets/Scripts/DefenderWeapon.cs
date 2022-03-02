using UnityEngine;

public class DefenderWeapon : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private float _flySpeed = 3f;
    [SerializeField] private int _damagePoints = 50;

    /// <summary>
    /// Make a fly step on update
    /// </summary>
    private void Update()
    {
        FlyStep();
    }

    /// <summary>
    /// Make one step movement to the right with walk speed
    /// </summary>
    private void FlyStep()
    {
        transform.Translate(Vector2.right * _flySpeed * Time.deltaTime);
    }

    /// <summary>
    /// Actions on trigger
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Destroy a weapon and send a hit on trigger with an attacker
        if (collider.gameObject.tag == "Attacker")
        {
            collider.gameObject.GetComponent<HealthSystem>().SetDamage(_damagePoints);
            Destroy(gameObject);
        }

        // Destroy on trigger with a collider out of the screen
        else if (collider.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
