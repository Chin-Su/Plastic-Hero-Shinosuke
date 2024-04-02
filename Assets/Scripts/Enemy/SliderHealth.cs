using UnityEngine;

public class SliderHealth : MonoBehaviour
{
    [SerializeField] private Transform parentOfThis;

    private void Update()
    {
        transform.localScale = new Vector3(parentOfThis.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}