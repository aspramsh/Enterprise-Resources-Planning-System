﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERPS.HR {
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
    internal class HRDestinations {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal HRDestinations() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ERPS.HR.HRDestinations", typeof(HRDestinations).Assembly);
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
        ///   Looks up a localized string similar to /API/Dal/AddDalEmployee.
        /// </summary>
        internal static string AddDalemployee {
            get {
                return ResourceManager.GetString("AddDalemployee", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /API/Dal/DeleteDalEmployee.
        /// </summary>
        internal static string DeleteDalEmployee {
            get {
                return ResourceManager.GetString("DeleteDalEmployee", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /Api/HRExternal/SendCandidate.
        /// </summary>
        internal static string GetDalCandidate {
            get {
                return ResourceManager.GetString("GetDalCandidate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /API/Dal/GetDalEmployees.
        /// </summary>
        internal static string GetDalEmployee {
            get {
                return ResourceManager.GetString("GetDalEmployee", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/DAL/GetHREmployeeByid.
        /// </summary>
        internal static string GetDalEmployeeId {
            get {
                return ResourceManager.GetString("GetDalEmployeeId", resourceCulture);
            }
        }
    }
}