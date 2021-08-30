using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    // so proud of myself for looking up all this stuff and actually succeeding :D
    // couldn't be happier with how the score system looks

    public GameObject CompleteScoreObject;
    private float _speed;  
    private float _amount;  

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ComboScoreText;

    public ScoreCalculation ScoreCalculation;

    public GameObject ComboScore;

    public int ScoreShown;
    public int ComboScoreShown;

    private float _timer;

    private bool _scoreIsIncreasing;

    private string _format;

    private float _shaking;
    private float _originalY;

    void Start()
    {
        _speed = 10.0f;  // how fast it shakes
        _amount = 50.0f;  // how much it shakes

        _format = "0000000";

        _originalY = CompleteScoreObject.transform.position.y;
    }

    void Update()
    {
        _shaking = Mathf.Sin(Time.deltaTime * _speed) * _amount;  // adding a shake to the counter when score is gained

        if (ScoreShown < ScoreCalculation.Score)
        {
            _scoreIsIncreasing = true;
        }
        else if (ScoreShown >= ScoreCalculation.Score)
        {
            _scoreIsIncreasing = false;
            ScoreShown = ScoreCalculation.Score;
        }

        if (_scoreIsIncreasing)
        {
            ScoreShown++;
            CompleteScoreObject.transform.position = new Vector3(CompleteScoreObject.transform.position.x, _originalY + _shaking, CompleteScoreObject.transform.position.z);
        }
        else
        {
            CompleteScoreObject.transform.position = new Vector3(CompleteScoreObject.transform.position.x, _originalY, CompleteScoreObject.transform.position.z);
        }        

        if (ScoreCalculation.ComboActive)
        {
            ComboScore.SetActive(true);
            ComboScoreText.text = ScoreCalculation.ComboScore.ToString(_format);
        }
        else
        {
            ComboScore.SetActive(false);
        }

        ScoreText.text = ScoreShown.ToString(_format);
    }
}
