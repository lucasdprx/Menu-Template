#if DOTWEEN
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.FeedBack
{
    /// <summary>
    /// Custom DOTween extensions for TextMeshPro to ensure compatibility 
    /// regardless of whether the user has enabled the DOTween TMP module.
    /// </summary>
    public static class DOTweenExtension
    {
        /// <summary>
        /// Animates the font size of a TextMeshProUGUI component safely.
        /// </summary>
        /// <param name="text">The target TextMeshProUGUI component.</param>
        /// <param name="targetSize">The final font size.</param>
        /// <param name="duration">The duration of the tween.</param>
        public static TweenerCore<float, float, FloatOptions> DOFontSize(this TextMeshProUGUI text, float targetSize, float duration)
        {
            return DOTween.To(() => text.fontSize, x => text.fontSize = x, targetSize, duration);
        }

        /// <summary>
        /// Animates the color of a TextMeshProUGUI component safely.
        /// </summary>
        /// <param name="text">The target TextMeshProUGUI component.</param>
        /// <param name="endValue">The final color.</param>
        /// <param name="duration">The duration of the tween.</param>
        public static TweenerCore<Color, Color, ColorOptions> DOColorCustom(this TextMeshProUGUI text, Color endValue, float duration)
        {
            return DOTween.To(() => text.color, x => text.color = x, endValue, duration);
        }
        
        /// <summary>
        /// Animates the color of a Graphic component safely.
        /// </summary>
        /// <param name="image">The target Graphic component.</param>
        /// <param name="endValue">The final color.</param>
        /// <param name="duration">The duration of the tween.</param>
        public static TweenerCore<Color, Color, ColorOptions> DOColorCustom(this Graphic image, Color endValue, float duration)
        {
            return DOTween.To(() => image.color, x => image.color = x, endValue, duration);
        }
    }
}
#endif
