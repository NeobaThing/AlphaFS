/*  Copyright (C) 2008-2018 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlphaFS.UnitTest
{
   public partial class CopyTest
   {
      // Pattern: <class>_<function>_<scenario>_<expected result>


      [TestMethod]
      public void AlphaFS_Directory_Copy_LocalAndNetwork_Success()
      {
         AlphaFS_Directory_Copy(false);
         AlphaFS_Directory_Copy(true);
      }


      private void AlphaFS_Directory_Copy(bool isNetwork)
      {
         using (var tempRoot = new TemporaryDirectory(isNetwork))
         {
            var folderSrc = tempRoot.CreateRecursiveRandomizedDatesAndAttributesTree(5);
            var folderDst = tempRoot.RandomDirectoryFullPath;
            
            Console.WriteLine("Src Directory Path: [{0}]", folderSrc.FullName);
            Console.WriteLine("Dst Directory Path: [{0}]", folderDst);


            const Alphaleonis.Win32.Filesystem.DirectoryEnumerationOptions dirEnumOptions = Alphaleonis.Win32.Filesystem.DirectoryEnumerationOptions.FilesAndFolders | Alphaleonis.Win32.Filesystem.DirectoryEnumerationOptions.Recursive;

            var props = Alphaleonis.Win32.Filesystem.Directory.GetProperties(folderSrc.FullName, dirEnumOptions);

            var sourceTotal = props["Total"];
            var sourceTotalFiles = props["File"];
            var sourceTotalSize = props["Size"];
            
            Console.WriteLine("\n\tTotal size: [{0}] - Total Folders: [{1}] - Files: [{2}]", Alphaleonis.Utils.UnitSizeToText(sourceTotalSize), sourceTotal - sourceTotalFiles, sourceTotalFiles);
            

            var copyResult = Alphaleonis.Win32.Filesystem.Directory.Copy(folderSrc.FullName, folderDst, Alphaleonis.Win32.Filesystem.CopyOptions.FailIfExists);

            UnitTestConstants.Dump(copyResult);


            props = Alphaleonis.Win32.Filesystem.Directory.GetProperties(folderDst, dirEnumOptions);
            Assert.AreEqual(sourceTotal, props["Total"], "The number of total file system objects do not match.");
            Assert.AreEqual(sourceTotalFiles, props["File"], "The number of total files do not match.");
            Assert.AreEqual(sourceTotalSize, props["Size"], "The total file size does not match.");
            Assert.AreNotEqual(null, copyResult);


            // Test against copyResult results.

            Assert.AreEqual(sourceTotal, copyResult.TotalFolders + copyResult.TotalFiles, "The number of total file system objects do not match.");
            Assert.AreEqual(sourceTotalFiles, copyResult.TotalFiles, "The number of total files do not match.");
            Assert.AreEqual(sourceTotalSize, copyResult.TotalBytes, "The total file size does not match.");
            Assert.IsTrue(copyResult.IsCopy);
            Assert.IsFalse(copyResult.IsMove);
            Assert.IsTrue(copyResult.IsDirectory);
            Assert.IsFalse(copyResult.IsFile);

            Assert.IsTrue(System.IO.Directory.Exists(folderSrc.FullName), "The original directory does not exist, but is expected to.");
         }


         Console.WriteLine();
      }
   }
}
