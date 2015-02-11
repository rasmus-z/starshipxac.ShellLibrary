using System;

namespace starshipxac.Windows.Shell.Media.Imaging.Interop
{
	/// <summary>
	/// �W���A�C�R����ID���`���܂��B
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762542(v=vs.85).aspx
	/// <c>ShellAPI.h</c>
	/// </remarks>
	internal enum SHSTOCKICONID
	{
		/// <summary>
		/// �A�v���P�[�V�����Ɋ֘A�Â����Ă��Ȃ��t�@�C���B
		/// </summary>
		SIID_DOCNOASSOC = 0,

		/// <summary>
		/// �A�v���P�[�V�����Ɋ֘A�Â����Ă���t�@�C���B
		/// </summary>
		SIID_DOCASSOC = 1,

		/// <summary>
		/// ��ʓI�ȃA�v���P�[�V�����̃A�C�R���B
		/// </summary>
		SIID_APPLICATION = 2,

		/// <summary>
		/// ���Ă���t�H���_�[�B
		/// </summary>
		SIID_FOLDER = 3,

		/// <summary>
		/// �J���Ă���t�H���_�[�B
		/// </summary>
		SIID_FOLDEROPEN = 4,

		/// <summary>
		/// 5.25�C���`�t���b�s�[�f�B�X�N�h���C�u�B
		/// </summary>
		SIID_DRIVE525 = 5,

		/// <summary>
		/// 3.5�C���`�t���b�s�[�f�B�X�N�h���C�u�B
		/// </summary>
		SIID_DRIVE35 = 6,

		/// <summary>
		/// �����[�o�u���h���C�u�B
		/// </summary>
		SIID_DRIVEREMOVE = 7,

		/// <summary>
		/// �Œ�h���C�u�B
		/// </summary>
		SIID_DRIVEFIXED = 8,

		/// <summary>
		/// �l�b�g���[�N�h���C�u�B
		/// </summary>
		SIID_DRIVENET = 9,

		/// <summary>
		/// �ؒf���ꂽ�l�b�g���[�N�h���C�u�B
		/// </summary>
		SIID_DRIVENETDISABLED = 10,

		/// <summary>
		/// CD�h���C�u�B
		/// </summary>
		SIID_DRIVECD = 11,

		/// <summary>
		/// RAM�h���C�u�B
		/// </summary>
		SIID_DRIVERAM = 12,

		/// <summary>
		/// �l�b�g���[�N�B
		/// </summary>
		SIID_WORLD = 13,

		/// <summary>
		/// �l�b�g���[�N��̃R���s���[�^�[�B
		/// </summary>
		SIID_SERVER = 15,

		/// <summary>
		/// �v�����^�[�B
		/// </summary>
		SIID_PRINTER = 16,

		/// <summary>
		/// �l�b�g���[�N�t�H���_�[�B
		/// </summary>
		SIID_MYNETWORK = 17,

		/// <summary>
		/// �����B
		/// </summary>
		SIID_FIND = 22,

		/// <summary>
		/// �w���v�B
		/// </summary>
		SIID_HELP = 23,

		/// <summary>
		/// ���L�B
		/// </summary>
		SIID_SHARE = 28,

		/// <summary>
		/// �V���[�g�J�b�g�B
		/// </summary>
		SIID_LINK = 29,

		/// <summary>
		/// Slow Item.
		/// </summary>
		SIID_SLOWFILE = 30,

		/// <summary>
		/// ��̂��ݔ��B
		/// </summary>
		SIID_RECYCLER = 31,

		/// <summary>
		/// ���ݔ��B
		/// </summary>
		SIID_RECYCLERFULL = 32,

		/// <summary>
		/// �I�[�f�B�ICD�B
		/// </summary>
		SIID_MEDIACDAUDIO = 40,

		/// <summary>
		/// �Z�L�����e�B�[���b�N�B
		/// </summary>
		SIID_LOCK = 47,

		/// <summary>
		/// Auto List.
		/// </summary>
		SIID_AUTOLIST = 49,

		/// <summary>
		/// �l�b�g���[�N�v�����^�[�B
		/// </summary>
		SIID_PRINTERNET = 50,

		/// <summary>
		/// ���L�l�b�g���[�N�t�H���_�[�B
		/// </summary>
		SIID_SERVERSHARE = 51,

