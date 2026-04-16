#if DOTWEEN
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.FeedBack
{
    public static class DOTweenExtension
    {
        public static TweenerCore<float, float, FloatOptions> DOFontSize(this TextMeshProUGUI text, float targetSize, float duration)
        {
            return DOTween.To(() => text.fontSize, x => text.fontSize = x, targetSize, duration);
        }

        public static TweenerCore<Color, Color, ColorOptions> DOColor(this TextMeshProUGUI text, Color endValue, float duration)
        {
            return DOTween.To(() => text.color, x => text.color = x, endValue, duration);
        }
        
        public static TweenerCore<Color, Color, ColorOptions> DOColor(this Image image, Color endValue, float duration)
        {
            return DOTween.To(() => image.color, x => image.color = x, endValue, duration);
        }
    }
}
#endif
