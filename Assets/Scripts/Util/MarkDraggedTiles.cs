using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Util
{
    public class MarkDraggedTiles: MonoBehaviour
    {
        [NotNull]
        private readonly List<Vector3> _path = new ();
    
        private Camera _camera;

        private int _touchId;
        private bool _dragging;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount == 0) return;

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (_dragging) break;
                
                    _path.Add(ConvertPos(touch.position));
                    _dragging = true;
                    break;
                case TouchPhase.Moved:
                    if (!_dragging) break;
                
                    var pos = ConvertPos(touch.position);

                    if(!_path.Contains(pos))
                        _path.Add(pos);
                    break;
                case TouchPhase.Ended:
                    if (!_dragging) break;

                    _dragging = false;
                    _path.Clear();
                    break;
                case TouchPhase.Stationary:
                case TouchPhase.Canceled:
                default:
                    break;
            }
        }

        private Vector3 ConvertPos(Vector2 pos)
        {
            var world = _camera.ScreenToWorldPoint(pos);
            return world;
        }
    }
}