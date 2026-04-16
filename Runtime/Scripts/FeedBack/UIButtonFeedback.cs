#if DOTWEEN
using DG.Tweening;
#endif
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.FeedBack
{
    #if DOTWEEN
    [RequireComponent(typeof(Button))]
    public class UIButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        public bool enableScaleAnimation = true;
        public Vector3 scaleMultiplier = Vector3.one * 1.15f;
        public float scaleDuration = 0.2f;
        public Ease scaleEase = Ease.OutBack;

        public bool enableImageAnimation = true;
        public Color highlightedImageColor = Color.white;
        public float imageColorDuration = 0.2f;
        public Ease imageColorEase = Ease.OutQuad;

        public bool enableTextAnimation = true;
        public Color highlightedTextColor = Color.white;
        public Ease textColorEase = Ease.OutQuad;
        public float textColorDuration = 0.05f;
        public float fontSizeMultiplier = 1f;
        public Ease fontSizeEase = Ease.OutQuad;

        public bool enablePunchAnimation = true;
        public float punchStrength = 0.1f;
        public float punchDuration = 0.2f;
        public int punchVibrato = 10;
        [Range(0, 1)] public float punchElasticity = 1F;
        public Ease punchEase = Ease.OutElastic;

        private Vector3 initialScale;
        private Color initialTextColor;
        private Color initialImageColor;
        private float initialFontSize;
        private Image targetImage;
        private TextMeshProUGUI label;
        private Button button;

        private void Awake()
        {
            targetImage = GetComponent<Image>();
            label = GetComponentInChildren<TextMeshProUGUI>();
            button = GetComponent<Button>();

            initialScale = transform.localScale;
            initialImageColor = targetImage.color;
            if (label != null)
            {
                initialTextColor = label.color;
                initialFontSize = label.fontSize;
            }
        }
        public void OnPointerDown(PointerEventData eventData) => Punch();
        public void OnPointerEnter(PointerEventData eventData) => Highlight();
        public void OnSelect(BaseEventData eventData) => Highlight();
        public void OnPointerExit(PointerEventData eventData) => ResetVisuals(true);
        public void OnDeselect(BaseEventData eventData) => ResetVisuals(true);
        private void OnDisable() => ResetVisuals(false);

        public void Highlight()
        {
            if (!button.interactable)
                return;

            KillAllTweens();


            if (enableScaleAnimation)
                transform.DOScale(Vector3.Scale(initialScale, scaleMultiplier), scaleDuration)
                    .SetUpdate(true)
                    .SetEase(scaleEase);
            if (enableImageAnimation)
                targetImage.DOColor(highlightedImageColor, imageColorDuration)
                    .SetUpdate(true)
                    .SetEase(imageColorEase);

            if (label != null && enableTextAnimation)
            {
                label.DOColor(highlightedTextColor, textColorDuration)
                    .SetUpdate(true)
                    .SetEase(textColorEase);
                label.DOFontSize(initialFontSize * fontSizeMultiplier, textColorDuration)
                    .SetUpdate(true)
                    .SetEase(fontSizeEase);
            }
        }

        public void ResetVisuals(bool animate)
        {
            if (!button.interactable)
                return;

            KillAllTweens();

            if (animate)
            {
                if (enableScaleAnimation)
                    transform.DOScale(initialScale, scaleDuration)
                        .SetUpdate(true)
                        .SetEase(scaleEase);

                if (enableImageAnimation)
                    targetImage.DOColor(initialImageColor, imageColorDuration)
                        .SetUpdate(true)
                        .SetEase(imageColorEase);

                if (enableTextAnimation && label != null)
                {
                    label.DOColor(initialTextColor, textColorDuration)
                        .SetUpdate(true)
                        .SetEase(textColorEase);

                    label.DOFontSize(initialFontSize, textColorDuration)
                        .SetUpdate(true)
                        .SetEase(fontSizeEase);
                }

                return;
            }

            if (enableScaleAnimation)
                transform.localScale = initialScale;

            if (enableImageAnimation)
                targetImage.color = initialImageColor;

            if (enableTextAnimation && label != null)
            {
                label.color = initialTextColor;
                label.fontSize = initialFontSize;
            }
        }

        public void Punch()
        {
            if (!button.interactable || !enablePunchAnimation)
                return;

            transform.DOKill(true);
            transform.DOPunchScale(Vector3.one * punchStrength, punchDuration, punchVibrato, punchElasticity)
                .SetUpdate(true)
                .SetEase(punchEase);
        }

        public void KillAllTweens()
        {
            transform.DOKill();
            targetImage.DOKill();
            if (label != null)
            {
                label.DOKill();
            }
        }

        private void OnDestroy()
        {
            KillAllTweens();
        }
    }
    #else
    [RequireComponent(typeof(Button))]
    public class UIButtonFeedback : MonoBehaviour
    {
        #pragma warning disable CS0414
        [SerializeField, Tooltip("Installez DOTween pour activer le feedback visuel.")]
        private string DOTweenMissingInfo = "DOTween n'est pas détecté. L'animation des boutons est désactivée.";
        #pragma warning restore CS0414
    }
    #endif
}
