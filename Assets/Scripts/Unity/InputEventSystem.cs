using System;
using UnityEngine;

namespace Unity
{
    public class InputEventSystem: MonoBehaviour
    {
        /// <summary>
        /// which infos are needed? maybe Id: which mouse button / touchId?
        /// </summary>
        public delegate void InputEvent(Vector3 inputPosition);

        public static event InputEvent OnInpBegin;
        public static event InputEvent OnInpDrag;
        public static event InputEvent OnInpEnd;

        private Camera _camera;

        private bool _mouseDrag;
        
        private void Start()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            MouseInp(); // replaced with OnMouseDrag ??
            TouchInp();
        }

        /// <summary>
        /// listens to left mouse click drag lift
        /// invokes the events with the mouse position based on Camera.main
        /// </summary>
        private void MouseInp()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnInpBegin?.Invoke(ConvertPos(Input.mousePosition));
            }

            if (Input.GetMouseButton(0))
            {
                OnInpDrag?.Invoke(ConvertPos(Input.mousePosition));
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnInpEnd?.Invoke(ConvertPos(Input.mousePosition));
            }
        }

        /// <summary>
        /// same thing as <see cref="MouseInp"/> but for touch
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">if this is thrown kill yourself</exception>
        private void TouchInp()
        {
            if (Input.touchCount == 0) return;

            var touch = Input.GetTouch(0);

            switch (touch.phase) 
            {
                case TouchPhase.Began:
                    OnInpBegin?.Invoke(ConvertPos(touch.position));
                    break;
                case TouchPhase.Moved:
                    OnInpDrag?.Invoke(ConvertPos(touch.position));
                    break;
                case TouchPhase.Ended:
                    OnInpEnd?.Invoke(ConvertPos(touch.position));
                    break;
                case TouchPhase.Stationary:
                case TouchPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private Vector3 ConvertPos(Vector3 pos)
        {
            return _camera.ScreenToWorldPoint(pos);
        }
    }
}