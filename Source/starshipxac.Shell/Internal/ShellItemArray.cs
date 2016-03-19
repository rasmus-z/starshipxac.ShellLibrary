using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Interop;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.Internal
{
    internal class ShellItemArray : IShellItemArray
    {
        private readonly List<IShellItem> shellItemList = new List<IShellItem>();

        internal ShellItemArray(IShellItem[] shellItems)
        {
            Contract.Requires<ArgumentNullException>(shellItems != null);

            this.shellItemList.AddRange(shellItems);
        }

        internal ShellItemArray(IEnumerable<IShellItem> shellItems)
        {
            if (shellItems != null)
            {
                this.shellItemList.AddRange(shellItems);
            }
        }

        #region IShellItemArrayメンバー

        public HRESULT BindToHandler(IntPtr pdc, ref Guid rbhid, ref Guid riid, out IntPtr ppvOut)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetPropertyStore(int Flags, ref Guid riid, out IntPtr ppvOut)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetPropertyDescriptionList(ref PROPERTYKEY keyType, ref Guid riid, out IntPtr ppv)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetAttributes(SIATTRIBFLAGS dwAttributeFlags, UInt32 sfgaoMask, out UInt32 psfgaoAttributes)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetCount(out uint pdwNumItems)
        {
            pdwNumItems = (uint)this.shellItemList.Count;
            return COMErrorCodes.S_OK;
        }

        public HRESULT GetItemAt(uint dwIndex, out IShellItem ppsi)
        {
            var index = (int)dwIndex;
            if (index < this.shellItemList.Count)
            {
                ppsi = this.shellItemList[index];
                return COMErrorCodes.S_OK;
            }

            ppsi = null;
            return COMErrorCodes.E_FAIL;
        }

        public HRESULT EnumItems(out IntPtr ppenumShellItems)
        {
            throw new NotImplementedException();
        }

        #endregion iShellItemArrayメンバー

        /// <summary>
        ///     <see cref="IShellItemArray" />から、<see cref="index" />で指定した位置の<see cref="IShellItem" />を取得します。
        /// </summary>
        /// <param name="shellItemArray"><see cref="IShellItemArray" />。</param>
        /// <param name="index">取得する位置のインデックス。</param>
        /// <returns>取得した<see cref="IShellItem" />。</returns>
        internal static IShellItem GetShellItemAt(IShellItemArray shellItemArray, int index)
        {
            Contract.Requires<ArgumentNullException>(shellItemArray != null);

            IShellItem result;
            shellItemArray.GetItemAt((uint)index, out result);
            return result;
        }

        /// <summary>
        ///     <see cref="IShellItemArray" />内のアイテム数を取得します。
        /// </summary>
        /// <param name="shellItemArray"><see cref="IShellItemArray" />。</param>
        /// <returns>取得したアイテム数。</returns>
        internal static int GetShellItemCount(IShellItemArray shellItemArray)
        {
            Contract.Requires<ArgumentNullException>(shellItemArray != null);

            uint result;
            shellItemArray.GetCount(out result);
            return (int)result;
        }
    }
}