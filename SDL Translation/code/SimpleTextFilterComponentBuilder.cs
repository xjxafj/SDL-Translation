using System;
using System.Windows.Forms;
using Sdl.Core.Globalization;
using Sdl.FileTypeSupport.Framework;
using Sdl.FileTypeSupport.Framework.IntegrationApi;
using Sdl.FileTypeSupport.Framework.NativeApi;

namespace SDL_Translation.code
{
    /// <summary>
    /// Define Simple Text filter component builder.
    /// </summary> 
    [FileTypeComponentBuilderAttribute(Id = "SimpleText_FilterComponentBuilderExtension_Id",
                                       Name = "SimpleText_FilterComponentBuilderExtension_Name",
                                       Description = "SimpleText_FilterComponentBuilderExtension_Description")]
    public class SimpleTextFilterComponentBuilder : IFileTypeComponentBuilder
    {
        /// <summary>
        /// Gets or sets file type manager
        /// </summary>
        public IFileTypeManager FileTypeManager { get; set; }

        /// <summary>
        /// Gets or sets Filter Definition
        /// </summary>
        public IFileTypeDefinition FileTypeDefinition { get; set; }

        /// <summary>
        /// Returns a file type information object.
        /// </summary>
        /// <param name="name">The <see cref="IFileTypeDefinition"/> will pass "" as the name for this parameter</param>
        /// <returns>an SimpleText file type information object</returns>
        public virtual IFileTypeInformation BuildFileTypeInformation(string name)
        {
            MessageBox.Show(name);
            var info = this.FileTypeManager.BuildFileTypeInformation();
            info.FileTypeDefinitionId = new FileTypeDefinitionId("Simple Text Filter 1.0.0.0");
            info.FileTypeName = new LocalizableString("Simple text files");
            info.FileTypeDocumentName = new LocalizableString("Test text files");
            info.FileTypeDocumentsName = new LocalizableString("Simple text files");
            info.Description = new LocalizableString("This sample filter is used to process simple text files.");
            info.FileDialogWildcardExpression = "*.text";
            info.DefaultFileExtension = "text";
            info.Icon = new IconDescriptor("assembly://Sdl.Sdk.FileTypeSupport.Samples.SimpleText/Sdl.Sdk.FileTypeSupport.Samples.SimpleText.SimpleText.ico");
            info.WinFormSettingsPageIds = new string[]
            {
                "SimpleText_Settings",
                "QuickInserts_Settings",
            };
            return info;
        }

        /// <summary>
        /// Gets the file sniffer for this component.
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>An Simple Text Native Filter Sniffer</returns>
        public virtual INativeFileSniffer BuildFileSniffer(string name)
        {
            return new SimpleTextSniffer();
        }

        /// <summary>
        /// Gets the file extractor for this component.
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>a FileExtractor containing an Simple Text Parser</returns>
        public virtual IFileExtractor BuildFileExtractor(string name)
        {
            throw new NotImplementedException();

            //// remember to call the original component builder method
            //var extractor = Original.BuildFileExtractor(name);
            //// add a pre tweaker
            //extractor.AddFileTweaker(new SimpleTextExtensionPreTweaker { RequireValidEncoding = false });
            //// add a native content processor
            //if (extractor.NativeExtractor != null)
            //{
            //    extractor.NativeExtractor.AddProcessor(new NativeContentProcessorStub());
            //}
            //// add a bilingual content processor
            //extractor.AddBilingualProcessor(new BilingualContentProcessorStub());
            //return extractor;
        }

        /// <summary>
        /// Gets the file generator for this component.
        /// </summary>
        /// <param name="name">not used herer</param>
        /// <returns><c>Null</c> if no file generator is defined</returns>
        public virtual IFileGenerator BuildFileGenerator(string name)
        {
            throw new NotImplementedException();
            //return this.FileTypeManager.BuildFileGenerator(this.FileTypeManager.BuildNativeGenerator(new SimpleTextWriter()));
        }

