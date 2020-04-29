//-----------------------------------------------------------------------
// <copyright file="HelloARController.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.EventSystems;

#if UNITY_EDITOR
    using Input = InstantPreviewInput;
#endif
    public class HelloARController2 : MonoBehaviour
    {
        public static HelloARController2 ins;
        public Camera firstPersonCamera;            // 카메라 이미지 관통하는 렌더링 하는데 사용
        public Camera uiCamera;                     // UI 카메라
        [SerializeField] LineRenderer line;
        public GameObject goVerticalPlanePrefab;    // 레이캐스트 수직 평면에 닿을 때 배치 할 프리팹
        public GameObject goHorizontalPlanePrefab;  // 수평면에 닿을 떄 
        public GameObject goPointPrefab;            // 피쳐 포인터에 닿을 때( 점 찍기 )
        private const float k_PrefabRotation = 180.0f;  // 회전각도

        /// True if the app is in the process of quitting due to an ARCore connection error, otherwise false
        private bool m_IsQuitting = false;

        List<Transform> list = new List<Transform>();
        bool bCalculate, bLine;
        GameObject goNewTile;
        [SerializeField] Material material;

        public void Awake()
        {
            ins = this;
            Application.targetFrameRate = 60;

            // 라인렌더러 안 들어가 있으면 찾아서 넣기
            if (line == null)
            {
                line = GetComponent<LineRenderer>();
            }
        }

        public void Update()
        {
            ///// PC TEST /////
            //if (Input.GetMouseButtonDown(0))
            //{
            //	float x = 0;
            //	for (int i = 0; i < 100000000; i++)
            //	{
            //		x += 1;
            //	}
            //	Debug.Log("e:" + Time.realtimeSinceStartup);
            //}


            // Mesh 생성
            DoCreateMesh();

            //Debug.Log(UICamera.currentCamera);
            _UpdateApplicationLifecycle();

            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

            // 뒷 화면 안찍히게 하기. ui에 찍히게 
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)
                || uiCamera == UICamera.currentCamera)
            {
                //Debug.Log(1);
                return;
            }

            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(firstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    GameObject prefab;
                    if (hit.Trackable is FeaturePoint)
                    {
                        prefab = goPointPrefab;
                    }
                    else if (hit.Trackable is DetectedPlane)
                    {
                        DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                        if (detectedPlane.PlaneType == DetectedPlaneType.Vertical)
                        {
                            prefab = goVerticalPlanePrefab;
                        }
                        else
                        {
                            prefab = goHorizontalPlanePrefab;
                        }
                    }
                    else
                    {
                        prefab = goHorizontalPlanePrefab;
                    }

                    // 포인터 생성
                    var _go = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
                    _go.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    _go.transform.SetParent(anchor.transform);

                    // 리스트에 포인터들 저장
                    list.Add(_go.transform);
                    bCalculate = true;
                    bLine = true;
                }
            }

            //line 렌더러 
            if (bLine && list.Count >= 2)
            {
                bLine = false;
                int _count = list.Count;
                line.SetVertexCount(_count);
                line.SetWidth(0.01f, 0.01f);
                line.SetColors(Color.red, Color.yellow);
                line.useWorldSpace = true;
                for (int i = 0; i < _count; i++)
                {
                    line.SetPosition(i, list[i].position);
                }
            }
        }

        bool bCreateMesh, bClearMesh;

        public void CreateMeshing(bool _bCreateMesh)
        {
            bCreateMesh = _bCreateMesh;
            Debug.Log("CreateMeshing 함수 호출");
        }

        public void ClearMesh(bool _bClearMesh)
        {
            bClearMesh = _bClearMesh;
            Debug.Log("ClearMesh 함수 호출");
        }
        void CalculateListObject()
        {
            if (goNewTile != null)
            {
                Destroy(goNewTile);
            }

            goNewTile = new GameObject();
            MeshFilter _meshFilter = goNewTile.AddComponent<MeshFilter>();
            MeshRenderer _meshRenderer = goNewTile.AddComponent<MeshRenderer>();
            Mesh _mesh = new Mesh();

            _meshRenderer.material = material;
            _mesh.name = "TileMesh";
            _meshFilter.mesh = _mesh;

            // Z방향 찍을 필요 없음. ㄷ자 순서로 찍어도 가능
            if (list.Count == 4)
                Triangulator.ReCalcuatePosition(list);

            //Vector3 -> vector2 로 변환
            Vector2[] _vertices2D = new Vector2[list.Count];
            Vector3 _pos;
            float _y = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                _pos = list[i].position;
                _vertices2D[i] = new Vector2(_pos.x, _pos.z);
                _y += _pos.y;
            }
            _y /= list.Count;


            //triangle ...
            Triangulator _tr = new Triangulator(_vertices2D);
            int[] _triangles = _tr.Triangulate();

            //vertices
            Vector3[] _vertices = new Vector3[_vertices2D.Length];
            for (int i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] = new Vector3(_vertices2D[i].x, _y, _vertices2D[i].y);
            }

            // Create the mesh
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            _mesh.uv = _tr.CalculateUV();
            _mesh.RecalculateNormals();
            _mesh.RecalculateBounds();

            //material texture, 
            material.mainTextureScale = _tr.CalculateScale(.2f);

            if (bClearMesh == true)
            {
                Debug.Log("ClearMesh 함수 호출2");
                _mesh.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    DestroyImmediate(list[i].gameObject);
                }
                list.Clear();

                bClearMesh = false;
            }
            //for (int i = 0; i < list.Count; i++)
            //	Destroy(list[i].gameObject);
            //list.Clear();
            //line.SetVertexCount(0);
        }

        //----------------------------------------
        public void SetMaterial(Texture _texture)
        {
            material.mainTexture = _texture;
        }

        void DoCreateMesh()
        {
            //ddd.text = bCalculate + ":" + list.Count + ":" + bCreateMesh;
            if (bCalculate && list.Count >= 3 && bCreateMesh)
            {
                bCreateMesh = false;
                bCalculate = false;
                CalculateListObject();
            }
        }



        // 라이프 사이클 관련
        private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (m_IsQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to
            // appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage(
                    "ARCore encountered a problem connecting.  Please start the app again.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void _DoQuit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>(
                            "makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}
