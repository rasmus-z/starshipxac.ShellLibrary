using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell.Resources
{
    /// <summary>
    /// ���\�[�X�Q�Ə���ێ����܂��B
    /// </summary>
    [ContractClass(typeof(ResourceReferenceContract))]
    public abstract class ResourceReference : IEquatable<ResourceReference>
    {
        /// <summary>
        /// ���C�u�������ƃ��\�[�XID���w�肵�āA<see cref="ResourceReference"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="libraryName">���s�t�@�C���܂��� DLL�t�@�C���A�A�C�R���t�@�C���̃��C�u�������B</param>
        /// <param name="resourceId">�A�C�R���̃C���f�b�N�X�B</param>
        protected ResourceReference(string libraryName, int resourceId)
        {
            this.LibraryPath = libraryName;
            this.ResourceId = resourceId;
            this.ReferencePath = GetReferencePathInternal();
        }

        /// <summary>
        /// �J���}�ŋ�؂�ꂽ���C�u�������ƃ��\�[�XID���w�肵�āA<see cref="ResourceReference"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="referencePath">�J���}�ŋ�؂�ꂽ���C�u�������ƃ��\�[�XID�B</param>
        protected ResourceReference(string referencePath)
        {
            this.ReferencePath = referencePath;
            ParseReferencePathInternal();
        }

        /// <summary>
        /// ���s�t�@�C���܂��� DLL�t�@�C���̃p�X�����擾���܂��B
        /// </summary>
        public string LibraryPath { get; private set; }

        /// <summary>
        /// ���\�[�XID���擾���܂��B
        /// </summary>
        public int ResourceId { get; private set; }

        /// <summary>
        /// ���\�[�X�Q�Ə����擾���܂��B
        /// </summary>
        /// <remarks>
        /// ���\�[�X�Q�Ə��́A���C�u�������ƃ��\�[�XID���J���}�Ō�������������ł��B
        /// </remarks>
        public string ReferencePath { get; private set; }

        private string GetReferencePathInternal()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return GetReferencePath();
        }

        private void ParseReferencePathInternal()
        {
            string libraryName;
            int resourceId;
            ParseReferencePath(out libraryName, out resourceId);
            this.LibraryPath = libraryName;
            this.ResourceId = resourceId;
        }

        protected abstract string GetReferencePath();

        protected abstract void ParseReferencePath(out string libraryPath, out int resourceId);

        /// <summary>
        /// 2��<see cref="ResourceReference"/>���r���āA���������ǂ����𔻒肵�܂��B
        /// </summary>
        /// <param name="left">1�߂�<see cref="ResourceReference"/>�B</param>
        /// <param name="right">2�߂�<see cref="ResourceReference"/>�B</param>
        /// <returns>
        /// 2��<see cref="ResourceReference"/>���������ꍇ��<c>true</c>�B����ȊO�̏ꍇ��<c>false</c>�B
        /// </returns>
        public static bool operator ==(ResourceReference left, ResourceReference right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// 2��<see cref="ResourceReference"/>���r���āA�������Ȃ����ǂ����𔻒肵�܂��B
        /// </summary>
        /// <param name="left">1�߂�<see cref="ResourceReference"/>�B</param>
        /// <param name="right">2�߂�<see cref="ResourceReference"/>�B</param>
        /// <returns>
        /// 2��<see cref="ResourceReference"/>���������Ȃ��ꍇ��<c>true</c>�B����ȊO�̏ꍇ��<c>false</c>�B
        /// </returns>
        public static bool operator !=(ResourceReference left, ResourceReference right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// �w�肵��<see cref="ResourceReference"/>�̒l���A���݂�<see cref="ResourceReference"/>�Ɠ��������ǂ����𔻒肵�܂��B
        /// </summary>
        /// <param name="other">���݂�<see cref="ResourceReference"/>�Ɣ�r����<see cref="ResourceReference"/>�B</param>
        /// <returns>
        /// <paramref name="other"/>�ƌ��݂�<see cref="ResourceReference"/>���������ꍇ��<c>true</c>�B
        /// ����ȊO�̏ꍇ��<c>false</c>�B
        /// </returns>
        public bool Equals(ResourceReference other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.LibraryPath.Equals(other.LibraryPath, StringComparison.InvariantCultureIgnoreCase) &&
                   this.ResourceId.Equals(other.ResourceId);
        }

        /// <summary>
        /// �w�肵���I�u�W�F�N�g�̒l���A���݂�<see cref="ResourceReference"/>�Ɠ��������ǂ����𔻒肵�܂��B
        /// </summary>
        /// <param name="obj">���݂�<see cref="ResourceReference"/>�Ɣ�r����I�u�W�F�N�g�B</param>
        /// <returns>
        /// <paramref name="obj"/>�ƌ��݂�<see cref="ResourceReference"/>���������ꍇ��<c>true</c>�B
        /// ����ȊO�̏ꍇ��<c>false</c>�B
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ResourceReference);
        }

        /// <summary>
        /// ���̃C���X�^���X�̃n�b�V���R�[�h���擾���܂��B
        /// </summary>
        /// <returns>�n�b�V���R�[�h�B</returns>
        public override int GetHashCode()
        {
            return this.ReferencePath.GetHashCode();
        }

        /// <summary>
        /// ���̃C���X�^���X�̕�����\�����擾���܂��B
        /// </summary>
        /// <returns>���̃C���X�^���X�̕�����\���B</returns>
        public override string ToString()
        {
            return this.ReferencePath;
        }
    }

    [ContractClassFor(typeof(ResourceReference))]
    abstract class ResourceReferenceContract : ResourceReference
    {
        protected ResourceReferenceContract(string libraryName, int resourceId)
            : base(libraryName, resourceId)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
        }

        protected ResourceReferenceContract(string referencePath)
            : base(referencePath)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(referencePath));
        }

        [ContractInvariantMethod]
        private void ObjectInvarinat()
        {
            Contract.Invariant(this.LibraryPath != null);
            Contract.Invariant(this.ReferencePath != null);
        }

        protected override string GetReferencePath()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return default(string);
        }

        protected override void ParseReferencePath(out string libraryPath, out int resourceId)
        {
            Contract.Ensures(Contract.ValueAtReturn(out libraryPath) != null);
            libraryPath = String.Empty;
            resourceId = 0;
        }
    }
}