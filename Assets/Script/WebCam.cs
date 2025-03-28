using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WebCam : MonoBehaviour
{
    [SerializeField] RawImage centerCamera;
    [SerializeField] int devicesIndex = 0;
    private int width = 640;
    private int height = 480;
    private int fps = 30;
    private WebCamTexture webcamTexture;

    void Start()
    {
        if (webcamTexture == null || !webcamTexture.isPlaying)
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            webcamTexture = new WebCamTexture(devices[devicesIndex].name, this.width, this.height, this.fps);
            centerCamera.texture = webcamTexture;
            webcamTexture.Play();
        }
    }

    void Update()
    {
        if (webcamTexture != null && !webcamTexture.isPlaying)
        {
            Debug.Log("カメラが切断されました。再接続を試みます...");
            webcamTexture.Stop();
            webcamTexture.Play();
        }
    }

    void OnDestroy()
    {
        if (webcamTexture != null)
        {
            webcamTexture.Stop(); // カメラ映像を停止
            webcamTexture = null; // カメラ映像を破棄
        }
    }
}
