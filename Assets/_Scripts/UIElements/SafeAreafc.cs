using UnityEngine;

namespace _Scripts.UIElements
{
    public class SafeAreafc : MonoBehaviour
    {
        public bool dontSafeBottomfc;
        RectTransform fittedRectTransformfc;
        Rect safeRectComponentfc;
        Vector2 minAnchorVectorfc;
        Vector2 maxAnchorVectorfc;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            fittedRectTransformfc = GetComponent<RectTransform>();
            safeRectComponentfc = Screen.safeArea;
            minAnchorVectorfc = safeRectComponentfc.position;
            maxAnchorVectorfc = minAnchorVectorfc + safeRectComponentfc.size;
        
            minAnchorVectorfc.x /= Screen.width;
            minAnchorVectorfc.y = dontSafeBottomfc ? minAnchorVectorfc.y = 0 : minAnchorVectorfc.y /= Screen.height;
            maxAnchorVectorfc.x /= Screen.width;
            maxAnchorVectorfc.y /= Screen.height;
        
            fittedRectTransformfc.anchorMin = minAnchorVectorfc;
            fittedRectTransformfc.anchorMax = maxAnchorVectorfc;

        }
    }
}
