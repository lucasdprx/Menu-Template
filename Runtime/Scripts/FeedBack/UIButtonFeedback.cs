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
    /// <summary>
    /// Provides highly customizable DOTween-based animations (Scale, Color, Punch) for UI Buttons 
    /// on hover, click, and selection events.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class UIButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler,
        ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        [Header("Scale Animation")] 
        [SerializeField] protected bool enableScaleAnimation = true;
        [SerializeField] protected Vector3 scaleMultiplier = Vector3.one * 1.15f;
        [SerializeField] protected float scaleDuration = 0.2f;
        [SerializeField] protected Ease scaleEase = Ease.OutBack;

        [Header("Image Animation")] 
        [SerializeField] protected bool enableImageAnimation = true;
        [SerializeField] protected Color highlightedImageColor = Color.white;
        [SerializeField] protected float imageColorDuration = 0.2f;
        [SerializeField] protected Ease imageColorEase = Ease.OutQuad;

        [Header("Text Animation")] 
        [SerializeField] protected bool enableTextAnimation = true;
        [SerializeField] protected Color highlightedTextColor = Color.white;
        [SerializeField] protected float textColorDuration = 0.05f;
        [SerializeField] protected Ease textColorEase = Ease.OutQuad;
        [SerializeField] protected float fontSizeMultiplier = 1f;
        [SerializeField] protected Ease fontSizeEase = Ease.OutQuad;

        [Header("Punch Animation")] 
        [SerializeField] protected bool enablePunchAnimation = true;
        [SerializeField] protected float punchStrength = 0.1f;
        [SerializeField] protected float punchDuration = 0.2f;
        [SerializeField] protected int punchVibrato = 10;
        [Range(0, 1)] [SerializeField] protected float punchElasticity = 1f;
        [SerializeField] protected Ease punchEase = Ease.OutElastic;

        protected Vector3 initialScale;
        protected Color initialTextColor;
        protected Color initialImageColor;
        protected float initialFontSize;
        protected Graphic targetGraphic;
        protected TextMeshProUGUI label;
        protected Button button;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            targetGraphic = button.targetGraphic;
            label = GetComponentInChildren<TextMeshProUGUI>();

            initialScale = transform.localScale;

            if (targetGraphic != null)
                initialImageColor = targetGraphic.color;

            if (label != null)
            {
                initialTextColor = label.color;
                initialFontSize = label.fontSize;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData) => Punch();
        public virtual void OnSubmit(BaseEventData eventData) => Punch();

        public virtual void OnPointerEnter(PointerEventData eventData) => Highlight();
        public virtual void OnSelect(BaseEventData eventData) => Highlight();

        public virtual void OnPointerExit(PointerEventData eventData) => ResetVisuals(true);
        public virtual void OnDeselect(BaseEventData eventData) => ResetVisuals(true);

        protected virtual void OnDisable() => ResetVisuals(false);
        protected virtual void OnDestroy() => KillAllTweens();

        /// <summary>
        /// Applies the highlighted visual states (scale up, color change). 
        /// Blocked if the button is not interactable.
        /// </summary>
        public virtual void Highlight()
        {
            if (!button.interactable) return;

            KillAllTweens();

            if (enableScaleAnimation)
                transform.DOScale(Vector3.Scale(initialScale, scaleMultiplier), scaleDuration)
                    .SetUpdate(true).SetEase(scaleEase);

            if (enableImageAnimation && targetGraphic != null)
                targetGraphic.DOColorCustom(highlightedImageColor, imageColorDuration)
                    .SetUpdate(true).SetEase(imageColorEase);

            if (enableTextAnimation && label != null)
            {
                label.DOColorCustom(highlightedTextColor, textColorDuration)
                    .SetUpdate(true).SetEase(textColorEase);
                label.DOFontSize(initialFontSize * fontSizeMultiplier, textColorDuration)
                    .SetUpdate(true).SetEase(fontSizeEase);
            }
        }

        /// <summary>
        /// Reverts the button to its initial visual state.
        /// Must execute even if the button is non-interactable to prevent stuck hover states.
        /// </summary>
        /// <param name="animate">If true, tweens back to normal. If false, snaps instantly.</param>
        public virtual void ResetVisuals(bool animate)
        {
            KillAllTweens();

            if (animate)
            {
                if (enableScaleAnimation)
                    transform.DOScale(initialScale, scaleDuration).SetUpdate(true).SetEase(scaleEase);

                if (enableImageAnimation && targetGraphic != null)
                    targetGraphic.DOColorCustom(initialImageColor, imageColorDuration).SetUpdate(true).SetEase(imageColorEase);

                if (enableTextAnimation && label != null)
                {
                    label.DOColorCustom(initialTextColor, textColorDuration).SetUpdate(true).SetEase(textColorEase);
                    label.DOFontSize(initialFontSize, textColorDuration).SetUpdate(true).SetEase(fontSizeEase);
                }

                return;
            }

            if (enableScaleAnimation) transform.localScale = initialScale;
            if (enableImageAnimation && targetGraphic != null) targetGraphic.color = initialImageColor;
            if (enableTextAnimation && label != null)
            {
                label.color = initialTextColor;
                label.fontSize = initialFontSize;
            }
        }

        /// <summary>
        /// Triggers a punch effect (wobble) to simulate a physical click.
        /// </summary>
        public virtual void Punch()
        {
            if (!button.interactable || !enablePunchAnimation) return;

            transform.DOKill(true);
            transform.DOPunchScale(Vector3.one * punchStrength, punchDuration, punchVibrato, punchElasticity)
                .SetUpdate(true).SetEase(punchEase);
        }

        /// <summary>
        /// Cancels all running DOTween animations on this button to prevent memory leaks or overlaps.
        /// </summary>
        public virtual void KillAllTweens()
        {
            transform.DOKill();
            if (targetGraphic != null) targetGraphic.DOKill();
            if (label != null) label.DOKill();
        }
    }
#else
    /// <summary>
    /// Fallback stub for UIButtonFeedback when DOTween is not installed.
    /// Prevents compilation errors while warning the developer in the Inspector.
    /// </summary>
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