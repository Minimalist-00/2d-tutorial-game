using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
  public float moveSpeed;
  private bool isMoving;
  private Vector2 input;

  private void Update()
  {
    if(!isMoving)
    {
      input.x = Input.GetAxisRaw("Horizontal");
      input.y = Input.GetAxisRaw("Vertical");
      
      // 斜め移動を防ぐ(4方向のみ)
      if(input.x != 0)
      {
        input.y = 0;
      }
      
      if(input != Vector2.zero)
      {
        // var targetPos = transform.position;
        // targetPos.x += input.x;
        // targetPos.y += input.y;
        // StartCoroutine(Move(targetPos));
        transform.position += new Vector3(input.x, input.y, 0) * moveSpeed * Time.deltaTime;
      }
    }
  }

  IEnumerator Move(Vector3 targetPos)
  {
    isMoving = true;
    while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
    {
      // 現在地から目標地点まで1フレームでどのくらい移動したかをいい感じに計算
      transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
      yield return null;
    }
    isMoving = false;
  }
}
