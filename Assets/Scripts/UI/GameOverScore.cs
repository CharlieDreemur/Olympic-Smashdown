using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour {
    public TextMeshProUGUI score_num;
    private void Awake() {
        score_num.text = Player.Instance.score.ToString();
    }
}