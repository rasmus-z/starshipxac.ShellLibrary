using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using starshipxac.Shell.PropertySystem.Internal;

namespace starshipxac.Shell.PropertySystem
{
    public class ShellProperties : IEnumerable<IShellProperty>
    {
        private List<IShellProperty> properties;

        internal ShellProperties(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            this.ShellObject = shellObject;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ShellObject != null);
        }

        private ShellObject ShellObject { get; }

        public IEnumerator<IShellProperty> GetEnumerator()
        {
            if (this.properties == null)
            {
                using (var propertyStore = ShellPropertyStore.Create(this.ShellObject))
                {
                    var propertyCount = propertyStore.Count;
                    this.properties = new List<IShellProperty>(propertyCount);
                    for (var index = 0u; index < propertyCount; ++index)
                    {
                        var propertyKey = propertyStore.GetAt(index);
                        try
                        {
                            this.properties.Add(ShellPropertyFactory.Create(propertyKey, this.ShellObject));
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                    }
                }
            }

            return this.properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}