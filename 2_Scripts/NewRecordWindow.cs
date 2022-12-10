using UnityEngine;
using UnityEngine.UI;

public class NewRecordWindow : MonoBehaviour
{
    [SerializeField] Text _timeText;

    InputField _nameField;
    public bool _closeMessageActive = false;

    void Awake()
    {
        _nameField = FindObjectOfType<InputField>();
    }

    public void SetTime(float time)
    {
        _timeText.text = string.Format($"{time:N2}");
    }
    public void OnClickRegistButton()
    {
        FirebaseManager.GetIntstance().WriteRecord(_timeText.text);
        Destroy(gameObject);
    }
}
