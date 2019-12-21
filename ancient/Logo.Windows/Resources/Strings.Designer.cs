﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Logo.Windows.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Logo.Windows.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to I made a mistake somewhere and can&apos;t work out what directory you meant..
        /// </summary>
        internal static string CommandChdirArgumentNullError {
            get {
                return ResourceManager.GetString("CommandChdirArgumentNullError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to That directory doesn&apos;t exist..
        /// </summary>
        internal static string CommandChdirDirectoryNotFoundError {
            get {
                return ResourceManager.GetString("CommandChdirDirectoryNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DIRECTORY.
        /// </summary>
        internal static string CommandChdirExampleText {
            get {
                return ResourceManager.GetString("CommandChdirExampleText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to That directory doesn&apos;t exist..
        /// </summary>
        internal static string CommandChdirFileNotFoundError {
            get {
                return ResourceManager.GetString("CommandChdirFileNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to That can&apos;t be a directory name..
        /// </summary>
        internal static string CommandChdirGeneralArgumentError {
            get {
                return ResourceManager.GetString("CommandChdirGeneralArgumentError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The computer told me there was an error..
        /// </summary>
        internal static string CommandChdirGeneralIOError {
            get {
                return ResourceManager.GetString("CommandChdirGeneralIOError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Change the &quot;working directory&quot; to DIRECTORY..
        /// </summary>
        internal static string CommandChdirHelpText {
            get {
                return ResourceManager.GetString("CommandChdirHelpText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to That&apos;s too long to be a directory name..
        /// </summary>
        internal static string CommandChdirPathTooLongError {
            get {
                return ResourceManager.GetString("CommandChdirPathTooLongError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The computer won&apos;t let you do that..
        /// </summary>
        internal static string CommandChdirSecurityError {
            get {
                return ResourceManager.GetString("CommandChdirSecurityError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to I can&apos;t use that as the name of a directory to move to..
        /// </summary>
        internal static string CommandChdirWrongTypeError {
            get {
                return ResourceManager.GetString("CommandChdirWrongTypeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns the name of the current &quot;working directory&quot;..
        /// </summary>
        internal static string CommandCurrentdirHelpText {
            get {
                return ResourceManager.GetString("CommandCurrentdirHelpText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns the names of all directories inside the current &quot;working directory&quot;..
        /// </summary>
        internal static string CommandDirectoriesHelpText {
            get {
                return ResourceManager.GetString("CommandDirectoriesHelpText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXT.
        /// </summary>
        internal static string CommandFilesExampleText {
            get {
                return ResourceManager.GetString("CommandFilesExampleText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns the names of all files in the current &quot;working directory&quot; whose names end in EXT.
        /// </summary>
        internal static string CommandFilesHelpText {
            get {
                return ResourceManager.GetString("CommandFilesHelpText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to I can&apos;t use that as the name of a type of file..
        /// </summary>
        internal static string CommandFilesWrongTypeError {
            get {
                return ResourceManager.GetString("CommandFilesWrongTypeError", resourceCulture);
            }
        }
    }
}
