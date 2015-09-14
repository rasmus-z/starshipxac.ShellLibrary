using System;
using System.Diagnostics;
using System.Windows;
using starshipxac.Windows.Dialogs;
using starshipxac.Windows.Dialogs.Controls;

namespace TaskDialogSample.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (this.DynamicDataContext != null)
            {
                this.DynamicDataContext.Loading(this);
            }
        }

        private dynamic DynamicDataContext => this.DataContext;

        public void ShowSimpleTaskDialog()
        {
            using (var taskDialog = new TaskDialog())
            {
                taskDialog.Title = "Simple Sample";
                taskDialog.MainInstructionText = "Common Buttons";
                taskDialog.CommonButtons =
                    TaskDialogCommonButtons.Ok |
                    TaskDialogCommonButtons.Yes |
                    TaskDialogCommonButtons.No |
                    TaskDialogCommonButtons.Cancel |
                    TaskDialogCommonButtons.Retry |
                    TaskDialogCommonButtons.Close;

                var result = taskDialog.Show();

                Debug.WriteLine("SelectedButton = " + result.SelectedButton);
                Debug.WriteLine("SelectedRadioButton = " + result.SelectedRadioButton);
                Debug.WriteLine("VerificationChecked = " + result.VerificationChecked);
            }
        }

        public void ShowAllControlsTaskDialog()
        {
            using (var taskDialog = new TaskDialog())
            {
                var minimum = 0;
                var maximum = 10000;

                taskDialog.Cancelable = true;
                taskDialog.Title = "All Controls Sample";
                taskDialog.MainInstructionText = "Main Instruction Text.";
                taskDialog.ContentText = "Content Text.";
                taskDialog.FooterText = "Footer Text.";
                taskDialog.ExpandedText = "Expanded Text.";
                taskDialog.ExpandedControlText = "Expanded Label";
                taskDialog.CollapsedControlText = "Collapsed Label";
                taskDialog.VerificationText = "Verification Text.";
                taskDialog.MainIcon = TaskDialogIcon.Information;
                taskDialog.FooterIcon = TaskDialogIcon.Warning;
                taskDialog.ProgressBar = new TaskDialogProgressBar("progress")
                {
                    Minimum = minimum,
                    Maximum = maximum,
                    Value = 0
                };
                taskDialog.CommonButtons = TaskDialogCommonButtons.Ok;

                taskDialog.CommandLinks.Add(new TaskDialogCommandLink("link1", "Command Link1"));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink("link2", "Command Link2"));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink("link3", "Command Link3"));

                taskDialog.Timer += (sender, args) =>
                {
                    var dialog = sender as TaskDialog;
                    if (dialog != null)
                    {
                        if (dialog.ProgressBar is TaskDialogProgressBar)
                        {
                            var progressBar = dialog.ProgressBar as TaskDialogProgressBar;

                            if (args.Ticks <= maximum)
                            {
                                progressBar.Value = args.Ticks;
                            }
                            else
                            {
                                progressBar.Value = maximum;
                            }
                        }
                    }
                };

                var result = taskDialog.Show();

                Debug.WriteLine("SelectedButton = " + result.SelectedButton);
                Debug.WriteLine("SelectedRadioButton = " + result.SelectedRadioButton);
                Debug.WriteLine("VerificationChecked = " + result.VerificationChecked);
            }
        }

        public void ShowCustomButtonTaskDialog()
        {
            using (var taskDialog = new TaskDialog())
            {
                taskDialog.Title = "Custom Button Sample";
                taskDialog.MainInstructionText = "Custom Buttons";
                taskDialog.Cancelable = true;

                taskDialog.CustomButtons.Add(new TaskDialogButton("button4", "Custom Button", true));
                taskDialog.CustomButtons.Add(new TaskDialogButton("button5", "Custom Button(Not Close Dialog)"));
                foreach (var button in taskDialog.CustomButtons)
                {
                    button.Click += (sender, args) => Debug.WriteLine("Click: {0}", sender);
                }

                var result = taskDialog.Show();

                Debug.WriteLine("SelectedButton = " + result.SelectedButton);
                Debug.WriteLine("SelectedRadioButton = " + result.SelectedRadioButton);
                Debug.WriteLine("VerificationChecked = " + result.VerificationChecked);
            }
        }

        public void ShowCommandLinkTaskDialog()
        {
            using (var taskDialog = new TaskDialog())
            {
                taskDialog.Title = "Command Link Sample";
                taskDialog.Cancelable = true;
                taskDialog.CommonButtons = TaskDialogCommonButtons.Close;

                taskDialog.CommandLinks.Add(new TaskDialogCommandLink("link1", "Custom Link1", true));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink("link2", "Custom Link2", "Close Dialog", true));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink("link3", "Custom Link3", "Close Dialog", true));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink("link4", "Custom Link4", "Not Close Dialog"));
                foreach (var commandLink in taskDialog.CommandLinks)
                {
                    commandLink.Click += (sender, args) => Debug.WriteLine("Click: {0}", sender);
                }

                var result = taskDialog.Show();

                Debug.WriteLine("SelectedButton = " + result.SelectedButton);
                Debug.WriteLine("SelectedRadioButton = " + result.SelectedRadioButton);
                Debug.WriteLine("VerificationChecked = " + result.VerificationChecked);
            }
        }

        public void ShowRadioButtonTaskDialog()
        {
            using (var taskDialog = new TaskDialog())
            {
                taskDialog.Title = "Radio Button Sample";
                taskDialog.Cancelable = true;
                taskDialog.CommonButtons = TaskDialogCommonButtons.Close;

                taskDialog.RadioButtons.Add(new TaskDialogRadioButton("radio1", "Radio Button1") { Default = true });
                taskDialog.RadioButtons.Add(new TaskDialogRadioButton("radio2", "Radio Button2"));
                taskDialog.RadioButtons.Add(new TaskDialogRadioButton("radio3", "Radio Button3"));
                taskDialog.RadioButtons.Add(new TaskDialogRadioButton("radio4", "Radio Button4"));
                taskDialog.RadioButtons.Add(new TaskDialogRadioButton("radio5", "Radio Button5"));

                var result = taskDialog.Show();

                Debug.WriteLine("SelectedButton = " + result.SelectedButton);
                Debug.WriteLine("SelectedRadioButton = " + result.SelectedRadioButton);
                Debug.WriteLine("VerificationChecked = " + result.VerificationChecked);
            }
        }

        public void ShowMarqueeTaskDialog()
        {
            using (var taskDialog = new TaskDialog())
            {
                taskDialog.Title = "Marquee Sample";
                taskDialog.Cancelable = true;
                taskDialog.ProgressBar = new TaskDialogMarquee("Marquee");

                taskDialog.Show();
            }
        }
    }
}