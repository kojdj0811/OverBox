using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleToneMono<T> : MonoBehaviour where T : SingleToneMono<T>
{
    protected static T m_instance;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<T>();

            return m_instance;
        }
    }

    protected virtual void Awake()
    {
        if (m_instance == null)
            m_instance = this as T;

        else if (m_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
}

public class SingleTone<T> where T : class, new()
{
    protected static T _instance;
    public static T Instance
    {
        get
        {
            // ���� instance�� �������� ���� ��� ���� �����Ѵ�.
            if (_instance == null)
            {
                _instance = new T();

            }
            // _instance�� ��ȯ�Ѵ�.
            return _instance;
        }
    }

    public void DoSomething()
    {
        Debug.Log("Singleton : DoSomething!");
    }
}
