using System;
using System.Collections;
using UnityEngine;

public abstract class Stunner : MonoBehaviour
{
    private float _elapsedTime;
    private Coroutine _disableTask;
    private Coroutine _timeCounter;

    protected abstract void Resume();
    protected abstract void Suspend();

    public void Disable(float time)
    {
        Run(ref _disableTask, DisableTask(time));
    }

    private void Run(ref Coroutine coroutine, IEnumerator ienumerator)
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(ienumerator);
        }
        else
        {
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(ienumerator);
        }
    }

    private IEnumerator DisableTask(float time)
    {
        Run(ref _timeCounter, TimeCounter());
        Suspend();

        while(_elapsedTime < time)
        {
            yield return null;
        }

        StopCoroutine(_timeCounter);
        Resume();
    }

    private IEnumerator TimeCounter()
    {
        _elapsedTime = 0;

        while(true)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
