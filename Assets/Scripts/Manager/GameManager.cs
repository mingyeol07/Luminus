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
    private string m_text = "과학이 발전한 22세기 지구.\r\n사람들의 생활영역은 우주로 확장되었으며\r\n우주를 돌아다니며새로운 우주의 것들에 대한 관심이 높아졌다.\r\n새 행성에 정착해 부자가 된 사람도 있는가 아직 밝혀내지 못한\r\n우주의 위험한 생물들을 탐사하다 위험에 처하는 사람들도 있다.\r\n당신도 이 기회를 놓치지 않고 부자가 되기 위해\r\n위험을 무릅쓰고 보물을 찾으러 이행성에 도착한다.";

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
