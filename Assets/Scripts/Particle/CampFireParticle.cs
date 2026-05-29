using UnityEngine;

public class CampFireParticle : MonoBehaviour
{

    [SerializeField] GameObject particleObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        EventManager.particle += TurnOnParticle;
    }

    void OnDisable()
    {
        EventManager.particle -= TurnOnParticle;
    }

    void TurnOnParticle(bool show)
    {
        particleObject.SetActive(show);
    }
}
