﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 11.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Abra.Compiler.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Abra.Compiler;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "11.0.0.0")]
    public partial class CompiledPlugin : CompiledPluginBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\nnamespace ");
            
            #line 12 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(cls.RootNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    using Binding = global::Abra.Internal.Binding;\r\n    using Resolver = glo" +
                    "bal::Abra.Internal.Resolver;\r\n    using RuntimeModule = global::Abra.Internal.Ru" +
                    "ntimeModule;\r\n    using IPlugin = global::Abra.Internal.IPlugin;\r\n\r\n    public c" +
                    "lass ");
            
            #line 19 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(cls.GeneratedClassName));
            
            #line default
            #line hidden
            this.Write(@" : IPlugin
    {
        private delegate Binding LazyBindingCtor(string key, object requiredBy, string lazyKey);
        private delegate Binding ProvidesBindingCtor(string key, object requiredBy, bool mustBeInjectable, string providesKey);

        private readonly IDictionary<string, Func<RuntimeModule>> modules = new Dictionary<string, Func<RuntimeModule>>(StringComparer.Ordinal);
        private readonly IDictionary<string, Func<Binding>> injectBindings = new Dictionary<string, Func<Binding>>(StringComparer.Ordinal);
        private readonly IDictionary<string, LazyBindingCtor> lazyBindings = new Dictionary<string, LazyBindingCtor>(StringComparer.Ordinal);
        private readonly IDictionary<string, ProvidesBindingCtor> providesBindings = new Dictionary<string, ProvidesBindingCtor>(StringComparer.Ordinal);

        public ");
            
            #line 29 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(cls.GeneratedClassName));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n");
            
            #line 31 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
 foreach (var module in cls.Modules) { 
            
            #line default
            #line hidden
            this.Write("            modules[\"");
            
            #line 32 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(module.Key));
            
            #line default
            #line hidden
            this.Write("\"] = () => new ");
            
            #line 32 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(module.ClassName));
            
            #line default
            #line hidden
            this.Write("();\r\n");
            
            #line 33 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
 } 
            
            #line default
            #line hidden
            
            #line 34 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
 foreach (var injectBinding in cls.InjectBindings) { 
            
            #line default
            #line hidden
            this.Write("            injectBindings[\"");
            
            #line 35 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(injectBinding.Key));
            
            #line default
            #line hidden
            this.Write("\"] = () => new ");
            
            #line 35 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(injectBinding.ClassName));
            
            #line default
            #line hidden
            this.Write("();\r\n");
            
            #line 36 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 38 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
 foreach (var lazyBinding in cls.LazyBindings) { 
            
            #line default
            #line hidden
            this.Write("            lazyBindings[\"");
            
            #line 39 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(lazyBinding.Key));
            
            #line default
            #line hidden
            this.Write("\"] = (k, r, l) => new ");
            
            #line 39 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(lazyBinding.ClassName));
            
            #line default
            #line hidden
            this.Write("(k, r, l);\r\n");
            
            #line 40 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 42 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
 foreach (var providesBinding in cls.ProvidesBindings) { 
            
            #line default
            #line hidden
            this.Write("            providesBindings[\"");
            
            #line 43 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(providesBinding.Key));
            
            #line default
            #line hidden
            this.Write("\"] = (k, r, m, p) => new ");
            
            #line 43 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(providesBinding.ClassName));
            
            #line default
            #line hidden
            this.Write("(k, r, m, p);\r\n");
            
            #line 44 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"        }

        public Binding GetInjectBinding(string key, string className, bool mustBeInjectable)
        {
            var ctor = injectBindings[key];
            return ctor();
        }

        public Binding GetLazyInjectBinding(string key, object requiredBy, string lazyKey)
        {
            var ctor = lazyBindings[key];
            return ctor(key, requiredBy, lazyKey);
        }

        public Binding GetIProviderInjectBinding(string key, object requiredBy, bool mustBeInjectable, string providerKey)
        {
            var ctor = providesBindings[providerKey];
            return ctor(key, requiredBy, mustBeInjectable, providerKey);
        }

        public RuntimeModule GetRuntimeModule(Type moduleType, object moduleInstance)
        {
            var ctor = modules[moduleType.FullName];
            return ctor();
        }
    }
}");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "C:\Users\ben\Development\abra-ioc\Abra.Compiler\Templates\CompiledPlugin.tt"

private global::Abra.Compiler.Generators.PluginGenerator _clsField;

/// <summary>
/// Access the cls parameter of the template.
/// </summary>
private global::Abra.Compiler.Generators.PluginGenerator cls
{
    get
    {
        return this._clsField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool clsValueAcquired = false;
if (this.Session.ContainsKey("cls"))
{
    if ((typeof(global::Abra.Compiler.Generators.PluginGenerator).IsAssignableFrom(this.Session["cls"].GetType()) == false))
    {
        this.Error("The type \'Abra.Compiler.Generators.PluginGenerator\' of the parameter \'cls\' did no" +
                "t match the type of the data passed to the template.");
    }
    else
    {
        this._clsField = ((global::Abra.Compiler.Generators.PluginGenerator)(this.Session["cls"]));
        clsValueAcquired = true;
    }
}
if ((clsValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("cls");
    if ((data != null))
    {
        if ((typeof(global::Abra.Compiler.Generators.PluginGenerator).IsAssignableFrom(data.GetType()) == false))
        {
            this.Error("The type \'Abra.Compiler.Generators.PluginGenerator\' of the parameter \'cls\' did no" +
                    "t match the type of the data passed to the template.");
        }
        else
        {
            this._clsField = ((global::Abra.Compiler.Generators.PluginGenerator)(data));
        }
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "11.0.0.0")]
    public class CompiledPluginBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
