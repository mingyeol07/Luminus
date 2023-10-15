using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    void Update()
    {
       MouseLock();
       if(tx.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            tx.gameObject.SetActive(false);
        }
    }

    private void MouseLock()
    {
        Cursor.lockState = CursorLockMode.Confined; 
    }

    public Text tx;
    private string m_text = "������ ������ 22���� ����.\r\n������� ��Ȱ������ ���ַ� Ȯ��Ǿ�����\r\n���ָ� ���ƴٴϸ���ο� ������ �͵鿡 ���� ������ ��������.\r\n�� �༺�� ������ ���ڰ� �� ����� �ִ°� ���� �������� ����\r\n������ ������ �������� Ž���ϴ� ���迡 ó�ϴ� ����鵵 �ִ�.\r\n��ŵ� �� ��ȸ�� ��ġ�� �ʰ� ���ڰ� �Ǳ� ����\r\n������ �������� ������ ã���� ���༺�� �����Ѵ�.";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_typing());
    }
    IEnumerator _typing()
    {
        for (int i = 0; i <= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