		/// <summary>
		/// �t�@�b�N�X�B
		/// </summary>
		SIID_PRINTERFAX = 52,

		/// <summary>
		/// �l�b�g���[�N�t�@�b�N�X�B
		/// </summary>
		SIID_PRINTERFAXNET = 53,

		/// <summary>
		/// ����t�@�C���B
		/// </summary>
		SIID_PRINTERFILE = 54,

		/// <summary>
		/// �X�^�b�N�B
		/// </summary>
		SIID_STACK = 55,

		/// <summary>
		/// SVCD���f�B�A�B
		/// </summary>
		SIID_MEDIASVCD = 56,

		/// <summary>
		/// ���̃A�C�e�����܂ރt�H���_�B
		/// </summary>
		SIID_STUFFEDFOLDER = 57,

		/// <summary>
		/// �s���ȃh���C�u�B
		/// </summary>
		SIID_DRIVEUNKNOWN = 58,

		/// <summary>
		/// DVD�h���C�u�B
		/// </summary>
		SIID_DRIVEDVD = 59,

		/// <summary>
		/// DVD���f�B�A�B
		/// </summary>
		SIID_MEDIADVD = 60,

		/// <summary>
		/// DVD-RAM���f�B�A�B
		/// </summary>
		SIID_MEDIADVDRAM = 61,

		/// <summary>
		/// DVD-RW���f�B�A�B
		/// </summary>
		SIID_MEDIADVDRW = 62,

		/// <summary>
		/// DVD-R���f�B�A�B
		/// </summary>
		SIID_MEDIADVDR = 63,

		/// <summary>
		/// DVD-ROM���f�B�A�B
		/// </summary>
		SIID_MEDIADVDROM = 64,

		/// <summary>
		/// CD+(Enhanced CD)���f�B�A�B
		/// </summary>
		SIID_MEDIACDAUDIOPLUS = 65,

		/// <summary>
		/// CD-RW���f�B�A�B
		/// </summary>
		SIID_MEDIACDRW = 66,

		/// <summary>
		/// CD-R���f�B�A�B
		/// </summary>
		SIID_MEDIACDR = 67,

		/// <summary>
		/// ��������CD���f�B�A�B
		/// </summary>
		SIID_MEDIACDBURN = 68,

		/// <summary>
		/// �u�����NCD���f�B�A�B
		/// </summary>
		SIID_MEDIABLANKCD = 69,

		/// <summary>
		/// CD-ROM���f�B�A�B
		/// </summary>
		SIID_MEDIACDROM = 70,

		/// <summary>
		/// �I�[�f�B�I�t�@�C���B
		/// </summary>
		SIID_AUDIOFILES = 71,

		/// <summary>
		/// �摜�t�@�C���B
		/// </summary>
		SIID_IMAGEFILES = 72,

		/// <summary>
		/// �r�f�I�t�@�C���B
		/// </summary>
		SIID_VIDEOFILES = 73,

		/// <summary>
		/// �����t�@�C���B
		/// </summary>
		SIID_MIXEDFILES = 74,

		/// <summary>
		/// Folder Back.
		/// </summary>
		SIID_FOLDERBACK = 75,

		/// <summary>
		/// Folder Front.
		/// </summary>
		SIID_FOLDERFRONT = 76,

		/// <summary>
		/// UAC�Ŏg�p���鏂�A�C�R���B
		/// </summary>
		SIID_SHIELD = 77,

		/// <summary>
		/// �x���A�C�R���B
		/// </summary>
		SIID_WARNING = 78,

		/// <summary>
		/// ���A�C�R���B
		/// </summary>
		SIID_INFO = 79,

		/// <summary>
		/// �G���[�A�C�R���B
		/// </summary>
		SIID_ERROR = 80,

		/// <summary>
		/// ���A�C�R���B
		/// </summary>
		SIID_KEY = 81,

		/// <summary>
		/// �\�t�g�E�F�A�A�C�R���B
		/// </summary>
		SIID_SOFTWARE = 82,

		/// <summary>
		/// ���O�ύX�A�C�R���B
		/// </summary>
		SIID_RENAME = 83,

		/// <summary>
		/// �폜�A�C�R���B
		/// </summary>
		SIID_DELETE = 84,

		/// <summary>
		/// �I�[�f�B�IDVD���f�B�A�B
		/// </summary>
		SIID_MEDIAAUDIODVD = 85,

