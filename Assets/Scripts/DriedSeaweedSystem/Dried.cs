using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BubbleIdle.DriedSeaweedSystem
{
    public class Dried : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private bool isDrag;
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (!isDrag)
            {
               transform.Translate(Vector3.right * (Time.deltaTime * 5f));
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDrag = true;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            transform.position += (Vector3)eventData.delta * 0.0185f;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("EndZone"))
                {
                    GameController.ResourcesManager.AddDried(5);
                    Destroy(gameObject);
                    return;
                }
            }

            isDrag = false;
        }
    }
}