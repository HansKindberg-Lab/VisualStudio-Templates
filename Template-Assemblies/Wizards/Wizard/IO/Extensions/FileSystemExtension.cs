using System;
using System.IO.Abstractions;

namespace HansKindberg.VisualStudio.Templates.Wizards.IO.Extensions
{
	public static class FileSystemExtension
	{
		#region Methods

		public static void CopyDirectory(this IFileSystem fileSystem, string sourceDirectoryPath, string destinationDirectoryPath, bool replaceDirectories, bool overwriteFiles, bool createDestinationDirectoryIfNecessary)
		{
			if(fileSystem == null)
				throw new ArgumentNullException("fileSystem");

			// Just to throw an exception if the source-directory does not exist.
			fileSystem.Directory.GetFiles(sourceDirectoryPath);

			fileSystem.CopyDirectoryInternal(sourceDirectoryPath, destinationDirectoryPath, replaceDirectories, overwriteFiles, createDestinationDirectoryIfNecessary);
		}

		private static void CopyDirectoryInternal(this IFileSystem fileSystem, string sourceDirectoryPath, string destinationDirectoryPath, bool replaceDirectories, bool overwriteFiles, bool createDestinationDirectoryIfNecessary)
		{
			if(fileSystem == null)
				throw new ArgumentNullException("fileSystem");

			var sourceDirectory = fileSystem.DirectoryInfo.FromDirectoryName(sourceDirectoryPath);

			var destinationDirectory = fileSystem.DirectoryInfo.FromDirectoryName(destinationDirectoryPath);

			if(replaceDirectories && destinationDirectory.Exists)
				fileSystem.Directory.Delete(destinationDirectoryPath);

			if(createDestinationDirectoryIfNecessary && !fileSystem.Directory.Exists(destinationDirectoryPath))
				fileSystem.Directory.CreateDirectory(destinationDirectoryPath);

			foreach(var file in sourceDirectory.GetFiles())
			{
				var destinationFilePath = fileSystem.Path.Combine(destinationDirectoryPath, file.Name);

				if(overwriteFiles)
					fileSystem.File.Copy(file.FullName, destinationFilePath, true);
				else if(!fileSystem.File.Exists(destinationFilePath))
					fileSystem.File.Copy(file.FullName, destinationFilePath);
			}

			foreach(var directory in sourceDirectory.GetDirectories())
			{
				fileSystem.CopyDirectoryInternal(directory.FullName, fileSystem.Path.Combine(destinationDirectoryPath, directory.Name), replaceDirectories, overwriteFiles, createDestinationDirectoryIfNecessary);
			}
		}

		public static void CopyFile(this IFileSystem fileSystem, string sourceFilePath, string destinationDirectoryPath, bool overwrite, bool createDestinationDirectoryIfNecessary)
		{
			if(fileSystem == null)
				throw new ArgumentNullException("fileSystem");

			// Just to throw an exception if the source-file does not exist.
			fileSystem.File.GetAttributes(sourceFilePath);

			var sourceFile = fileSystem.FileInfo.FromFileName(sourceFilePath);

			if(createDestinationDirectoryIfNecessary && !fileSystem.Directory.Exists(destinationDirectoryPath))
				fileSystem.Directory.CreateDirectory(destinationDirectoryPath);

			var destinationFile = fileSystem.FileInfo.FromFileName(fileSystem.Path.Combine(destinationDirectoryPath, sourceFile.Name));

			fileSystem.File.Copy(sourceFilePath, destinationFile.FullName, overwrite);
		}

		public static void CreateFile(this IFileSystem fileSystem, string filePath, bool overwrite)
		{
			if(fileSystem == null)
				throw new ArgumentNullException("fileSystem");

			if(fileSystem.File.Exists(filePath) && !overwrite)
				return;

			if(!fileSystem.File.Exists(filePath))
			{
				var directory = fileSystem.FileInfo.FromFileName(filePath).Directory;

				if(!directory.Exists)
					fileSystem.Directory.CreateDirectory(directory.FullName);
			}

			using(fileSystem.File.Create(filePath)) {}
		}

		#endregion
	}
}