using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Attack : ScriptableObject // Αντικείμενο για την οργάνωση των επιθέσεων
{
    public int staminaDrain, power;
    public string attackName, trigger, animBool;
}
