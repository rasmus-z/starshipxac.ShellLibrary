using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace starshipxac.Windows.Controls
{
    [TemplateVisualState(Name = "Inactive", GroupName = "ActiveStates")]
    [TemplateVisualState(Name = "Active", GroupName = "ActiveStates")]
    public class ProgressLinear : Control
    {
        private Storyboard storyboard = null;

        static ProgressLinear()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressLinear), new FrameworkPropertyMetadata(typeof(ProgressLinear)));
        }

        public ProgressLinear()
        {
            this.SizeChanged += OnSizeChanged;
        }

        #region IsActive Property

        public bool IsActive
        {
            get
            {
                return (bool)GetValue(IsActiveProperty);
            }
            set
            {
                SetValue(IsActiveProperty, value);
            }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(ProgressLinear),
            new PropertyMetadata(false, OnIsActivePropertyChanged));

        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProgressLinear;
            if (control != null)
            {
                control.UpdateActiveState();
            }
        }

        #endregion

        #region ContainerAnimationStartPosition Property

        public double ContainerAnimationStartPosition
        {
            get
            {
                return (double)GetValue(ContainerAnimationStartPositionProperty);
            }
            private set
            {
                SetValue(ContainerAnimationStartPositionPropertyKey, value);
            }
        }

        private static readonly DependencyPropertyKey ContainerAnimationStartPositionPropertyKey = DependencyProperty
            .RegisterReadOnly(
                "ContainerAnimationStartPosition", typeof(double), typeof(ProgressLinear),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ContainerAnimationStartPositionProperty =
            ContainerAnimationStartPositionPropertyKey.DependencyProperty;

        #endregion

        #region ContainerAnimationEndPosition Property

        public double ContainerAnimationEndPosition
        {
            get
            {
                return (double)GetValue(ContainerAnimationEndPositionProperty);
            }
            private set
            {
                SetValue(ContainerAnimationEndPositionPropertyKey, value);
            }
        }

        private static readonly DependencyPropertyKey ContainerAnimationEndPositionPropertyKey = DependencyProperty
            .RegisterReadOnly(
                "ContainerAnimationEndPosition", typeof(double), typeof(ProgressLinear),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ContainerAnimationEndPositionProperty =
            ContainerAnimationEndPositionPropertyKey.DependencyProperty;

        #endregion

        #region EllipseAnimationWellPosition Property

        public double EllipseAnimationWellPosition
        {
            get
            {
                return (double)GetValue(EllipseAnimationWellPositionProperty);
            }
            private set
            {
                SetValue(EllipseAnimationWellPositionPropertyKey, value);
            }
        }

        private static readonly DependencyPropertyKey EllipseAnimationWellPositionPropertyKey = DependencyProperty.RegisterReadOnly
            (
                "EllipseAnimationWellPosition", typeof(double), typeof(ProgressLinear),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty EllipseAnimationWellPositionProperty =
            EllipseAnimationWellPositionPropertyKey.DependencyProperty;

        #endregion

        #region EllipseAnimationEndPosition Property

        public double EllipseAnimationEndPosition
        {
            get
            {
                return (double)GetValue(EllipseAnimationEndPositionProperty);
            }
            private set
            {
                SetValue(EllipseAnimationEndPositionPropertyKey, value);
            }
        }

        private static readonly DependencyPropertyKey EllipseAnimationEndPositionPropertyKey = DependencyProperty.RegisterReadOnly(
            "EllipseAnimationEndPosition", typeof(double), typeof(ProgressLinear),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty EllipseAnimationEndPositionProperty =
            EllipseAnimationEndPositionPropertyKey.DependencyProperty;

        #endregion

        #region EllipseDiameter Property

        public double EllipseDiameter
        {
            get
            {
                return (double)GetValue(EllipseDiameterProperty);
            }
            private set
            {
                SetValue(EllipseDiameterPropertyKey, value);
            }
        }

        private static readonly DependencyPropertyKey EllipseDiameterPropertyKey = DependencyProperty.RegisterReadOnly(
            "EllipseDiameter", typeof(double), typeof(ProgressLinear),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty EllipseDiameterProperty = EllipseDiameterPropertyKey.DependencyProperty;

        #endregion

        #region EllipseOffset Property

        public double EllipseOffset
        {
            get
            {
                return (double)GetValue(EllipseOffsetProperty);
            }
            private set
            {
                SetValue(EllipseOffsetPropertyKey, value);
            }
        }

        private static readonly DependencyPropertyKey EllipseOffsetPropertyKey = DependencyProperty.RegisterReadOnly(
            "EllipseOffset", typeof(double), typeof(ProgressLinear),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty EllipseOffsetProperty = EllipseOffsetPropertyKey.DependencyProperty;

        #endregion

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                UpdateProperties();
                if (this.IsActive)
                {
                    BeginStoryboard();
                }
            }
        }

        private void BeginStoryboard()
        {
            this.storyboard = CreateStoryboad();
            if (this.storyboard != null)
            {
                this.storyboard.Begin();
            }
        }

        private void StopStoryboard()
        {
            if (this.storyboard != null)
            {
                this.storyboard.Stop();
            }
        }

        private void UpdateActiveState()
        {
            if (this.IsActive)
            {
                VisualStateManager.GoToState(this, "Active", true);
                UpdateProperties();
                BeginStoryboard();
            }
            else
            {
                VisualStateManager.GoToState(this, "Inactive", true);
                StopStoryboard();
            }
        }

        private void UpdateProperties()
        {
            if (this.ActualWidth <= 181)
            {
                this.ContainerAnimationStartPosition = -34;
                this.ContainerAnimationEndPosition = (3.917 / 9.0 * this.ActualWidth) - (309.19 / 9.0);
                this.EllipseDiameter = 4;
                this.EllipseOffset = 4;
            }
            else if (this.ActualWidth <= 381)
            {
                this.ContainerAnimationStartPosition = -50.5;
                this.ContainerAnimationEndPosition = (3.917 / 9.0 * this.ActualWidth) - (458.417 / 9.0);
                this.EllipseDiameter = 5;
                this.EllipseOffset = 7;
            }
            else
            {
                this.ContainerAnimationStartPosition = -63.0;
                this.ContainerAnimationEndPosition = (3.917 / 9.0 * this.ActualWidth) - (570.917 / 9.0);
                this.EllipseDiameter = 6;
                this.EllipseOffset = 9;
            }

            this.EllipseAnimationWellPosition = (this.ActualWidth - 1) / 3.0;
            this.EllipseAnimationEndPosition = (this.ActualWidth - 1) * 2.0 / 3.0;
        }

        #region Create Storyboard

        private Storyboard CreateStoryboad()
        {
            if (this.Template == null)
            {
                return null;
            }

            var ellipseGrid = this.Template.FindName("EllipseGrid", this) as DependencyObject;
            if (ellipseGrid == null)
            {
                return null;
            }

            const int EllipseCount = 5;
            var result = new Storyboard() { RepeatBehavior = RepeatBehavior.Forever };

            var gx = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(3.917),
                From = this.ContainerAnimationStartPosition,
                To = this.ContainerAnimationEndPosition
            };
            Storyboard.SetTarget(gx, ellipseGrid);
            Storyboard.SetTargetProperty(gx, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
            result.Children.Add(gx);

            for (var index = 0; index < EllipseCount; ++index)
            {
                var e = new DoubleAnimationUsingKeyFrames();
                Storyboard.SetTarget(e, GetControl("E", index));
                Storyboard.SetTargetProperty(e, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

                var delay = 0.5 / 3 * index;

                e.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
                e.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0 + delay)));
                e.KeyFrames.Add(new SplineDoubleKeyFrame(this.EllipseAnimationWellPosition, TimeSpan.FromSeconds(1 + delay),
                    new KeySpline(0.4, 0.0, 0.6, 1.0)));
                e.KeyFrames.Add(new EasingDoubleKeyFrame(this.EllipseAnimationWellPosition, TimeSpan.FromSeconds(2 + delay)));
                e.KeyFrames.Add(new SplineDoubleKeyFrame(this.EllipseAnimationEndPosition, TimeSpan.FromSeconds(3 + delay),
                    new KeySpline(0.4, 0.0, 0.6, 1.0)));

                result.Children.Add(e);
            }

            for (var index = 0; index < EllipseCount; ++index)
            {
                var b = new DoubleAnimationUsingKeyFrames();
                Storyboard.SetTarget(b, GetControl("B", index));
                Storyboard.SetTargetProperty(b, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

                var delay = 0.5 / 3.0 * index;

                b.KeyFrames.Add(new EasingDoubleKeyFrame(-50, TimeSpan.FromSeconds(0)));
                b.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0.5 + delay)));
                b.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(2.0 + delay)));
                b.KeyFrames.Add(new EasingDoubleKeyFrame(100, TimeSpan.FromSeconds(3.0 + delay)));

                result.Children.Add(b);
            }

            var go = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.0),
                To = 1
            };
            Storyboard.SetTarget(go, ellipseGrid);
            Storyboard.SetTargetProperty(go, new PropertyPath("Opacity"));
            result.Children.Add(go);

            result.Freeze();
            return result;
        }

        private DependencyObject GetControl(string prefix, int index)
        {
            return this.Template.FindName(String.Format("{0}{1}", prefix, index + 1), this) as DependencyObject;
        }

        #endregion
    }
}