using System.IO;
using UnityEngine;
using UnityEngine.UI;
using aus.Extension;

public class FaceLandmarksTester
    : MonoBehaviour
{
    [Header("Visualize")]
    public RawImage input;
    public RawImage output;


    #region Unity message handlers
    void Start()
    {
        var inputImageFilePath = Application.streamingAssetsPath + "/image/face1.png";
        var imageBin = File.ReadAllBytes(inputImageFilePath);
        var inputTex = new Texture2D(1, 1); // no means
        inputTex.LoadImage(imageBin);
        input.texture = inputTex;

        // facial landmark model
        var onnxFilePath = Application.streamingAssetsPath + "/onnx/pfld.onnx";
        var landmarks = Onnx.Predict2DPositions(onnxFilePath, inputTex);

        // visualize
        var outputTex = new Texture2D(inputTex.width, inputTex.height, inputTex.format, true);
        Graphics.CopyTexture(inputTex, outputTex);
        foreach (var l in landmarks)
        {
            outputTex.FillEllipse(Color.red, l, Vector2Int.one * 5);
        }
        outputTex.Apply();
        output.texture = outputTex;
    }
    #endregion
}