        /// <summary>
        /// Gets the different sets of previews supported for this component.
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>not implemented</returns>
        public virtual IPreviewSetsFactory BuildPreviewSetsFactory(string name)
        {
            IPreviewSetsFactory previewFactory = FileTypeManager.BuildPreviewSetsFactory();

            //preview in external application
            IPreviewSet previewSet = previewFactory.CreatePreviewSet();
            previewSet.Id = new PreviewSetId("ExternalPreview");
            previewSet.Name = new LocalizableString("assembly://Sdl.Sdk.FileTypeSupport.Samples.SimpleText/Sdl.Sdk.FileTypeSupport.Samples.SimpleText.Properties.Resources/ExternalPreview_Name");

            IApplicationPreviewType sourceAppPreviewType = previewFactory.CreatePreviewType<IApplicationPreviewType>() as IApplicationPreviewType;

            if (sourceAppPreviewType != null)
            {
                sourceAppPreviewType.SingleFilePreviewApplicationId = new PreviewApplicationId("ExternalPreview");
                previewSet.Source = sourceAppPreviewType;
            }

            IApplicationPreviewType targetAppPreviewType = previewFactory.CreatePreviewType<IApplicationPreviewType>() as IApplicationPreviewType;
            if (targetAppPreviewType != null)
            {
                targetAppPreviewType.SingleFilePreviewApplicationId = new PreviewApplicationId("ExternalPreview");
                previewSet.Target = targetAppPreviewType;
            }

            previewFactory.GetPreviewSets(null).Add(previewSet);

            //internal static preview
            IPreviewSet internalPreviewSet = previewFactory.CreatePreviewSet();
            internalPreviewSet.Id = new PreviewSetId("InternalStaticPreview");
            internalPreviewSet.Name = new LocalizableString("assembly://Sdl.Sdk.FileTypeSupport.Samples.SimpleText/Sdl.Sdk.FileTypeSupport.Samples.SimpleText.Properties.Resources/InternalStaticPreview_Name");

            IControlPreviewType sourceCtrlPreviewType = previewFactory.CreatePreviewType<IControlPreviewType>() as IControlPreviewType;
            if (sourceCtrlPreviewType != null)
            {
                sourceCtrlPreviewType.TargetGeneratorId = new GeneratorId("InternalPreview");
                internalPreviewSet.Source = sourceCtrlPreviewType;
            }

            IControlPreviewType targetCtrlPreviewType = previewFactory.CreatePreviewType<IControlPreviewType>() as IControlPreviewType;
            if (targetCtrlPreviewType != null)
            {
                targetCtrlPreviewType.TargetGeneratorId = new GeneratorId("InternalPreview");
                internalPreviewSet.Target = targetCtrlPreviewType;
            }
            previewFactory.GetPreviewSets(null).Add(internalPreviewSet);

            //source and target real-time navigable HTML browser control preview
            IPreviewSet internalHtmlPreviewSet = previewFactory.CreatePreviewSet();
            internalHtmlPreviewSet.Id = new PreviewSetId("InternalRealTimeNavigableHtmlPreview");
            internalHtmlPreviewSet.Name = new LocalizableString("assembly://Sdl.Sdk.FileTypeSupport.Samples.SimpleText/Sdl.Sdk.FileTypeSupport.Samples.SimpleText.Properties.Resources/InternalRealTimeNavigablePreview_Name");

            IControlPreviewType sourceInternalHtmlCtrlPreviewType = previewFactory.CreatePreviewType<IControlPreviewType>() as IControlPreviewType;
            if (sourceInternalHtmlCtrlPreviewType != null)
            {
                sourceInternalHtmlCtrlPreviewType.TargetGeneratorId = new GeneratorId("InternalPreview");
                sourceInternalHtmlCtrlPreviewType.SingleFilePreviewControlId = new PreviewControlId("InternalRealtimeNavigableBrowser");
                internalHtmlPreviewSet.Source = sourceInternalHtmlCtrlPreviewType;
            }

            IControlPreviewType targetInternalHtmlCtrlPreviewType = previewFactory.CreatePreviewType<IControlPreviewType>() as IControlPreviewType;
            if (targetInternalHtmlCtrlPreviewType != null)
            {
                targetInternalHtmlCtrlPreviewType.TargetGeneratorId = new GeneratorId("InternalPreview");
                targetInternalHtmlCtrlPreviewType.SingleFilePreviewControlId =
                    new PreviewControlId("InternalRealtimeNavigableBrowser");
                internalHtmlPreviewSet.Target = targetCtrlPreviewType;
            }
            previewFactory.GetPreviewSets(null).Add(internalHtmlPreviewSet);

            return previewFactory;
        }

