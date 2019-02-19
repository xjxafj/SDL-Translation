﻿using Sdl.Core.Settings;
using Sdl.FileTypeSupport.Framework.NativeApi;
using Sdl.Core.Globalization;
using System.IO;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace SDL_Translation.code
{
    // the file sniffer component determines whether a given file
    // can be processed by the filter or not
    class SimpleTextSniffer : INativeFileSniffer
    {
        public SniffInfo Sniff(string nativeFilePath, Language language, Codepage suggestedCodepage, INativeTextLocationMessageReporter messageReporter, ISettingsGroup settingsGroup)
        {
            SniffInfo fileInfo = new SniffInfo();

            StreamReader _reader = new StreamReader(nativeFilePath);

            if (_reader.ReadLine().StartsWith("[Version="))
                fileInfo.IsSupported = true;
            else
            {
                fileInfo.IsSupported = false;
                //messageReporter.ReportMessage(this, nativeFilePath,
                //                              ErrorLevel.Error, StringResources.Sniffer_Message,
                //                              StringResources.Sniffer_Location);
                messageReporter.ReportMessage(this, nativeFilePath,
                                              ErrorLevel.Error, "dfdfdfdd",
                                              "dfddfdfdfddfdfs");
            }


            return fileInfo;
        }
    }
}
