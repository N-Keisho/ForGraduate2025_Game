using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WebCam : MonoBehaviour
{
    [SerializeField] RawImage centerCamera;
    int devicesIndex = 0;
    private int width = 640;
    private int height = 480;
    private int fps = 30;
    [SerializeField] private GameObject cameraButton;
    static private WebCamTexture webcamTexture;
    static private bool isCameraAvailable = false;

    IEnumerator Start()
    {
        if(cameraButton != null) cameraButton.SetActive(false);

        if (!isCameraAvailable)
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                isCameraAvailable = true;
            }
        }


        if (isCameraAvailable)
        {
            devicesIndex = GlobalVariables.cameraIndex;
            if (webcamTexture == null)
            {
                WebCamDevice[] devices = WebCamTexture.devices;
                if(devices.Length > 1 && cameraButton != null) cameraButton.SetActive(true);
                webcamTexture = new WebCamTexture(devices[devicesIndex].name, this.width, this.height, this.fps);
                centerCamera.texture = webcamTexture;
                webcamTexture.Play();
            }
            else
            {
                centerCamera.texture = webcamTexture; // 既存のWebCamTextureを再利用
                webcamTexture.Play();
            }
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
        if (Input.GetKeyDown(KeyCode.Return) && SceneManager.GetActiveScene().name == "Explain")
        {
            ChangeCamera(); // カメラを切り替える
        }
    }

    void ChangeCamera()
    {
        if (webcamTexture != null && WebCamTexture.devices.Length > 1)
        {
            webcamTexture.Stop(); // カメラ映像を停止
            devicesIndex = (devicesIndex + 1) % WebCamTexture.devices.Length; // 次のカメラに切り替え
            GlobalVariables.cameraIndex = devicesIndex;
            webcamTexture = new WebCamTexture(WebCamTexture.devices[devicesIndex].name, this.width, this.height, this.fps);
            centerCamera.texture = webcamTexture;
            webcamTexture.Play(); // 新しいカメラ映像を再生
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
