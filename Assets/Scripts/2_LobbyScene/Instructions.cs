using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject attributesInfo;

    public void BtnInfo()
    {
        attributesInfo.SetActive(!attributesInfo.activeSelf);
    }
}
