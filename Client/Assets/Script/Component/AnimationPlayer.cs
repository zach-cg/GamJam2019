﻿using UnityEngine;
using UnityEngine.UI;

public class AnimationPlayer : MonoBehaviour
{
    AnimationInfo curInfo;
    Sprite[] frames;
    public float speed = 0.1f;
    public int actionFrame = -1;
    //public UnityEvent frameEvent;

    private Image container;
    private int ticked;
    private float time;
    private bool doAnim;

    private void Awake()
    {
        container = GetComponent<Image>();
    }

    public void Play(AnimationInfo info,bool replay = false)
    {
        if(curInfo == info && !replay)
        {
            return;
        }
        Stop();
        curInfo = info;
        frames = Resources.LoadAll<Sprite>(info.resName);
        ticked = info.startFrame;
        time = 0;
        speed = info.duration / (info.endFrame - info.startFrame + 1);
        doAnim = true;
        container.sprite = frames[info.startFrame];
    }

    public void Pause()
    {
        doAnim = false;
    }

    public void Resume()
    {
        doAnim = true;
    }

    public void Stop()
    {
        time = 0;
        doAnim = false;
        //container.sprite = frames[0];
    }

    void Update()
    {
        if (doAnim)
        {
            time += Time.deltaTime;
            if (time > speed)
            {
                ticked++;
                if (ticked > curInfo.endFrame)
                    if (curInfo.loop)
                    {
                        ticked = curInfo.startFrame;
                    }
                    else
                    {
                        ticked = curInfo.endFrame;
                    }
                    
                else
                    time = 0;

                //if (ticked == actionFrame)
                //    frameEvent.Invoke();

                container.sprite = frames[ticked];
            }
        }
    }
}