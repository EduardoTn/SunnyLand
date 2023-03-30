using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerFade : MonoBehaviour
{

    public static ControllerFade _instanceFade;
    public Image _imagemFade;
    public Color _corInicial, _corFinal;
    public float _duracaoFade;
    public bool _isFading;
    private float _tempo;

    void Awake()
    {
        _instanceFade= this;
    }
    void Start()
    {
        StartCoroutine(inicioFade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator inicioFade()
    {
        _isFading= true;
        _tempo= 0;
        while (_tempo <= _duracaoFade)
        {
            _imagemFade.color= Color.Lerp(_corInicial, _corFinal, _tempo/_duracaoFade);
            _tempo += Time.deltaTime;
            yield return null;
        }

        _isFading= false;
    }
}
