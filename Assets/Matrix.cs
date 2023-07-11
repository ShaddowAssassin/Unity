using UnityEngine;
using UnityEngine.UI;

public class Matrix : MonoBehaviour
{
    private const string Path = "LegacyRuntime.ttf";
    private Text text;
    private float characterSpeed = 1f;
    private float characterOffset = 0f;
    private int numberOfLines = 60;
    private int lineLength = 300;

    void Awake()
    {
        // Load the Arial font from the Unity Resources folder.
        Font arial = (Font)Resources.GetBuiltinResource(typeof(Font), Path);

        // Create Canvas GameObject.
        GameObject canvasGO = new GameObject();
        canvasGO.name = "Canvas";
        canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Get canvas from the GameObject.
        Canvas canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Create the Text GameObject.
        GameObject textGO = new GameObject();
        textGO.transform.parent = canvasGO.transform;
        textGO.AddComponent<Text>();

        // Set Text component properties.
        text = textGO.GetComponent<Text>();
        text.font = arial;
        text.text = "";
        text.fontSize = 50;
        text.alignment = TextAnchor.MiddleCenter;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Overflow;

        // Provide Text position and size using RectTransform.
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(1200, 1000);

        // Set the color of the text to green
        text.color = Color.green;
    }

    void Update()
    {
        characterOffset -= Time.deltaTime * characterSpeed;
        RenderFrame();
    }

    void RenderFrame()
    {
        // Calculate the vertical offset for each character
        string updatedText = "";
        for (int i = 0; i < numberOfLines; i++)
        {
            string line = "";
            for (int j = 0; j < lineLength; j++)
            {
                if (Random.value < 0.1f) // 10% chance to generate a new random character
                {
                    line += GetRandomCharacter();
                }
                else
                {
                    line += " ";
                }
            }
            updatedText += line + "\n";
        }

        text.text = updatedText;
    }

    char GetRandomCharacter()
    {
        int randomAscii = Random.Range(32, 127); // ASCII range for printable characters
        return (char)randomAscii;
    }
}
