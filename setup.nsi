######################################################################

!define APP_NAME "AudioSharp"
!define COMP_NAME "Stefan 'Heufneutje' Frijters"
!define WEB_SITE "https://github.com/Heufneutje/AudioSharp"
!define VERSION "1.6.0.0"
!define DESCRIPTION "AudioSharp"
!define LICENSE_TXT "LICENSE.txt"
!define INSTALLER_NAME "Release\AudioSharp Setup.exe"
!define MAIN_APP_EXE "AudioSharp.exe"
!define INSTALL_TYPE "SetShellVarContext current"
!define REG_ROOT "HKCU"
!define REG_APP_PATH "Software\Microsoft\Windows\CurrentVersion\App Paths\${MAIN_APP_EXE}"
!define UNINSTALL_PATH "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}"
!define MUI_FINISHPAGE_RUN
!define MUI_FINISHPAGE_RUN_TEXT "Run AudioSharp"
!define MUI_FINISHPAGE_RUN_FUNCTION "LaunchLink"

!define REG_START_MENU "Start Menu Folder"

var SM_Folder

######################################################################

VIProductVersion  "${VERSION}"
VIAddVersionKey "ProductName"  "${APP_NAME}"
VIAddVersionKey "CompanyName"  "${COMP_NAME}"
VIAddVersionKey "FileDescription"  "${DESCRIPTION}"
VIAddVersionKey "FileVersion"  "${VERSION}"

######################################################################

SetCompressor ZLIB
Name "${APP_NAME}"
Caption "${APP_NAME}"
OutFile "${INSTALLER_NAME}"
BrandingText "${APP_NAME}"
XPStyle on
InstallDirRegKey "${REG_ROOT}" "${REG_APP_PATH}" ""

######################################################################

!include "MUI.nsh"
!include x64.nsh

InstallDir "$PROGRAMFILES\AudioSharp"
function .onInit
    ${If} ${RunningX64}
        SetRegView 64
        StrCpy $INSTDIR "$PROGRAMFILES64\AudioSharp"
    ${EndIf}
functionEnd
function LaunchLink
	ExecShell "" "$INSTDIR\AudioSharp.exe"
functionEnd

!define MUI_ABORTWARNING
!define MUI_UNABORTWARNING

!insertmacro MUI_PAGE_WELCOME

!ifdef LICENSE_TXT
!insertmacro MUI_PAGE_LICENSE "${LICENSE_TXT}"
!endif

!insertmacro MUI_PAGE_DIRECTORY

!ifdef REG_START_MENU
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "AudioSharp"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${REG_ROOT}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${UNINSTALL_PATH}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${REG_START_MENU}"
!insertmacro MUI_PAGE_STARTMENU Application $SM_Folder
!endif

!insertmacro MUI_PAGE_INSTFILES

!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM

!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_UNPAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

######################################################################

Section -MainProgram
${INSTALL_TYPE}
SetOverwrite ifnewer
SetOutPath "$INSTDIR"
File "Release\AudioSharp.Config.dll"
File "Release\AudioSharp.Core.dll"
File "Release\AudioSharp.exe"
File "Release\AudioSharp.exe.config"
File "Release\AudioSharp.GUI.CustomControls.dll"
File "Release\AudioSharp.Translations.dll"
File "Release\AudioSharp.Utils.dll"
File "Release\GlobalHotKey.dll"
File "Release\Hardcodet.Wpf.TaskbarNotification.dll"
File "Release\libmp3lame.32.dll"
File "Release\libmp3lame.64.dll"
File "Release\License.txt"
File "Release\NAudio.dll"
File "Release\NAudio.Lame.dll"
File "Release\Newtonsoft.Json.dll"
File "Release\Xceed.Wpf.AvalonDock.dll"
File "Release\Xceed.Wpf.AvalonDock.Themes.Aero.dll"
File "Release\Xceed.Wpf.AvalonDock.Themes.Metro.dll"
File "Release\Xceed.Wpf.AvalonDock.Themes.VS2010.dll"
File "Release\Xceed.Wpf.DataGrid.dll"
File "Release\Xceed.Wpf.Toolkit.dll"
SectionEnd

######################################################################

Section -Icons_Reg
SetOutPath "$INSTDIR"
WriteUninstaller "$INSTDIR\uninstall.exe"

!ifdef REG_START_MENU
!insertmacro MUI_STARTMENU_WRITE_BEGIN Application
CreateDirectory "$SMPROGRAMS\$SM_Folder"
CreateShortCut "$SMPROGRAMS\$SM_Folder\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
CreateShortCut "$DESKTOP\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
CreateShortCut "$SMPROGRAMS\$SM_Folder\Uninstall ${APP_NAME}.lnk" "$INSTDIR\uninstall.exe"

