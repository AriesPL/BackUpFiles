using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backupey
{
	class Backuper
    {
        private readonly string _targetPath;
        private readonly List<string> _sourcePaths = new List<string>();
        private readonly string _timestamp;

		public Backuper(string targetPath, string[] sourcePaths, string timestamp)
		{
			if (!ValidatePath(targetPath))
			{
				var error = $"Не верно указан путь целевой папки: {targetPath}.";
				throw new ArgumentException(error);
			}
			_targetPath = targetPath;

			foreach (var sourcePath in sourcePaths)
			{
				if (!ValidatePath(sourcePath))
				{
					continue;
				}

				if (!Directory.Exists(sourcePath))
				{
					continue;
				}

				_sourcePaths.Add(sourcePath);
			}

			if (!_sourcePaths.Any())
			{
				var error = $"Не указаны валидные пути исходных папок.";
				throw new ArgumentException(error);
			}

			_timestamp = timestamp;
		}

		/// <summary>
		/// Выполнить бекап папок
		/// </summary>
		public void Backup()
        {
            var backupPath = Path.Combine(_targetPath, _timestamp);
            Directory.CreateDirectory(backupPath);

            foreach (var sourcePath in _sourcePaths)
            {
                var targetPath = GetUniqueTargetPath(backupPath, sourcePath);
                Directory.CreateDirectory(targetPath);

                CopyFilesRecursively(sourcePath, targetPath);
            }
        }

        /// <summary>
        /// Проверить путь на валидность
        /// </summary>
        /// <param name="targetPath">Путь к директории</param>
        /// <returns>True - путь валидный</returns>
        private static bool ValidatePath(string targetPath)
        {
            if (string.IsNullOrEmpty(targetPath))
                return false;

            try
            {
                // Выбрасывает ArgumentException если параметр содержит невалидные символы, пустой или содержит только пробелы.
                string root = Path.GetPathRoot(targetPath);

                if (string.IsNullOrEmpty(root))
                    return false;

                // Выбрасывает ArgumentException если параметр содержит невалидные символы, определенные в GetInvalidPathChars или
                // если передать String.Empty в параметр.
                string directory = Path.GetDirectoryName(targetPath);

                if (string.IsNullOrEmpty(directory))
                    return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Получить уникальное имя целевой папки с временным штампом
        /// </summary>
        /// <param name="targetPath">Целевой путь</param>
        private string GetUniqueTargetPath(string backupPath, string sourcePath)
        {
            var name = new DirectoryInfo(sourcePath).Name;

            var basePath = Path.Combine(backupPath, name);

            var newTargetPath = basePath;

            var n = 0;
            while (Directory.Exists(newTargetPath))
            {
                newTargetPath = $"{basePath}({n})";
                n++;
            }

            return newTargetPath;
        }

        /// <summary>
        /// Скопировать содержимое папки
        /// </summary>
        /// <param name="sourcePath">Папка источник</param>
        /// <param name="targetPath">Целевая папка</param>
        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            // Создать все папки, включая вложенные
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                var replacedPath = dirPath.Replace(sourcePath, targetPath);
                Directory.CreateDirectory(replacedPath);
            }

            // Создать копии всех файлов
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                var replacedFile = newPath.Replace(sourcePath, targetPath);
                File.Copy(newPath, replacedFile, true);
            }
        }

    }
}

