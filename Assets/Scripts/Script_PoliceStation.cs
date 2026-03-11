using UnityEngine;

public class PoliceStation : MonoBehaviour
{
    public GameObject policemen;
    public SleepingPolice policeSleeping;
    public IdlePolice policeIdle1;
    public IdlePolice policeIdle2;
    public GameObject thug;

    private void Start()
    {
        policemen.SetActive(true);
        thug.SetActive(false);
    }

    public void GatherPolicemen()
    {
        policeIdle1.ReturnToStation();
        policeIdle2.ReturnToStation();
        AwakenPolice();
    }

    public void AwakenPolice()
    {
        policeSleeping.StayAwake();
    }

    public void RemovePolicemen()
    {
        policemen.SetActive(false);
    }

    public void JailThug()
    {
        thug.SetActive(false);
    }
}
