using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LazerDataView : MonoBehaviour
{
    [SerializeField] private TMP_Text _laserCountText;
    [SerializeField] private Image _rechargingBarFillImage;

    public void ChangeLaserCountText(string laserCountString) => _laserCountText.text = laserCountString;

    public void ChangeProgressBarFillingValue(float value) => _rechargingBarFillImage.fillAmount = value;

    public void SetRechargingBarColor(Color color) => _rechargingBarFillImage.color = color;
}
