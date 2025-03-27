using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WebCam : MonoBehaviour
{
    [SerializeField] RawImage rawImage;
    [SerializeField] int devicesIndex = 0;
    private int width = 1920;
    private int height = 1080;
    private int fps = 30;
    private WebCamTexture webcamTexture;
    
    void Start () {
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(devices[devicesIndex].name, this.width, this.height, this.fps);
        rawImage.texture = webcamTexture;
        webcamTexture.Play();
    }
}
