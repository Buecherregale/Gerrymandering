using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{

[SerializeField] private TMP_Dropdown resolutionDropdown;
[SerializeField] private CanvasScaler canvasScaler;

    // Start is called before the first frame update
    void Start()
    {
        resolutionDropdown.onValueChanged.AddListener(delegate { ChangeResolution(resolutionDropdown.value); });
    }

    public void ChangeResolution(int index) {
        int[] res = new int[2];
        string text = resolutionDropdown.options[index].text;
        string[] split = text.Split('x');
        for (int i = 0; i < split.Length; i++) {
            split[i] = split[i].Trim();
            res[i] = int.Parse(split[i]);
        }
        Debug.Log(res[0] + ",:," + res[1]);
        ChangeResolution(res[0], res[1]);
    }
    
    private void ChangeResolution(int width, int height) {
        //TODO: make the Resolution actually change
        //Camera.main.aspect = (float) width / height;
        //canvasScaler.referenceResolution = new Vector2(width, height);
        Screen.SetResolution(width, height, Screen.fullScreen);
    }
    
}
