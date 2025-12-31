using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
  public float moveSpeed;
  private bool isMoving;
  private Vector2 input;

  private Animator animator;

  private void Awake()
  {
    animator = GetComponent<Animator>();
  }

  private void Update()
  {
    if(!isMoving)
    {
      input.x = Input.GetAxisRaw("Horizontal");
      input.y = Input.GetAxisRaw("Vertical");
      
      // 移動を4方向のみにする
      if(input.x != 0)
      {
        input.y = 0;
      }
      
      if(input != Vector2.zero)
      {
        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);

        var targetPos = transform.position;
        targetPos.x += input.x;
        targetPos.y += input.y;
        StartCoroutine(Move(targetPos));
      }
    }

    animator.SetBool("isMoving", isMoving);
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
