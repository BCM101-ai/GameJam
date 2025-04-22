using UnityEngine;

public class DimensionObjectToggler : MonoBehaviour
{
    public enum DimensionMode { Only2D, Only3D, Both }
    public DimensionMode dimensionMode = DimensionMode.Only2D;

    public void SetActiveForDimension(bool is2D)
    {
        switch (dimensionMode)
        {
            case DimensionMode.Only2D:
                gameObject.SetActive(is2D);
                break;

            case DimensionMode.Only3D:
                gameObject.SetActive(!is2D);
                break;

            case DimensionMode.Both:
                gameObject.SetActive(true);
                break;
        }
    }
}