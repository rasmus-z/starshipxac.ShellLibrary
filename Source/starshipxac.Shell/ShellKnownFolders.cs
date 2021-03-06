﻿using System;
using System.Collections.Generic;
using static starshipxac.Shell.ShellKnownFolderFactory;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Get the known folder you use frequently.
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/ja-jp/library/windows/desktop/dd378457(v=vs.85).aspx
    /// </remarks>
    public static class ShellKnownFolders
    {
        /// <summary>
        ///     Roaming user's application specific data.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{3eb685db-65f9-4cf6-a03a-e3ef65729f3d}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>AppData</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\AppData\Roaming</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder ApplicationData => FromCanonicalName("AppData");

        /// <summary>
        ///     Computer.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{0ac0837c-bbf8-452a-850d-79d08e667ca7}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>MyComputerFolder</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Virtual</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>::{20D04FE0-3AEA-1069-A2D8-08002B30309D}</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Computer => FromCanonicalName("MyComputerFolder");

        /// <summary>
        ///     Desktop.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{b4bfcc3a-db2c-424c-b029-7fe99a87c641}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Desktop</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Desktop</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Desktop => FromCanonicalName("Desktop");

        public static ShellKnownFolder Documents => Personal;

        /// <summary>
        ///     Download.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{374de290-123f-4565-9164-39c4925e467b}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Downloads</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Downloads</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Downloads => FromCanonicalName("Downloads");

        /// <summary>
        ///     Favorites.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{1777f761-68ad-4d8a-87bd-30b759fa33dd}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Favorites</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Favorites</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Favorites => FromCanonicalName("Favorites");

        /// <summary>
        ///     Games.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{cac52c1a-b53d-4edc-92d7-6b2e8ac19434}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Games</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Virtual</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>::{ED228FDF-9EA8-4870-83B1-96B02CFE0D52}</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Games => FromCanonicalName("Games");

        /// <summary>
        ///     GameExplorer
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{054fae61-4dd8-4787-80b6-090220c4b700}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>GameTasks</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%LOCALAPPDATA%\Microsoft\Windows\GameExplorer</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder GameTasks => FromCanonicalName("GameTasks");

        /// <summary>
        ///     HomeGroup
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{52528a6b-b9e3-4add-b60d-588c2dba842d}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>HomeGroupFolder</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Virtual</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>::{B4FB3F98-C1EA-428D-A78A-D1F5659CBA93}</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder HomeGroup => FromCanonicalName("HomeGroupFolder");

        /// <summary>
        ///     Current user's home group
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{9b74b6a3-0dfd-4f11-9e78-5f7800f2e772}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>HomeGroupCurrentUserFolder</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Virtual</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>::{B4FB3F98-C1EA-428D-A78A-D1F5659CBA93}\(Account ID)</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder HomeGroupCurrentUser => FromCanonicalName("HomeGroupCurrentUserFolder");

        /// <summary>
        ///     Library
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{1b3ea5dc-b587-4786-b4ef-bd1dc332aeae}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Libraries</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>::{031E4825-7B94-4DC3-B131-E946B44C8DD5}</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Libraries => FromCanonicalName("Libraries");

        /// <summary>
        ///     Links
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{bfb9d5e0-c6a9-404c-b2b2-ae6db6af4968}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Links</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Links</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Links => FromCanonicalName("Links");

        /// <summary>
        ///     Local application data.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{f1b32785-6fba-4fcf-9d55-7b8e7f157091}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Local AppData</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\AppData\Local</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder LocalApplicationData => FromCanonicalName("Local AppData");

        /// <summary>
        ///     Music
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{4bd8d571-6d19-48d3-be97-422220080e43}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>My Music</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Music</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Music => FromCanonicalName("My Music");

        /// <summary>
        ///     Network
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{d20beec4-5ca8-4905-ae3b-bf251ea09b53}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>NetworkPlacesFolder</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Virtual</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>::{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Network => FromCanonicalName("NetworkPlacesFolder");

        /// <summary>
        ///     NetHood
        /// </summary>
        public static ShellKnownFolder NetworkSortcuts => FromCanonicalName("NetHood");

        /// <summary>
        ///     OneDrive
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{a52bba46-e9e1-435f-b3d9-28daa648c0f6}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>OneDrive</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\SkyDrive</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder OneDrive => FromCanonicalName("OneDrive");

        /// <summary>
        ///     OneDrive camera roll.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{767e6811-49cb-4273-87c2-20f355e1085b}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>SkyDriveCameraRoll</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\SkyDrive\画像\カメラ ロール</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder OneDriveCameraRoll => FromCanonicalName("SkyDriveCameraRoll");

        /// <summary>
        ///     OneDrive Documents.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{24d89e24-2f19-4534-9dde-6a6671fbb8fe}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>SkyDriveDocuments</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\SkyDrive\ドキュメント</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder OneDriveDocuments => FromCanonicalName("SkyDriveDocuments");

        /// <summary>
        ///     OneDrive Pictures.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{339719b5-8c47-4894-94c2-d8f77add44a6}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>SkyDrivePictures</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\SkyDrive\Pictures</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder OneDrivePictures => FromCanonicalName("SkyDrivePictures");

        /// <summary>
        ///     Documents
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{fdd39ad0-238f-46af-adb4-6c85480369c7}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Personal</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Documents</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Personal => FromCanonicalName("Personal");

        /// <summary>
        ///     Pictures
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{33e28130-4e1e-4676-835a-98395c3bc3bb}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>My Pictures</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Pictures</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Pictures => FromCanonicalName("My Pictures");

        /// <summary>
        ///     Playlists
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{de92c1c7-837f-4f69-a3bb-86e631204a23}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Playlists</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Music\Playlists</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Playlists => FromCanonicalName("Playlists");

        /// <summary>
        ///     Printer folder.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{76fc4e2d-d6ad-4519-a663-37bd56068185}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>PrintersFolder</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Virtual</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term>
        ///             <description>::{21EC2020-3AEA-1069-A2DD-08002B30309D}\::{2227A280-3AEA-1069-A2DE-08002B30309D}</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Printers => FromCanonicalName("PrintersFolder");

        /// <summary>
        ///     User's folder.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{5e6c858f-0e22-4760-9afe-ea3317b67173}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Profile</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Fixed</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Profile => FromCanonicalName("Profile");

        /// <summary>
        ///     Common application data.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{62ab5d82-fdc1-4dc3-a9dd-070d1d495d97}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Common AppData</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Fixed</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%ALLUSERSPROFILE%</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder ProgramData => FromCanonicalName("Common AppData");

        /// <summary>
        ///     Programs
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{a77f5d77-2e2b-44c3-a6a2-aba601054a51}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Programs</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term>
        ///             <description>%USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Programs => FromCanonicalName("Programs");

        /// <summary>
        ///     Public
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{dfdf76a2-c82a-4d63-906a-5644ac457385}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Public</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Fixed</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%PUBLIC%</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Public => FromCanonicalName("Public");

        /// <summary>
        ///     Public desktop.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{c4aa340d-f20f-4863-afef-f87ef2e6ba25}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Common Desktop</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Common</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%PUBLIC%\Desktop</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder PublicDesktop => FromCanonicalName("Common Desktop");

        /// <summary>
        ///     Public documents.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{ed4824af-dce4-45a8-81e2-fc7965083634}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Common Documents</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Common</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%PUBLIC%\Documents</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder PublicDocuments => FromCanonicalName("Common Documents");

        /// <summary>
        ///     Public downloads.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{3d644c9b-1fb8-4f30-9b45-f670235f79c0}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>CommonDownloads</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Common</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%PUBLIC%\Downloads</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder PublicDownloads => FromCanonicalName("CommonDownloads");

        /// <summary>
        ///     GameExplorer
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{debf2536-e1a8-4c59-b6a2-414586476aea}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>PublicGameTasks</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Common</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%ALLUSERSPROFILE%\Microsoft\Windows\GameExplorer</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder PublicGameTasks => FromCanonicalName("PublicGameTasks");

        /// <summary>
        ///     Public libraries.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{48daf80b-e6cf-4f4e-b800-0e69d84ee384}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>PublicLibraries</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Common</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%PUBLIC%\Libraries</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder PublicLibraries => FromCanonicalName("PublicLibraries");

        /// <summary>
        ///     Public music.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{3214fab5-9757-4298-bb61-92a9deaa44ff}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>CommonMusic</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Common</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%PUBLIC%\Music</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder PublicMusic => FromCanonicalName("CommonMusic");

        /// <summary>
        ///     Public pictures.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{b6ebfb86-6907-413c-9af7-4fc2abf07cc5}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>CommonPictures</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Common</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%PUBLIC%\Pictures</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder PublicPictures => FromCanonicalName("CommonPictures");

        /// <summary>
        ///     Public video.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{2400183a-6185-49fb-a2d8-4a392a602ba3}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>CommonVideo</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Common</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%PUBLIC%\Videos</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder PublicVideos => FromCanonicalName("CommonVideo");

        /// <summary>
        ///     Recycle bin folder.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{b7534046-3ecb-4c18-be4e-64cd4cb7d6ac}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>RecycleBinFolder</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Virtual</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>::{645FF040-5081-101B-9F08-00AA002F954E}</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder RecycleBin => FromCanonicalName("RecycleBinFolder");

        /// <summary>
        ///     Saved games.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{4c5c32ff-bb9d-43b0-b5b4-2d72e54eaaa4}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>SavedGames</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Saved Games</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder SavedGames => FromCanonicalName("SavedGames");

        /// <summary>
        ///     Searches
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{7d1d3a04-debb-4115-95cf-2f29da2920da}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Searches</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Searches</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Searches => FromCanonicalName("Searches");

        /// <summary>
        ///     Search history folder.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{0d4c3db6-03a3-462f-a0e6-08924c41b5d4}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>SearchHistoryFolder</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term>
        ///             <description>%USERPROFILE%\AppData\Local\Microsoft\Windows\ConnectedSearch\History</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder SearchHistory => FromCanonicalName("SearchHistoryFolder");

        /// <summary>
        ///     Search home folder.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{190337d1-b8ca-4121-a639-6d472d16972a}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>SearchHomeFolder</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Virtual</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>::{9343812E-1C37-4A49-A12E-4B2D810D956B}</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder SearchHome => FromCanonicalName("SearchHomeFolder");

        /// <summary>
        ///     Send to.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{8983036c-27c0-404b-8f08-102d10dcfd74}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>SendTo</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\AppData\Roaming\Microsoft\Windows\SendTo</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder SendTo => FromCanonicalName("SendTo");

        /// <summary>
        ///     Startup
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{b97d20bb-f46a-4c97-ba10-5e3608430854}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>Startup</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term>
        ///             <description>%USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Startup => FromCanonicalName("Startup");

        /// <summary>
        ///     Users
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{0762d272-c50a-4bb0-a382-697dcd729b80}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>UserProfiles</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Fixed</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%SystemDrive%\Users</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder UserProfiles => FromCanonicalName("UserProfiles");

        /// <summary>
        ///     User's folder.
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{f3ce0f7c-4901-4acc-8648-d5d44b04ef8f}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>UsersFilesFolder</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>Virtual</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder UserProfile => FromCanonicalName("UsersFilesFolder");

        /// <summary>
        ///     Video
        /// </summary>
        /// <remarks>
        ///     <list type="table">
        ///         <item>
        ///             <term>GUID</term><description>{18989b1d-99b5-455b-841c-ab7c74e4ddfc}</description>
        ///         </item>
        ///         <item>
        ///             <term>Canonical Name</term><description>My Video</description>
        ///         </item>
        ///         <item>
        ///             <term>Folder Category</term><description>PerUser</description>
        ///         </item>
        ///         <item>
        ///             <term>Parsing Name</term><description>%USERPROFILE%\Videos</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static ShellKnownFolder Videos => FromCanonicalName("My Video");

        /// <summary>
        ///     Enumerate all known folders.
        /// </summary>
        public static IEnumerable<ShellKnownFolder> EnumerateKnownFolders()
        {
            return GetAllFolders();
        }
    }
}