#if DOTWEEN
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;

namespace Menu.FeedBack
{
    public static class DOTweenExtension
    {
        public static TweenerCore<float, float, FloatOptions> DOFontSize(this TextMeshProUGUI text, float targetSize, float duration)
        {
            return DOTween.To(() => text.fontSize, x => text.fontSize = x, targetSize, duration);
        }
    }
}
#endif
