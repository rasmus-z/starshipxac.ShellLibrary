using System;

namespace TaskDialogSample.Manipulations
{
    public interface IMainWindowManipulator
    {
        void ShowSimpleTaskDialog();

        void ShowAllControlsTaskDialog();

        void ShowCustomButtonTaskDialog();

        void ShowCommandLinkTaskDialog();

        void ShowRadioButtonTaskDialog();

        void ShowMarqueeTaskDialog();
    }
}