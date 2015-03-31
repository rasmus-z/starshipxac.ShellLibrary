using System;
using System.Diagnostics;
using System.Windows;
using starshipxac.Windows.Dialogs;
using starshipxac.Windows.Dialogs.Controls;
using TaskDialogSample.Manipulations;

namespace TaskDialogSample.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window, IMainWindowManipulator
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DynamicContext.Loading(this);
        }

        private dynamic DynamicContext
        {
            get
            {
                return this.DataContext;
            }
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            this.DynamicContext.Initialize();
        }

        public void ShowSimpleTaskDialog()
        {
            using (var taskDialog = new TaskDialog())
            {
                taskDialog.Title = "Simple Sample";
                taskDialog.MainInstructionText = "Common Buttons Sample";
                taskDialog.CommonButtons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No;
                var result = taskDialog.Show();

                Debug.WriteLine("SelectedButtonId = " + result.SelectedButtonId);
                Debug.WriteLine("SelectedRadioButtonId = " + result.SelectedRadioButtonId);
                Debug.WriteLine("VerificationChecked = " + result.VerificationChecked);
            }
        }

        public void ShowAllPartsTaskDialog()
        {
            using (var taskDialog = new TaskDialog())
            {
                taskDialog.Cancelable = true;
                taskDialog.Title = "All Parts Sample";
                taskDialog.MainInstructionText = "Main Instruction Text.";
                taskDialog.ContentText = "Content Text.";
                taskDialog.FooterText = "Footer Text.";
                taskDialog.ExpandedText = "Expanded Text.";
                taskDialog.ExpandedControlText = "Expanded Label";
                taskDialog.CollapsedControlText = "Collapsed Label";
                taskDialog.VerificationText = "Verification Text.";
                taskDialog.MainIcon = TaskDialogIcon.Information;
                taskDialog.FooterIcon = TaskDialogIcon.Warning;
                taskDialog.CommonButtons = TaskDialogCommonButtons.Ok;

                const int id = (int)TaskDialogCommonButtonId.MinCustomControlId;
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink(id + 0, "link1", "Command Link1"));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink(id + 1, "link2", "Command Link2"));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink(id + 2, "link3", "Command Link3"));

                var result = taskDialog.Show();

                Debug.WriteLine("SelectedButtonId = " + result.SelectedButtonId);
                Debug.WriteLine("SelectedRadioButtonId = " + result.SelectedRadioButtonId);
                Debug.WriteLine("VerificationChecked = " + result.VerificationChecked);
            }
        }

        public void ShowCustomButtonTaskDialog()
        {
            using (var taskDialog = new TaskDialog())
            {
                taskDialog.Title = "Custom Button Sample";
                taskDialog.Cancelable = true;
                const int id = (int)TaskDialogCommonButtonId.MinCustomControlId;

                var button1 = new TaskDialogButton(TaskDialogCommonButtonId.Ok, "button1", "OK");
                taskDialog.CustomButtons.Add(button1);

                var button2 = new TaskDialogButton(TaskDialogCommonButtonId.Retry, "button2", "Retry");
                taskDialog.CustomButtons.Add(button2);

                var button3 = new TaskDialogButton(TaskDialogCommonButtonId.Cancel, "button3", "Cancel");
                taskDialog.CustomButtons.Add(button3);

                var button4 = new TaskDialogButton(id + 0, "button4", "Custom Button", true);
                taskDialog.CustomButtons.Add(button4);

                var result = taskDialog.Show();

                Debug.WriteLine("SelectedButtonId = " + result.SelectedButtonId);
                Debug.WriteLine("SelectedRadioButtonId = " + result.SelectedRadioButtonId);
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

                const int id = (int)TaskDialogCommonButtonId.MinCustomControlId;
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink(TaskDialogCommonButtonId.Ok, "link1", "OK"));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink(TaskDialogCommonButtonId.Retry, "link2", "Retry"));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink(TaskDialogCommonButtonId.Close, "link3", "Cancel"));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink(id + 0, "link4", "Custom Link1", "Close Dialog", true));
                taskDialog.CommandLinks.Add(new TaskDialogCommandLink(id + 1, "link5", "Custom Link2", "Not Close Dialog"));
                var result = taskDialog.Show();

                Debug.WriteLine("SelectedButtonId = " + result.SelectedButtonId);
                Debug.WriteLine("SelectedRadioButtonId = " + result.SelectedRadioButtonId);
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

                const int id = (int)TaskDialogCommonButtonId.MinCustomControlId;
                taskDialog.RadioButtons.Add(new TaskDialogRadioButton(id + 0, "radio1", "Radio Button1"));
                taskDialog.RadioButtons.Add(new TaskDialogRadioButton(id + 1, "radio2", "Radio Button2"));
                taskDialog.RadioButtons.Add(new TaskDialogRadioButton(id + 2, "radio3", "Radio Button3"));
                taskDialog.RadioButtons.Add(new TaskDialogRadioButton(id + 3, "radio4", "Radio Button4"));
                taskDialog.RadioButtons.Add(new TaskDialogRadioButton(id + 4, "radio5", "Radio Button5"));

                var result = taskDialog.Show();

                Debug.WriteLine("SelectedButtonId = " + result.SelectedButtonId);
                Debug.WriteLine("SelectedRadioButtonId = " + result.SelectedRadioButtonId);
                Debug.WriteLine("VerificationChecked = " + result.VerificationChecked);
            }
        }
    }
}