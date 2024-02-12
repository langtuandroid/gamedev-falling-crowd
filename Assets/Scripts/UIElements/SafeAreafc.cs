using UnityEngine;

namespace UIElements
{
    public class SafeAreafc : MonoBehaviour
    {
        public bool dontSafeBottomfc;
        private RectTransform fittedRectTransformfc;
        private Rect safeRectComponentfc;
        private Vector2 minAnchorVectorfc;
        private Vector2 maxAnchorVectorfc;

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
