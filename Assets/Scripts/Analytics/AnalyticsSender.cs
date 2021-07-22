using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SWT.Analytics;

public class AnalyticsSender : MonoBehaviour
{
  public float maxTimer = 5.0f;

  private System.DateTime _sessionStart;

  private float _timer;
  // Start is called before the first frame update
  void Start()
  {
    _sessionStart = System.DateTime.UtcNow;
    Analytics.Get.QueueDesignEvent(string.Empty, "session_start", _sessionStart.ToString("yyyy/MM/dd HH:mm:ss:ff"));
    _timer = 0.0f;
  }

  // Update is called once per frame
  void Update()
  {
    _timer += Time.deltaTime;
    if (_timer > maxTimer)
    {
      Analytics.Get.QueueDesignEvent(string.Empty, "session_test", _sessionStart.ToString("yyyy/MM/dd HH:mm:ss:ff"));
      _timer = 0.0f;
    }
  }

  private void OnApplicationQuit()
  {

    Analytics.Get.QueueDesignEvent(string.Empty, "session_duration", (float)System.DateTime.UtcNow.Subtract(_sessionStart).TotalSeconds);
    Analytics.Get.QueueDesignEvent(string.Empty, "session_end", System.DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss:ff"));
    Analytics.Get.ForceQueue();
  }

  public void DebugMessage()
  {
    Debug.Log("SENT DATA");
  }

}
