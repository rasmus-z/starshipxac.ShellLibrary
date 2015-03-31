using System;
using System.Windows;
using System.Windows.Controls;

namespace starshipxac.Windows.Controls
{
    [TemplateVisualState(Name = "Large", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Small", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Inactive", GroupName = "ActiveStates")]
    [TemplateVisualState(Name = "Active", GroupName = "ActiveStates")]
    public class ProgressRing : Control
    {
        static ProgressRing()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(typeof(ProgressRing)));
        }

        public ProgressRing()
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
            "IsActive", typeof(bool), typeof(ProgressRing),
            new PropertyMetadata(false, (d, e) =>
            {
                var control = d as ProgressRing;
                if (control != null)
                {
                    control.UpdateActiveState();
                }
            }));

        #endregion

        #region MaxSideLength Property

        public double MaxSideLength
        {
            get
            {
                return (double)GetValue(MaxSideLengthProperty);
            }
            private set
            {
                SetValue(MaxSideLengthPropertyKey, value);
            }
        }

        private static readonly DependencyPropertyKey MaxSideLengthPropertyKey = DependencyProperty.RegisterReadOnly(
            "MaxSideLength", typeof(double), typeof(ProgressRing),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty MaxSideLengthProperty = MaxSideLengthPropertyKey.DependencyProperty;

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
            "EllipseDiameter", typeof(double), typeof(ProgressRing),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty EllipseDiameterProperty = EllipseDiameterPropertyKey.DependencyProperty;

        #endregion

        #region EllipseOffset Property

        public Thickness EllipseOffset
        {
            get
            {
                return (Thickness)GetValue(EllipseOffsetProperty);
            }
            private set
            {
                SetValue(EllipseOffsetPropertyKey, value);
            }
        }

        private static readonly DependencyPropertyKey EllipseOffsetPropertyKey = DependencyProperty.RegisterReadOnly(
            "EllipseOffset", typeof(Thickness), typeof(ProgressRing),
            new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty EllipseOffsetProperty = EllipseOffsetPropertyKey.DependencyProperty;

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateActiveState();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateProperties();
        }

        private void UpdateActiveState()
        {
            if (this.IsActive)
            {
                VisualStateManager.GoToState(this, "Active", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Inactive", true);
            }
        }

        private void UpdateProperties()
        {
            if (this.ActualWidth <= 25)
            {
                this.MaxSideLength = 20;
                this.EllipseDiameter = 3;
                this.EllipseOffset = new Thickness(0.0, 7.0, 0.0, 0.0);
            }
            else if (this.ActualWidth <= 30)
            {
                this.MaxSideLength = this.ActualWidth - 5;
                this.EllipseDiameter = (this.ActualWidth / 10.0) + 0.5;
                this.EllipseOffset = new Thickness(0.0, this.ActualWidth * (9.0 / 20) - (9.0 / 2.0), 0.0, 0.0);
            }
            else
            {
                this.MaxSideLength = this.ActualWidth - 5;
                this.EllipseDiameter = (this.ActualWidth / 10.0) + 0.5;
                this.EllipseOffset = new Thickness(0.0, this.ActualWidth * (2.0 / 5.0) - 2.0, 0.0, 0.0);
            }
        }
    }
}