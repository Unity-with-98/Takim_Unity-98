using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance {  get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public int point = 0;

    
    public void PointUp()
    {
        point+= 5;
        UIManager.Instance.UpdatePointText();
        print(point);
    }
}
