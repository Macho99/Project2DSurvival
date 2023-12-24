using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private int tileBlockSize = 30; // �÷��̾��� ��� �浹 ũ�⵵ ���������!!
    private void OnTriggerExit2D(Collider2D collision)
    {
        Vector2 playerPos = PlaySceneMaster.Instance.Player.transform.position;
        Vector2 myPos = transform.position;
        float diffX = Mathf.Abs(myPos.x - playerPos.x);
        float diffY = Mathf.Abs(myPos.y - playerPos.y);

        float dirX = myPos.x < playerPos.x ? 1f : -1f;
        float dirY = myPos.y < playerPos.y ? 1f : -1f;

        if(diffX > diffY)   // X �������� �̵�
        {
            transform.Translate(Vector2.right * dirX * (tileBlockSize * 2f));
        }
        else if(diffX < diffY)  // Y �������� �̵�
        {
            transform.Translate(Vector2.up * dirY * (tileBlockSize * 2f));
        }
        else if(diffX == diffY)
        {
            transform.Translate(Vector2.right * dirX * (tileBlockSize * 2f));
            transform.Translate(Vector2.up * dirY * (tileBlockSize * 2f));
        }
    }
}