﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GizmoNS
{
    public class ArrowRenderer : MonoBehaviour
    {
        public float height = 0.5f;
        public float segmentLength = 0.5f;
        public float fadeDistance = 0.35f;
        public float speed = 1f;

        private Color color = Color.green;

        [SerializeField] GameObject arrowPrefab;
        [SerializeField] GameObject segmentPrefab;

        [Space] [SerializeField] Vector3 start;
        [SerializeField] Vector3 end;
        [SerializeField] Vector3 upwards = Vector3.up;

        private bool isEnabled = false;

        Transform arrow;

        readonly List<Transform> segments = new List<Transform>();
        readonly List<MeshRenderer> renderers = new List<MeshRenderer>();

        [Inject]
        public void Construct(ArrowRendererProvider arrowRendererProvider)
        {
            arrowRendererProvider.ArrowRenderer = this;
        }

        public void SetColor(Color color)
        {
            this.color = color;
            UpdateSegments();
        }

        public void SetStart(Vector3 start)
        {
            SetPositions(start, end);
        }

        public void SetEnd(Vector3 end)
        {
            SetPositions(start, end);
        }

        public void SetPositions(Vector3 start0, Vector3 end0)
        {
            start = start0;
            end = end0;
            UpdateSegments();
        }

        public bool Enabled
        {
            set {
                renderers.ForEach(renderer => renderer.enabled = value);
                if (arrow != null)
                {
                    arrow.GetComponent<Renderer>().enabled = value;
                }
                isEnabled = value;
            }
        }

        void Update()
        {
            if (isEnabled)
            {
                UpdateSegments();
            }
        }

        void UpdateSegments()
        {
            if (isEnabled == false)
                return;

            Debug.DrawLine(start, end, Color.yellow);

            float distance = Vector3.Distance(start, end);
            float radius = height / 2f + distance * distance / (8f * height);
            float diff = radius - height;
            float angle = 2f * Mathf.Acos(diff / radius);
            float length = angle * radius;
            float segmentAngle = segmentLength / radius * Mathf.Rad2Deg;

            Vector3 center = new Vector3(0, -diff, distance / 2f);
            Vector3 left = Vector3.zero;
            Vector3 right = new Vector3(0, 0, distance);

            int segmentsCount = (int)(length / segmentLength) + 1;

            CheckSegments(segmentsCount);

            float offset = Time.time * speed * segmentAngle;
            Vector3 firstSegmentPos =
                Quaternion.Euler(Mathf.Repeat(offset, segmentAngle), 0f, 0f) * (left - center) + center;

            float fadeStartDistance = (Quaternion.Euler(segmentAngle / 2f, 0f, 0f) * (left - center) + center).z;

            for (int i = 0; i < segmentsCount; i++)
            {
                Vector3 pos = Quaternion.Euler(segmentAngle * i, 0f, 0f) * (firstSegmentPos - center) + center;
                segments[i].localPosition = pos;
                segments[i].localRotation = Quaternion.FromToRotation(Vector3.up, pos - center);

                MeshRenderer rend = renderers[i];

                if (!rend)
                    continue;

                color.a = GetAlpha(pos.z - left.z, right.z - fadeDistance - pos.z, fadeStartDistance);
                rend.material.color = color;
            }

            if (!arrow)
                arrow = Instantiate(arrowPrefab, transform).transform;

            arrow.GetComponent<Renderer>().material.color = color;
            arrow.localPosition = right;
            arrow.localRotation = Quaternion.FromToRotation(Vector3.up, right - center);

            transform.position = start;
            transform.rotation = Quaternion.LookRotation(end - start, upwards);
        }

        void CheckSegments(int segmentsCount)
        {
            while (segments.Count < segmentsCount)
            {
                Transform segment = Instantiate(segmentPrefab, transform).transform;
                segments.Add(segment);
                renderers.Add(segment.GetComponent<MeshRenderer>());
            }

            for (int i = 0; i < segments.Count; i++)
            {
                GameObject segment = segments[i].gameObject;
                if (segment.activeSelf != i < segmentsCount)
                    segment.SetActive(i < segmentsCount);
            }
        }

        static float GetAlpha(float distance0, float distance1, float distanceMax)
        {
            return Mathf.Clamp01(Mathf.Clamp01(distance0 / distanceMax) + Mathf.Clamp01(distance1 / distanceMax) - 1f);
        }
    }
}