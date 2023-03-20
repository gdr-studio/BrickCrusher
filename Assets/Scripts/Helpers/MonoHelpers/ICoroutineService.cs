using System.Collections;
using UnityEngine;

namespace Helpers
{
    public interface ICoroutineService
    {
        Coroutine StartCor(IEnumerator enumerator);
        void StopCor(Coroutine coroutine);
    }
}