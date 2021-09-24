using System.Collections;
using System.Linq;
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
  private bool early;
  private List<float> attempts;
  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(StartTest());
    attempts = new List<float>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      if (clickable)
      {
        float timeTaken = (Time.time - before) * 1000;
        sr.color = Color.blue;
        string textToSet = timeTaken + "ms";
        if (early) textToSet += "\n(invalid, clicked early)";
        else attempts.Add(timeTaken);
        if (attempts.Count > 0)
        {
          float averageTimeTaken = attempts.Average();
          textToSet += "\n\n\nAverage: " + averageTimeTaken + "ms";
          textToSet += "\nNumber of Attempts: " + attempts.Count;
          textToSet += "\nBest this Session: " + attempts.Min() + "ms";
        }
        text.SetText(textToSet);
        clickable = false;
        running = false;
        early = false;
      }
      else if (running)
      {
        sr.color = Color.yellow;
        text.SetText("Too early!");
        early = true;
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
