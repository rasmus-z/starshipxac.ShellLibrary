using System;

namespace TaskDialogSample.Manipulations
{
    public interface IMainWindowManipulator
    {
        void ShowSimpleTaskDialog();

        void ShowAllPartsTaskDialog();

        void ShowCustomButtonTaskDialog();

        void ShowCommandLinkTaskDialog();

        void ShowRadioButtonTaskDialog();
    }
}