		/// <summary>
		/// �f��DVD���f�B�A�B
		/// </summary>
		SIID_MEDIAMOVIEDVD = 86,

		/// <summary>
		/// �G���n���X�hCD���f�B�A�B
		/// </summary>
		SIID_MEDIAENHANCEDCD = 87,

		/// <summary>
		/// �G���n���X�hDVD���f�B�A�B
		/// </summary>
		SIID_MEDIAENHANCEDDVD = 88,

		/// <summary>
		/// HD-DVD���f�B�A�B
		/// </summary>
		SIID_MEDIAHDDVD = 89,

		/// <summary>
		/// BluRay���f�B�A�B
		/// </summary>
		SIID_MEDIABLURAY = 90,

		/// <summary>
		/// VCD���f�B�A�B
		/// </summary>
		SIID_MEDIAVCD = 91,

		/// <summary>
		/// DVD+R���f�B�A�B
		/// </summary>
		SIID_MEDIADVDPLUSR = 92,

		/// <summary>
		/// DVD+RW���f�B�A�B
		/// </summary>
		SIID_MEDIADVDPLUSRW = 93,

		/// <summary>
		/// �f�X�N�g�b�v�R���s���[�^�[�B
		/// </summary>
		SIID_DESKTOPPC = 94,

		/// <summary>
		/// ���o�C���R���s���[�^�[�B
		/// </summary>
		SIID_MOBILEPC = 95,

		/// <summary>
		/// ���[�U�[�A�J�E���g�R���g���[���p�l���B
		/// </summary>
		SIID_USERS = 96,

		/// <summary>
		/// �X�}�[�g���f�B�A�B
		/// </summary>
		SIID_MEDIASMARTMEDIA = 97,

		/// <summary>
		/// �R���p�N�g�t���b�V�����f�B�A�B
		/// </summary>
		SIID_MEDIACOMPACTFLASH = 98,

		/// <summary>
		/// �g�ѓd�b�B
		/// </summary>
		SIID_DEVICECELLPHONE = 99,

		/// <summary>
		/// �J�����B
		/// </summary>
		SIID_DEVICECAMERA = 100,

		/// <summary>
		/// �r�f�I�J�����B
		/// </summary>
		SIID_DEVICEVIDEOCAMERA = 101,

		/// <summary>
		/// �I�[�f�B�I�v���[���[�B
		/// </summary>
		SIID_DEVICEAUDIOPLAYER = 102,

		/// <summary>
		/// �l�b�g���[�N�ڑ��B
		/// </summary>
		SIID_NETWORKCONNECT = 103,

		/// <summary>
		/// �l�b�g���[�N�ƃC���^�[�l�b�g�R���g���[���p�l���B
		/// </summary>
		SIID_INTERNET = 104,

		/// <summary>
		/// ZIP�t�@�C���B
		/// </summary>
		SIID_ZIPFILE = 105,

		/// <summary>
		/// �ݒ�B
		/// </summary>
		SIID_SETTINGS = 106,

		/// <summary>
		/// HDDVD�h���C�u�B
		/// </summary>
		SIID_DRIVEHDDVD = 132,

		/// <summary>
		/// BluRay�h���C�u�B
		/// </summary>
		SIID_DRIVEBD = 133,

		/// <summary>
		/// HDDVD-ROM���f�B�A�B
		/// </summary>
		SIID_MEDIAHDDVDROM = 134,

		/// <summary>
		/// HDDVD-R���f�B�A�B
		/// </summary>
		SIID_MEDIAHDDVDR = 135,

		/// <summary>
		/// HDDVD-RAM���f�B�A�B
		/// </summary>
		SIID_MEDIAHDDVDRAM = 136,

		/// <summary>
		/// BluRay ROM���f�B�A�B
		/// </summary>
		SIID_MEDIABDROM = 137,

		/// <summary>
		/// BluRay R���f�B�A�B
		/// </summary>
		SIID_MEDIABDR = 138,

		/// <summary>
		/// BluRay RE���f�B�A�B
		/// </summary>
		SIID_MEDIABDRE = 139,

		/// <summary>
		/// �N���X�^�[�����ꂽ�f�B�X�N�B
		/// </summary>
		SIID_CLUSTEREDDRIVE = 140,

		/// <summary>
		/// �񋓒l�̍ő�l�B
		/// </summary>
		SIID_MAX_ICONS = 174,
	}
}