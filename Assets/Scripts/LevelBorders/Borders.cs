using System;
using UnityEngine;

namespace LevelBorders
{
    public class Borders : MonoBehaviour
    {
        public Transform back;
        public Transform left;
        public Transform right;
        public float sideOffset;
        public float backOffset;

        public void Init()
        {
            var backPos = back.localPosition;
            backPos.z = backOffset;
            back.localPosition = backPos;
            back.localEulerAngles = Vector3.zero;

            var leftPos = left.localPosition;
            leftPos.x = -sideOffset;
            left.localPosition = leftPos;
            left.localEulerAngles = new Vector3(0f, 90f, 0f);


            var rightPos = right.localPosition;
            rightPos.x = sideOffset;
            right.localPosition = rightPos;
            right.localEulerAngles = new Vector3(0f, 90f, 0f);

        }
    }
}