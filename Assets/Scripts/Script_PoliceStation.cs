using UnityEngine;

public class PoliceStation : MonoBehaviour
{
    public GameObject policemen;
    public GameObject cage;
    public GameObject thug;

    private void Start()
    {
        policemen.SetActive(true);
        cage.SetActive(false);
    }

    public void RemovePolicemen()
    {
        policemen.SetActive(false);
    }

    public void JailThug()
    {
        cage.SetActive(true);
        thug.SetActive(false);
    }
}
