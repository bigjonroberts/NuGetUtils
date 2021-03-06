﻿/*
 * Copyright 2019 Stanislav Muhametsin. All rights Reserved.
 *
 * Licensed  under the  Apache License,  Version 2.0  (the "License");
 * you may not use  this file  except in  compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed  under the  License is distributed on an "AS IS" BASIS,
 * WITHOUT  WARRANTIES OR CONDITIONS  OF ANY KIND, either  express  or
 * implied.
 *
 * See the License for the specific language governing permissions and
 * limitations under the License. 
 */
using NuGetUtils.Lib.Exec.Agnostic;
using System;

namespace NuGetUtils.Lib.Exec.Agnostic
{
   /// <summary>
   /// This data interface provides required information to execute a method within assembly located in NuGet package.
   /// </summary>
   /// <remarks>All properties except <see cref="PackageID"/> are optional.</remarks>
   public interface NuGetExecutionConfiguration
   {
      /// <summary>
      /// Gets the package ID of the NuGet package containing assembly with the method to execute.
      /// </summary>
      /// <value>The package ID of the NuGet package containing assembly with the method to execute.</value>
      /// <remarks>This property is required.</remarks>
      String PackageID { get; }

      /// <summary>
      /// Gets the package version of the NuGet package containing assembly with the method to execute.
      /// </summary>
      /// <value>The package version of the NuGet package containing assembly with the method to execute.</value>
      /// <remarks>If this property is <c>null</c> or empty string, then NuGet source will be queried for the newest version.</remarks>
      String PackageVersion { get; }

      /// <summary>
      /// Gets the path within the target folder (e.g. <c>"lib/netstandard1.3"</c>) of the NuGet package pointing to the assembly containing method to execute.
      /// </summary>
      /// <value>The path within the target folder (e.g. <c>"lib/netstandard1.3"</c>) of the NuGet package pointing to the assembly containing method to execute.</value>
      /// <remarks>This is required only when the NuGet package contains more than one assembly in its target folder, and the method to execute is not located in assembly named as package ID.</remarks>
      String AssemblyPath { get; }

      /// <summary>
      /// Gets the full name of the type within assembly which contains the method to execute.
      /// </summary>
      /// <value>The full name of the type within assembly which contains the method to execute.</value>
      /// <remarks>This is needed, together with <see cref="EntrypointMethodName"/>, when the assembly does not have <see cref="T:NuGetUtils.Lib.EntryPoint.ConfiguredEntryPointAttribute"/> applied to, and it is also lacking the entrypoint information within DLL file; or when the method to be executed is neither of those.</remarks>
      /// <seealso cref="T:NuGetUtils.Lib.EntryPoint.ConfiguredEntryPointAttribute"/>
      String EntrypointTypeName { get; }

      /// <summary>
      /// Gets the name of the method to execute.
      /// </summary>
      /// <value>The name of the method to execute.</value>
      /// <remarks>This is needed, optionally together with <see cref="EntrypointTypeName"/>, when the assembly does not have <see cref="T:NuGetUtils.Lib.EntryPoint.ConfiguredEntryPointAttribute"/> applied to, and it is also lacking the entrypoint information within DLL file; or when the method to be executed is neither of those.</remarks>
      /// <seealso cref="T:NuGetUtils.Lib.EntryPoint.ConfiguredEntryPointAttribute"/>
      String EntrypointMethodName { get; }

      /// <summary>
      /// Gets the optional path where to write the return value of the method.
      /// </summary>
      /// <value>The optional path where to write the return value of the method.</value>
      /// <remarks>Currently there is only one format to write the return value: JSON.</remarks>
      String ReturnValuePath { get; }

      /// <summary>
      /// Gets the optional entrypoint method token. This method token overrides <see cref="EntrypointTypeName"/> and <see cref="EntrypointMethodName"/> values if they are specified.
      /// </summary>
      /// <value>The optional entrypoint method token.</value>
      Int32? MethodToken { get; }

#if !NET46
      /// <summary>
      /// Gets the value indicating whether the SDK package (typically "Microsoft.NETCore.App" on .NET Core) should be restored.
      /// </summary>
      /// <value>The value indicating whether the SDK package (typically "Microsoft.NETCore.App" on .NET Core) should be restored.</value>
      Boolean RestoreSDKPackage { get; }

#endif
   }
}

/// <summary>
/// This class contains extension methods for types defined in this assembly.
/// </summary>
public static partial class E_NuGetUtils
{
   /// <summary>
   /// Checks that this <see cref="NuGetExecutionConfiguration"/> is valid.
   /// The configuration is valid when <see cref="NuGetExecutionConfiguration"/> itself is not null, and its <see cref="NuGetExecutionConfiguration.PackageID"/> is not <c>null</c> and not empty string.
   /// </summary>
   /// <param name="configuration">This <see cref="NuGetExecutionConfiguration"/>.</param>
   /// <returns><c>true</c> if this <see cref="NuGetExecutionConfiguration"/> is valid; <c>false</c> otherwise.</returns>
   public static Boolean ValidateConfiguration(
      this NuGetExecutionConfiguration configuration
      )
   {
      // Only package ID is necessary
      return !String.IsNullOrEmpty( configuration?.PackageID );
   }
}