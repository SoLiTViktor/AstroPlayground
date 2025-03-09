using TMPro;
using UnityEngine;

public class PlayerDataView : MonoBehaviour
{
    [SerializeField] private TMP_Text _positionText;
    [SerializeField] private TMP_Text _rotationText;
    [SerializeField] private TMP_Text _speedText;

    public void ChangePositionText(string positionString) => _positionText.text = positionString;

    public void ChangeRotationText(string rotationString) => _rotationText.text = rotationString;

    public void ChangeSpeedText(string speedString) => _speedText.text = speedString;
}
