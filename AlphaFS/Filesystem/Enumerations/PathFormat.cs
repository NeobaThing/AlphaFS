/* Copyright (C) 2008-2015 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy 
 *  of this software and associated documentation files (the "Software"), to deal 
 *  in the Software without restriction, including without limitation the rights 
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 *  copies of the Software, and to permit persons to whom the Software is 
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 *  THE SOFTWARE. 
 */

namespace Alphaleonis.Win32.Filesystem
{
   /// <summary>Indicates the format of a path passed to a method.</summary>
   /// <remarks>
   /// At some point in code you know the full path of file system objects, e.g.: "C:\Windows".
   /// For example, Directory.EnumerateFileSystemEntries() will return all files and directories from a given path.
   /// Most likely, some processing will happen on the results of the enum. The file or directory may be passed
   /// on to another function. Whenever a file path is required, some performance can be gained.
   /// 
   /// A path like: "C:\Windows" or "\\server\share" is considered a full path for a directory because it is rooted.
   /// If the method supports it, <see cref="PathFormat.FullPath"/> and <see cref="PathFormat.LongFullPath"/> will skip GetFullPath() calls,
   /// for path resolving of the object, while also avoiding path validation and checks.
   /// Using <see cref="PathFormat.FullPath"/> (default) will always call GetFullPath() and perform path validation and checks.
   /// </remarks>
   public enum PathFormat
   {
      /// <summary>The format of the path is automatically detected by the method and internally converted to an extended length path.
      /// It can be either a standard (short) full path, an extended length (unicode) full path or a relative path.
      /// Example relative path: "Windows".
      /// </summary>
      RelativePath,

      /// <summary>The path is a full path in either normal or extended length (UNICODE) format.
      /// Internally it will be converted to an extended length (UNICODE) path.
      /// Using this option has a very slight performance advantage compared to using <see cref="RelativePath"/>.
      /// Example full path: "C:\Windows" or "\\server\share".
      /// </summary>
      FullPath,

      /// <summary>The path is an extended length path. No additional processing will be done on the path, and it will be used as is.
      /// This option has a slight performance advantage to using the <see cref="RelativePath"/> option.
      /// Example long full path: "\\?\C:\Windows" or "\\?\UNC\server\share".
      /// </summary>
      LongFullPath
   }
}