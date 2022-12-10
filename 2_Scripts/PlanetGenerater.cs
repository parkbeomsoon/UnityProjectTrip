using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerater : MonoBehaviour
{
    [SerializeField] GameObject _planetPrefab;

    CharacterController cc;


    public int _planetCnt = 0;
    int _maxImageCnt = 6;
    float _delayCnt = 1f;
    float _maxYPos = 350f;

    private void Awake()
    {
        cc = FindObjectOfType<CharacterController>();
    }

    void Start()
    {
        StartCoroutine(GenaratePlanet());
    }

    public void Reset()
    {
        GameObject[] goList = GameObject.FindGameObjectsWithTag("Planet");
        for(int i = 0; i < goList.Length; i++)
        {
            Destroy(goList[i]);
        }
        _planetCnt = 0;
    }

    IEnumerator GenaratePlanet()
    {
        Transform bg = GameObject.FindGameObjectWithTag("Background").transform;
        while (!cc.isDead)
        {
            float randPosY = Random.Range(-_maxYPos, _maxYPos);

            Vector2 randVec = new Vector2(transform.localPosition.x, randPosY);

            GameObject go = Instantiate(_planetPrefab, bg);
            go.GetComponent<RectTransform>().anchoredPosition = randVec;
            int imgRand = Random.Range(0, _maxImageCnt);
            go.GetComponent<PlanetController>().SetImage(imgRand);

            _planetCnt++;
            yield return new WaitForSeconds(_delayCnt);
        }
    }
}