        /// <summary>
        /// Creates a new instance of the preview control with the specified name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Should only be called from the main thread, as controls must always be
        /// instantiated on the same thread as the application message pump.
        /// </para>
        /// </remarks>
        /// <param name="name">not used here</param>
        /// <returns>not implemented</returns>
        public virtual IAbstractPreviewControl BuildPreviewControl(string name)
        {

            throw new NotImplementedException();
            //if (name == "PreviewControl_InternalStaticPreviewControl")
            //{
            //    return new InternalPreviewController();
            //}
            //else if (name == "PreviewControl_InternalRealtimeNavigableBrowser")
            //{
            //    //return GenericInternalWebBrowserPreviewControl();
            //    throw new NotImplementedException();
            //}
            //else
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Gets the QuickTags object for this component.
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>a Quick tags factory</returns>
        public virtual IQuickTagsFactory BuildQuickTagsFactory(string name)
        {
            throw new NotImplementedException();
            //IQuickTagsFactory quickTags = FileTypeManager.BuildQuickTagsFactory();
            //quickTags.GetQuickTags(null).SetStandardQuickTags(QuickInsertBuilder.BuildStandardQuickTags());
            //return quickTags;
        }

        /// <summary>
        /// Gets the verifier list of this component.
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>a verifier collection</returns>
        /// <remarks> The verifier list is an optional component for a file type.</remarks>
        public virtual IVerifierCollection BuildVerifierCollection(string name)
        {
            return null;
        }

        /// <summary>
        /// Gets a native or bilingual document generator of the type
        /// defined for the specified name.
        /// </summary>
        /// <param name="name">Abstract generator name</param>
        /// <returns>not generator for default preview</returns>
        public virtual IAbstractGenerator BuildAbstractGenerator(string name)
        {
            //if (name == "Generator_DefaultPreview")
            //{
            //    return FileTypeManager.BuildFileGenerator(FileTypeManager.BuildNativeGenerator(new SimpleTextWriter()));
            //}
            //if (name == "Generator_InternalPreview")
            //{
            //    INativeGenerator nativeGenerator = FileTypeManager.BuildNativeGenerator(new InternalPreviewWriter());
            //    IFileGenerator fileGenerator = FileTypeManager.BuildFileGenerator(nativeGenerator);
            //    return fileGenerator;
            //}
            //return null;

            throw new NotImplementedException();
        }

        /// <summary>
        /// The the additional generator information for this file type
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>not implemented</returns>
        public virtual IAdditionalGeneratorsInfo BuildAdditionalGeneratorsInfo(string name)
        {
            return null;
        }

        /// <summary>
        /// Gets the bilingual writer components for this component (if any).
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns><c>null</c> if no bilingual generator is defined</returns>
        public virtual IBilingualDocumentGenerator BuildBilingualGenerator(string name)
        {
            return null;
        }

        /// <summary>
        /// Creates a new instance of the preview application with the specified name.
        /// Right now only allows to build expternal preview application.
        /// </summary>
        /// <param name="name">Preview application name</param>
        /// <returns>External preview application</returns>
        public virtual IAbstractPreviewApplication BuildPreviewApplication(string name)
        {
            if (name == "PreviewApplication_ExternalPreview")
            {
                // GenericExteralPreviewApplication genericExteralPreviewApplication = new GenericExteralPreviewApplication();
                // genericExteralPreviewApplication.ApplicationPath = @"c:\Windows\System32\notepad.exe";
                // return genericExteralPreviewApplication;
                throw new NotImplementedException();
            }
            else
            {
                return null;
            }
        }
    }
}