!ifdef WEB_SITE
WriteIniStr "$INSTDIR\${APP_NAME} website.url" "InternetShortcut" "URL" "${WEB_SITE}"
CreateShortCut "$SMPROGRAMS\$SM_Folder\${APP_NAME} Website.lnk" "$INSTDIR\${APP_NAME} website.url"
!endif
!insertmacro MUI_STARTMENU_WRITE_END
!endif

!ifndef REG_START_MENU
CreateDirectory "$SMPROGRAMS\AudioSharp"
CreateShortCut "$SMPROGRAMS\AudioSharp\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
CreateShortCut "$DESKTOP\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
CreateShortCut "$SMPROGRAMS\AudioSharp\Uninstall ${APP_NAME}.lnk" "$INSTDIR\uninstall.exe"

!ifdef WEB_SITE
WriteIniStr "$INSTDIR\${APP_NAME} website.url" "InternetShortcut" "URL" "${WEB_SITE}"
CreateShortCut "$SMPROGRAMS\AudioSharp\${APP_NAME} Website.lnk" "$INSTDIR\${APP_NAME} website.url"
!endif
!endif

WriteRegStr ${REG_ROOT} "${REG_APP_PATH}" "" "$INSTDIR\${MAIN_APP_EXE}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayName" "${APP_NAME}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "UninstallString" "$INSTDIR\uninstall.exe"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayIcon" "$INSTDIR\${MAIN_APP_EXE}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayVersion" "${VERSION}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "Publisher" "${COMP_NAME}"

!ifdef WEB_SITE
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "URLInfoAbout" "${WEB_SITE}"
!endif
SectionEnd

######################################################################

Section Uninstall
${INSTALL_TYPE}
Delete "$INSTDIR\AudioSharp.Config.dll"
Delete "$INSTDIR\AudioSharp.Core.dll"
Delete "$INSTDIR\AudioSharp.exe"
Delete "$INSTDIR\AudioSharp.exe.config"
Delete "$INSTDIR\AudioSharp.GUI.CustomControls.dll"
Delete "$INSTDIR\AudioSharp.Translations.dll"
Delete "$INSTDIR\AudioSharp.Utils.dll"
Delete "$INSTDIR\GlobalHotKey.dll"
Delete "$INSTDIR\Hardcodet.Wpf.TaskbarNotification.dll"
Delete "$INSTDIR\libmp3lame.32.dll"
Delete "$INSTDIR\libmp3lame.64.dll"
Delete "$INSTDIR\License.txt"
Delete "$INSTDIR\NAudio.dll"
Delete "$INSTDIR\NAudio.Lame.dll"
Delete "$INSTDIR\Newtonsoft.Json.dll"
Delete "$INSTDIR\Xceed.Wpf.AvalonDock.dll"
Delete "$INSTDIR\Xceed.Wpf.AvalonDock.Themes.Aero.dll"
Delete "$INSTDIR\Xceed.Wpf.AvalonDock.Themes.Metro.dll"
Delete "$INSTDIR\Xceed.Wpf.AvalonDock.Themes.VS2010.dll"
Delete "$INSTDIR\Xceed.Wpf.DataGrid.dll"
Delete "$INSTDIR\Xceed.Wpf.Toolkit.dll"
Delete "$INSTDIR\uninstall.exe"
!ifdef WEB_SITE
Delete "$INSTDIR\${APP_NAME} website.url"
!endif

RmDir "$INSTDIR"

!ifdef REG_START_MENU
!insertmacro MUI_STARTMENU_GETFOLDER "Application" $SM_Folder
Delete "$SMPROGRAMS\$SM_Folder\${APP_NAME}.lnk"
Delete "$SMPROGRAMS\$SM_Folder\Uninstall ${APP_NAME}.lnk"
!ifdef WEB_SITE
Delete "$SMPROGRAMS\$SM_Folder\${APP_NAME} Website.lnk"
!endif
Delete "$DESKTOP\${APP_NAME}.lnk"

RmDir "$SMPROGRAMS\$SM_Folder"
!endif

!ifndef REG_START_MENU
Delete "$SMPROGRAMS\AudioSharp\${APP_NAME}.lnk"
Delete "$SMPROGRAMS\AudioSharp\Uninstall ${APP_NAME}.lnk"
!ifdef WEB_SITE
Delete "$SMPROGRAMS\AudioSharp\${APP_NAME} Website.lnk"
!endif
Delete "$DESKTOP\${APP_NAME}.lnk"
Delete "$SMSTARTUP\${APP_NAME}.lnk"

RmDir "$SMPROGRAMS\AudioSharp"
!endif

DeleteRegKey ${REG_ROOT} "${REG_APP_PATH}"
DeleteRegKey ${REG_ROOT} "${UNINSTALL_PATH}"
SectionEnd

######################################################################

