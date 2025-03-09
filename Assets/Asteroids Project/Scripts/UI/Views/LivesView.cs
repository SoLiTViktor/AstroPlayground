using TMPro;
using UnityEngine;

public class LivesView : MonoBehaviour
{
    [SerializeField] private TMP_Text _livesText;

    public void ChangeLivesText(string lives) => _livesText.text = lives;
}
