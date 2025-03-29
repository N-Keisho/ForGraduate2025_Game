using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WebCam : MonoBehaviour
{
    [SerializeField] RawImage centerCamera;
    [SerializeField] int devicesIndex = 0;
    private int width = 640;
    private int height = 480;
    private int fps = 30;
    static private WebCamTexture webcamTexture;

    void Start()
    {
        if (webcamTexture == null)
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            webcamTexture = new WebCamTexture(devices[devicesIndex].name, this.width, this.height, this.fps);
            centerCamera.texture = webcamTexture;
            webcamTexture.Play();
        }
        else{
            centerCamera.texture = webcamTexture; // 既存のWebCamTextureを再利用
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
        if (webcamTexture != null && SceneManager.GetActiveScene().name == "Result")
        {
            webcamTexture.Stop(); // カメラ映像を停止
            webcamTexture = null; // カメラ映像を破棄
        }
    }
}
