using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetController : MonoBehaviour
{
    Rigidbody2D _rigid;
    PlanetGenerater generator;

    [SerializeField] Sprite[] _planetImages;

    void Awake()
    {
        generator = FindObjectOfType<PlanetGenerater>();
        _rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _rigid.AddForce(Vector3.left * 5 , ForceMode2D.Impulse);
    }

    public void SetImage(int no)
    {
        GetComponent<Image>().sprite = _planetImages[no];
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        generator._planetCnt--;
        Destroy(gameObject);
    }
}
