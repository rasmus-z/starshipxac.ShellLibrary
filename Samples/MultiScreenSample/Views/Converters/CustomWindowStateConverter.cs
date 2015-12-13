using System;
using System.Reactive.Linq;
using Reactive.Bindings.Interactivity;

namespace MultiScreenSample.Views.Converters
{
    public class CustomWindowStateConverter : ReactiveConverter<CustomWindowStateEventArgs, CustomWindowStates>
    {
        protected override IObservable<CustomWindowStates> OnConvert(IObservable<CustomWindowStateEventArgs> source)
        {
            return source.Select(x => x.NewWindowState);
        }
    }
}