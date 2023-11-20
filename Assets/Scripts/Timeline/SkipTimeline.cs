using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipTimeline : MonoBehaviour
{

    private PlayableDirector timelineDirector;
    public bool isTimelineSkipped = false;
    private float timeToSkip = 48.0f;
    // Start is called before the first frame update
    void Start()
    {
        timelineDirector = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isTimelineSkipped && timelineDirector.time >= 5.0f)
        {
            timelineDirector.time = timeToSkip;
            isTimelineSkipped = true;
        }
    }
}
