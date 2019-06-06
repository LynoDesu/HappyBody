using System;
using Xamarin.Forms;

namespace HappyBody
{
    public class ScaleUpAndDownAction : TriggerAction<VisualElement>
    {
        public ScaleUpAndDownAction()
        {
            Anchor = new Point(0.5, 0.5);
            Scale = 1.2;
            Length = 500;
        }

        public Point Anchor { set; get; }

        public double Scale { set; get; }

        public int Length { set; get; }

        protected override async void Invoke(VisualElement sender)
        {
            sender.AnchorX = Anchor.X;
            sender.AnchorY = Anchor.Y;
            await sender.ScaleTo(Scale, (uint)Length / 2, Easing.SinOut);
            await sender.ScaleTo(1, (uint)Length / 2, Easing.SinIn);
        }
    }
}
