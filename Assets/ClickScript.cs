using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickScript : MonoBehaviour
{
  public SpriteRenderer sr;
  public TextMeshProUGUI text;
  private float before;
  private bool running;
  private bool clickable;
  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(StartTest());
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      if (clickable)
      {
        float timeTaken = Time.time - before;
        sr.color = Color.blue;
        text.SetText(timeTaken * 1000 + "ms");
        clickable = false;
        running = false;
      }
      else if (running)
      {
        sr.color = Color.yellow;
      }
      else
      {
        StartCoroutine(StartTest());
      }
    }
  }

  IEnumerator StartTest()
  {
    text.SetText("Wait...");
    sr.color = Color.red;
    running = true;
    float timeToWait = Random.Range(2000, 4000);
    yield return new WaitForSeconds(timeToWait / 1000);
    text.SetText("Click!");
    sr.color = Color.green;
    before = Time.time;
    clickable = true;
  }
}